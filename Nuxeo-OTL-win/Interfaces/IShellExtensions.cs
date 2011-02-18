using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

[ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("000214e8-0000-0000-c000-000000000046")]
internal interface IShellExtInit
{
    void Initialize(
        IntPtr pidlFolder,
        IntPtr pDataObj,
        IntPtr hKeyProgID);
}


[ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("000214e4-0000-0000-c000-000000000046")]
internal interface IContextMenu
{
    [PreserveSig]
    int QueryContextMenu(
        IntPtr hMenu,
        uint iMenu,
        uint idCmdFirst,
        uint idCmdLast,
        uint uFlags);

    void InvokeCommand(IntPtr pici);

    void GetCommandString(
        UIntPtr idCmd,
        uint uFlags,
        IntPtr pReserved,
        StringBuilder pszName,
        uint cchMax);
}