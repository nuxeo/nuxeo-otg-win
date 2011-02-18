using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Nuxeo.Otg.Win
{
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("00A1405D-93F1-4672-B314-C25CBFCB3728"), ComVisible(true)]
    public class ShellIconOverlayExt //: IShellIconOverlayIdentifier
    {
        #region COM Registration
        //[ComRegisterFunction()]
        public static void Register(Type t)
        {
            try
            {
                ShellExtensionRegister.RegisterShellExtIconOverlay(t.GUID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); // Log the error
                throw;  // Re-throw the exception
            }
        }

        //[ComUnregisterFunction()]
        public static void Unregister(Type t)
        {
            try
            {
                ShellExtensionRegister.UnregisterShellExtIconOverlay(t.GUID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); // Log the error
                throw;  // Re-throw the exception
            }
        }
        #endregion

        #region IShellIconOverlayIdentifier Members

        public int IsMemberOf(string path, uint attributes)
        {
            MessageBox.Show("Is Member of with path : " + path);
            return ErrorCode.S_FALSE;
        }

        public int GetOverlayInfo(string iconFileBuffer, int iconFileBufferSize, out int iconIndex, out uint flags)
        {
            throw new NotImplementedException();
        }

        public int GetPriority(out int priority)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
