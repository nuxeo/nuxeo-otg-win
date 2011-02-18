using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace Nuxeo.Otg.Win
{
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("01A57E3A-3100-445C-877E-12578F32D4BB"), ComVisible(true)]
    public class ContextMenuExt : IShellExtInit, IContextMenu
    {
        protected static bool DEBUG = true;

        private List<string> selectedFiles;
        private ActionsDispatcher currentActionDispacher;

        #region Shell Extension Registration Members

        [ComRegisterFunction()]
        public static void Register(Type t)
        {
            try
            {
                ShellExtensionRegister.RegisterShellExtContextMenuHandler(t.GUID, "NuxeoOtg");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        [ComUnregisterFunction()]
        public static void Unregister(Type t)
        {
            try
            {
                ShellExtensionRegister.UnregisterShellExtContextMenuHandler(t.GUID, "NuxeoOtg");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        #endregion

        #region IShellExtInit Members
        public void Initialize(IntPtr pidlFolder, IntPtr pDataObj, IntPtr hKeyProgID)
        {
            if (pDataObj == IntPtr.Zero)
            {
                throw new ArgumentException();
            }

            FORMATETC fe = new FORMATETC();
            fe.cfFormat = (short)CLIPFORMAT.HDROP;
            fe.ptd = IntPtr.Zero;
            fe.dwAspect = DVASPECT.DVASPECT_CONTENT;
            fe.lindex = -1;
            fe.tymed = TYMED.TYMED_HGLOBAL;
            STGMEDIUM stm = new STGMEDIUM();

            // The pDataObj pointer contains the objects being acted upon. In this 
            // example, we get an HDROP handle for enumerating the selected files 
            // and folders.
            IDataObject dataObject = (IDataObject)Marshal.GetObjectForIUnknown(pDataObj);
            dataObject.GetData(ref fe, out stm);

            try
            {
                // Get an HDROP handle.
                IntPtr hDrop = stm.unionmember;
                if (hDrop == IntPtr.Zero)
                {
                    throw new ArgumentException();
                }

                uint nFiles = Import.DragQueryFile(hDrop, UInt32.MaxValue, null, 0);
                if (nFiles == 0)
                {
                    Marshal.ThrowExceptionForHR(ErrorCode.E_FAIL);
                }

                selectedFiles = new List<string>();
                for (uint i = 0; i < nFiles; i++)
                {
                    selectedFiles.Add(getFileName(hDrop, i));
                }
            }
            finally
            {
                Import.ReleaseStgMedium(ref stm);
            }
        }

        #endregion

        #region IContextMenu Members

        public int QueryContextMenu(
            IntPtr hMenu,
            uint iMenu,
            uint idCmdFirst,
            uint idCmdLast,
            uint uFlags)
        {
            try
            {
                if (0 != ((uint)CMF.DEFAULTONLY & uFlags))
                {
                    return ErrorCode.MAKE_HRESULT(ErrorCode.SEVERITY_SUCCESS, 0, 0);
                }

                IntPtr subMenu = Import.CreatePopupMenu();

                // XXX hard-coded status
                String state = Constants.STATE_UNATTACHED;
                if (selectedFiles.Count == 1 && Directory.Exists(selectedFiles[0])) {
                    // Do not display contextual menu on drive root.
                    if (selectedFiles[0].EndsWith(@"\"))
                    {
                        return ErrorCode.S_FALSE;
                    }
                    state = Constants.STATE_DIRECTORY;
                }

                // Add state and a separator to the OTG SubMenu
                MenuItemFactory.AddStateMenuItem(subMenu, state, 0);
                MenuItemFactory.AddSeparator(subMenu, 1);

                currentActionDispacher = new ActionsDispatcher(state);
                List<String> actions = currentActionDispacher.CurrentActions;
                // So dirty ... but it's quick for now :D
                for (uint i = 2; (i - 2) < actions.Count; i++)
                {
                    uint index = i - 2;
                    String[] texts = Constants.getLabels(actions[Convert.ToInt32(index)]);
                    MenuItemFactory.AddMenuItem(subMenu, texts[0], idCmdFirst + index, i + 1);
                }

                uint lastId = Convert.ToUInt32(actions.Count + 1);
                MENUITEMINFO mii = MenuItemFactory.CreateMenu("Nuxeo on the Go", lastId, subMenu);
                if (!MenuItemFactory.SaveMenuItem(hMenu, iMenu, ref mii))
                {
                    return Marshal.GetHRForLastWin32Error();
                }

                MENUITEMINFO sep = MenuItemFactory.CreateSeparator();
                if (!MenuItemFactory.SaveMenuItem(hMenu, iMenu + 1, ref sep))
                {
                    return Marshal.GetHRForLastWin32Error();
                }

                return ErrorCode.MAKE_HRESULT(ErrorCode.SEVERITY_SUCCESS, 0, lastId + 1);
            }
            catch (Exception e)
            {
                if (DEBUG)
                    System.Windows.Forms.MessageBox.Show(String.Format("{0} : {1}", e.Message, e.StackTrace));
                throw;
            }
        }

        public void InvokeCommand(IntPtr pici)
        {
            CMINVOKECOMMANDINFO ici = (CMINVOKECOMMANDINFO)Marshal.PtrToStructure(
                pici, typeof(CMINVOKECOMMANDINFO));
            int index = Import.LowWord(ici.verb.ToInt32());
            String action = currentActionDispacher.CurrentActions[index];
            currentActionDispacher.ExecuteAction(action, selectedFiles[0]);
        }

        public void GetCommandString(
            UIntPtr idCmd,
            uint uFlags,
            IntPtr pReserved,
            StringBuilder pszName,
            uint cchMax)
        {
            int index = Convert.ToInt32(idCmd.ToUInt32());
            String[] texts = Constants.getLabels(currentActionDispacher.CurrentActions[index]);
            // To change
            switch ((GCS)uFlags)
            {
                case GCS.VERBW:
                    pszName.Clear();
                    pszName.Append(texts[1]);
                    break;
                case GCS.HELPTEXTW:
                    pszName.Clear();
                    pszName.Append(texts[2]);
                    break;
            }
        }

        #endregion

        protected String getFileName(IntPtr hDrop, uint iFile)
        {
            StringBuilder fileName = new StringBuilder(260);
            if (0 == Import.DragQueryFile(hDrop, iFile, fileName,
                fileName.Capacity))
            {
                Marshal.ThrowExceptionForHR(ErrorCode.E_FAIL);
            }
            return fileName.ToString();
        }
    }
}