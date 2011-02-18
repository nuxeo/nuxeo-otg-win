using System;
using System.Text;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

internal enum GCS : uint
{
    VERBA = 0x00000000,
    HELPTEXTA = 0x00000001,
    VALIDATEA = 0x00000002,
    VERBW = 0x00000004,
    HELPTEXTW = 0x00000005,
    VALIDATEW = 0x00000006,
    VERBICONW = 0x00000014,
    UNICODE = 0x00000004
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal struct CMINVOKECOMMANDINFO
{
    public uint cbSize;
    public CMIC fMask;
    public IntPtr hwnd;
    public IntPtr verb;
    [MarshalAs(UnmanagedType.LPStr)]
    public string parameters;
    [MarshalAs(UnmanagedType.LPStr)]
    public string directory;
    public int nShow;
    public uint dwHotKey;
    public IntPtr hIcon;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal struct CMINVOKECOMMANDINFOEX
{
    public uint cbSize;
    public CMIC fMask;
    public IntPtr hwnd;
    public IntPtr verb;
    [MarshalAs(UnmanagedType.LPStr)]
    public string parameters;
    [MarshalAs(UnmanagedType.LPStr)]
    public string directory;
    public int nShow;
    public uint dwHotKey;
    public IntPtr hIcon;
    [MarshalAs(UnmanagedType.LPStr)]
    public string title;
    public IntPtr verbW;
    public string parametersW;
    public string directoryW;
    public string titleW;
    POINT ptInvoke;
}

[Flags]
internal enum CMIC : uint
{
    ICON = 0x00000010,
    HOTKEY = 0x00000020,
    NOASYNC = 0x00000100,
    FLAG_NO_UI = 0x00000400,
    UNICODE = 0x00004000,
    NO_CONSOLE = 0x00008000,
    ASYNCOK = 0x00100000,
    NOZONECHECKS = 0x00800000,
    FLAG_LOG_USAGE = 0x04000000,
    SHIFT_DOWN = 0x10000000,
    PTINVOKE = 0x20000000,
    CONTROL_DOWN = 0x40000000
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct POINT
{
    public int X;
    public int Y;
}

internal enum CLIPFORMAT : uint
{
    TEXT = 1,
    BITMAP = 2,
    METAFILEPICT = 3,
    SYLK = 4,
    DIF = 5,
    TIFF = 6,
    OEMTEXT = 7,
    DIB = 8,
    PALETTE = 9,
    PENDATA = 10,
    RIFF = 11,
    WAVE = 12,
    UNICODETEXT = 13,
    ENHMETAFILE = 14,
    HDROP = 15,
    LOCALE = 16,
    MAX = 17,

    OWNERDISPLAY = 0x0080,
    DSPTEXT = 0x0081,
    DSPBITMAP = 0x0082,
    DSPMETAFILEPICT = 0x0083,
    DSPENHMETAFILE = 0x008E,
    PRIVATEFIRST = 0x0200,
    PRIVATELAST = 0x02FF,
    GDIOBJFIRST = 0x0300,
    GDIOBJLAST = 0x03FF
}

[Flags]
internal enum CMF : uint
{
    NORMAL = 0x00000000,
    DEFAULTONLY = 0x00000001,
    VERBSONLY = 0x00000002,
    EXPLORE = 0x00000004,
    NOVERBS = 0x00000008,
    CANRENAME = 0x00000010,
    NODEFAULT = 0x00000020,
    INCLUDESTATIC = 0x00000040,
    ITEMMENU = 0x00000080,
    EXTENDEDVERBS = 0x00000100,
    DISABLEDVERBS = 0x00000200,
    ASYNCVERBSTATE = 0x00000400,
    OPTIMIZEFORINVOKE = 0x00000800,
    SYNCCASCADEMENU = 0x00001000,
    DONOTPICKDEFAULT = 0x00002000,
    RESERVED = 0xFFFF0000
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal struct MENUITEMINFO
{
    public uint cbSize;
    public MIIM fMask;
    public MFT fType;
    public MFS fState;
    public uint wID;
    public IntPtr hSubMenu;
    public IntPtr hbmpChecked;
    public IntPtr hbmpUnchecked;
    public UIntPtr dwItemData;
    public string dwTypeData;
    public uint cch;
    public IntPtr hbmpItem;
}

[Flags]
internal enum MIIM : uint
{
    STATE = 0x00000001,
    ID = 0x00000002,
    SUBMENU = 0x00000004,
    CHECKMARKS = 0x00000008,
    TYPE = 0x00000010,
    DATA = 0x00000020,
    STRING = 0x00000040,
    BITMAP = 0x00000080,
    FTYPE = 0x00000100
}

internal enum MFT : uint
{
    STRING = 0x00000000,
    BITMAP = 0x00000004,
    MENUBARBREAK = 0x00000020,
    MENUBREAK = 0x00000040,
    OWNERDRAW = 0x00000100,
    RADIOCHECK = 0x00000200,
    SEPARATOR = 0x00000800,
    RIGHTORDER = 0x00002000,
    RIGHTJUSTIFY = 0x00004000
}

internal enum MFS : uint
{
    ENABLED = 0x00000000,
    UNCHECKED = 0x00000000,
    UNHILITE = 0x00000000,
    GRAYED = 0x00000003,
    DISABLED = 0x00000003,
    CHECKED = 0x00000008,
    HILITE = 0x00000080,
    DEFAULT = 0x00001000
}

internal class Import
{
    [DllImport("ole32.dll", CharSet = CharSet.Unicode)]
    public static extern void ReleaseStgMedium(ref STGMEDIUM pmedium);

    [DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool InsertMenuItem(
        IntPtr hMenu,
        uint uItem,
        [MarshalAs(UnmanagedType.Bool)]bool fByPosition,
        ref MENUITEMINFO mii);

    [DllImport("user32.dll")]
    public static extern IntPtr CreatePopupMenu();

    [DllImport("shell32", CharSet = CharSet.Unicode)]
    public static extern uint DragQueryFile(
        IntPtr hDrop,
        uint iFile,
        StringBuilder pszFile,
        int cch);

    public static int HighWord(int number)
    {
        return ((number & 0x80000000) == 0x80000000) ?
            (number >> 16) : ((number >> 16) & 0xffff);
    }

    public static int LowWord(int number)
    {
        return number & 0xffff;
    }
}

internal static class ErrorCode
{
    public const int S_OK = 0x0000;
    public const int S_FALSE = 0x0001;
    public const int E_FAIL = -2147467259;
    public const int E_INVALIDARG = -2147024809;
    public const int E_OUTOFMEMORY = -2147024882;
    public const int STRSAFE_E_INSUFFICIENT_BUFFER = -2147024774;

    public const uint SEVERITY_SUCCESS = 0;
    public const uint SEVERITY_ERROR = 1;

    public static int MAKE_HRESULT(uint sev, uint fac, uint code)
    {
        return (int)((sev << 31) | (fac << 16) | code);
    }
}