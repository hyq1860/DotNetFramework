using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ExtendedWebBrowser2
{
  static class NativeMethods
  {
    public enum OLECMDF
    {
      // Fields
      OLECMDF_DEFHIDEONCTXTMENU = 0x20,
      OLECMDF_ENABLED = 2,
      OLECMDF_INVISIBLE = 0x10,
      OLECMDF_LATCHED = 4,
      OLECMDF_NINCHED = 8,
      OLECMDF_SUPPORTED = 1
    }

    public enum OLECMDID
    {
        OLECMDID_OPEN = 1,
        OLECMDID_NEW = 2,
        OLECMDID_SAVE = 3,
        OLECMDID_SAVEAS = 4,
        OLECMDID_SAVECOPYAS = 5,
        OLECMDID_PRINT = 6,
        OLECMDID_PRINTPREVIEW = 7,
        OLECMDID_PAGESETUP = 8,
        OLECMDID_SPELL = 9,
        OLECMDID_PROPERTIES = 10,
        OLECMDID_CUT = 11,
        OLECMDID_COPY = 12,
        OLECMDID_PASTE = 13,
        OLECMDID_PASTESPECIAL = 14,
        OLECMDID_UNDO = 15,
        OLECMDID_REDO = 16,
        OLECMDID_SELECTALL = 17,
        OLECMDID_CLEARSELECTION = 18,
        OLECMDID_ZOOM = 19,
        OLECMDID_GETZOOMRANGE = 20,
        OLECMDID_UPDATECOMMANDS = 21,
        OLECMDID_REFRESH = 22,
        OLECMDID_STOP = 23,
        OLECMDID_HIDETOOLBARS = 24,
        OLECMDID_SETPROGRESSMAX = 25,
        OLECMDID_SETPROGRESSPOS = 26,
        OLECMDID_SETPROGRESSTEXT = 27,
        OLECMDID_SETTITLE = 28,
        OLECMDID_SETDOWNLOADSTATE = 29,
        OLECMDID_STOPDOWNLOAD = 30,
        OLECMDID_ONTOOLBARACTIVATED = 31,
        OLECMDID_FIND = 32,
        OLECMDID_DELETE = 33,
        OLECMDID_HTTPEQUIV = 34,
        OLECMDID_HTTPEQUIV_DONE = 35,
        OLECMDID_ENABLE_INTERACTION = 36,
        OLECMDID_ONUNLOAD = 37,
        OLECMDID_PROPERTYBAG2 = 38,
        OLECMDID_PREREFRESH = 39,
        OLECMDID_SHOWSCRIPTERROR = 40,
        OLECMDID_SHOWMESSAGE = 41,
        OLECMDID_SHOWFIND = 42,
        OLECMDID_SHOWPAGESETUP = 43,
        OLECMDID_SHOWPRINT = 44,
        OLECMDID_CLOSE = 45,
        OLECMDID_ALLOWUILESSSAVEAS = 46,
        OLECMDID_DONTDOWNLOADCSS = 47,
        OLECMDID_UPDATEPAGESTATUS = 48,
        OLECMDID_PRINT2 = 49,
        OLECMDID_PRINTPREVIEW2 = 50,
        OLECMDID_SETPRINTTEMPLATE = 51,
        OLECMDID_GETPRINTTEMPLATE = 52,
        OLECMDID_PAGEACTIONBLOCKED = 55,
        OLECMDID_PAGEACTIONUIQUERY = 56,
        OLECMDID_FOCUSVIEWCONTROLS = 57,
        OLECMDID_FOCUSVIEWCONTROLSQUERY = 58,
        OLECMDID_SHOWPAGEACTIONMENU = 59,
        OLECMDID_ADDTRAVELENTRY = 60,
        OLECMDID_UPDATETRAVELENTRY = 61,
        OLECMDID_UPDATEBACKFORWARDSTATE = 62,
        OLECMDID_OPTICAL_ZOOM = 63,
        OLECMDID_OPTICAL_GETZOOMRANGE = 64,
        OLECMDID_WINDOWSTATECHANGED = 65,
        //OLECMDID_IE7_SHOWSCRIPTERROR = 69
    }

    public enum OLECMDEXECOPT
    {
      // Fields
      OLECMDEXECOPT_DODEFAULT = 0,
      OLECMDEXECOPT_DONTPROMPTUSER = 2,
      OLECMDEXECOPT_PROMPTUSER = 1,
      OLECMDEXECOPT_SHOWHELP = 3
    }

    //[StructLayout(LayoutKind.Sequential)]
    //public class POINT
    //{
    //  public int x;
    //  public int y;
    //  public POINT() { }
    //  public POINT(int x, int y)
    //  {
    //    this.x = x;
    //    this.y = y;
    //  }
    //}

    //[ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("B722BCCB-4E68-101B-A2BC-00AA00404770"), ComVisible(true)]
    //public interface IOleCommandTarget
    //{
    //  [return: MarshalAs(UnmanagedType.I4)]
    //  [PreserveSig]
    //  int QueryStatus(ref Guid pguidCmdGroup, int cCmds, [In, Out] NativeMethods.OLECMD prgCmds, [In, Out] IntPtr pCmdText);
    //  [return: MarshalAs(UnmanagedType.I4)]
    //  [PreserveSig]
    //  int Exec(ref Guid pguidCmdGroup, int nCmdID, int nCmdexecopt, [In, MarshalAs(UnmanagedType.LPArray)] object[] pvaIn, ref int pvaOut);
    //}

    //[StructLayout(LayoutKind.Sequential)]
    //public class OLECMD
    //{
    //  [MarshalAs(UnmanagedType.U4)]
    //  public int cmdID;
    //  [MarshalAs(UnmanagedType.U4)]
    //  public int cmdf;
    //  public OLECMD()
    //  {
    //  }
    //}

    //public const int S_FALSE = 1;
    //public const int S_OK = 0;

 
  }
}
