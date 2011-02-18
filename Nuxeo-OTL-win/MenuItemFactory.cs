using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Nuxeo.Otg.Win
{
    public class MenuItemFactory
    {
        private MenuItemFactory() { }

        internal static MENUITEMINFO CreateMenu(String text, uint id, IntPtr hSubMenu)
        {
            MENUITEMINFO menu = CreateMenu(text, id);
            menu.hSubMenu = hSubMenu;
            return menu;
        }

        internal static MENUITEMINFO CreateMenu(String text, uint id)
        {
            MENUITEMINFO mii = new MENUITEMINFO();
            mii.cbSize = (uint)Marshal.SizeOf(mii);
            mii.fMask = MIIM.ID | MIIM.STRING | MIIM.SUBMENU;
            mii.wID = id;
            mii.fType = MFT.STRING;
            mii.dwTypeData = text;
            return mii;
        }

        internal static MENUITEMINFO CreateSeparator()
        {
            MENUITEMINFO sep = new MENUITEMINFO();
            sep.cbSize = (uint)Marshal.SizeOf(sep);
            sep.fMask = MIIM.TYPE;
            sep.fType = MFT.SEPARATOR;

            return sep;
        }

        internal static void AddMenuItem(IntPtr hMenu, string text, uint id, uint position)
        {
            MENUITEMINFO mii = new MENUITEMINFO();
            mii.cbSize = (uint)Marshal.SizeOf(mii);
            mii.fMask = MIIM.ID | MIIM.TYPE | MIIM.STATE;
            mii.wID = id;
            mii.fType = MFT.STRING;
            mii.dwTypeData = text;
            mii.fState = MFS.ENABLED;
            Import.InsertMenuItem(hMenu, position, true, ref mii);
        }

        internal static void AddSeparator(IntPtr hMenu, uint position)
        {
            MENUITEMINFO sep = CreateSeparator();
            Import.InsertMenuItem(hMenu, position, true, ref sep);
        }

        internal static void AddStateMenuItem(IntPtr hMenu, string text, uint position)
        {
            MENUITEMINFO mii = new MENUITEMINFO();
            mii.cbSize = (uint)Marshal.SizeOf(mii);
            mii.fMask = MIIM.ID | MIIM.TYPE | MIIM.STATE;
            mii.fType = MFT.STRING;
            mii.dwTypeData = text;
            mii.fState = MFS.DISABLED;
            Import.InsertMenuItem(hMenu, position, true, ref mii);
        }

        internal static bool SaveMenuItem(IntPtr hMenu, uint iMenu, ref MENUITEMINFO mii)
        {
            return Import.InsertMenuItem(hMenu, iMenu, true, ref mii);
        }
    }
}
