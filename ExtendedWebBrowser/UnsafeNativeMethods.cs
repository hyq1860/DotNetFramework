using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using mshtml;

namespace ExtendedWebBrowser2
{
    class UnsafeNativeMethods
    {
        private UnsafeNativeMethods()
        { 

        }

        [ComImport, TypeLibType((short)0x1010), InterfaceType((short)2), Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D")]
        public interface DWebBrowserEvents2
        {
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x66)]
          void StatusTextChange([In, MarshalAs(UnmanagedType.BStr)] string Text);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x6c)]
          void ProgressChange([In] int Progress, [In] int ProgressMax);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x69)]
          void CommandStateChange([In] int Command, [In] bool Enable);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x6a)]
          void DownloadBegin();
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x68)]
          void DownloadComplete();
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x71)]
          void TitleChange([In, MarshalAs(UnmanagedType.BStr)] string Text);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x70)]
          void PropertyChange([In, MarshalAs(UnmanagedType.BStr)] string szProperty);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(250)]
          void BeforeNavigate2([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In, MarshalAs(UnmanagedType.Struct)] ref object URL, [In, MarshalAs(UnmanagedType.Struct)] ref object Flags, [In, MarshalAs(UnmanagedType.Struct)] ref object TargetFrameName, [In, MarshalAs(UnmanagedType.Struct)] ref object PostData, [In, MarshalAs(UnmanagedType.Struct)] ref object Headers, [In, Out] ref bool Cancel);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0xfb)]
          void NewWindow2([In, Out, MarshalAs(UnmanagedType.IDispatch)] ref object ppDisp, [In, Out] ref bool Cancel);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0xfc)]
          void NavigateComplete2([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In, MarshalAs(UnmanagedType.Struct)] ref object URL);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x103)]
          void DocumentComplete([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In, MarshalAs(UnmanagedType.Struct)] ref object URL);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0xfd)]
          void OnQuit();
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0xfe)]
          void OnVisible([In] bool Visible);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0xff)]
          void OnToolBar([In] bool ToolBar);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x100)]
          void OnMenuBar([In] bool MenuBar);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x101)]
          void OnStatusBar([In] bool StatusBar);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x102)]
          void OnFullScreen([In] bool FullScreen);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(260)]
          void OnTheaterMode([In] bool TheaterMode);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x106)]
          void WindowSetResizable([In] bool Resizable);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x108)]
          void WindowSetLeft([In] int Left);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x109)]
          void WindowSetTop([In] int Top);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x10a)]
          void WindowSetWidth([In] int Width);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x10b)]
          void WindowSetHeight([In] int Height);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x107)]
          void WindowClosing([In] bool IsChildWindow, [In, Out] ref bool Cancel);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x10c)]
          void ClientToHostWindow([In, Out] ref int CX, [In, Out] ref int CY);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x10d)]
          void SetSecureLockIcon([In] int SecureLockIcon);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(270)]
          void FileDownload([In, Out] ref bool Cancel);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x10f)]
          void NavigateError([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In, MarshalAs(UnmanagedType.Struct)] ref object URL, [In, MarshalAs(UnmanagedType.Struct)] ref object Frame, [In, MarshalAs(UnmanagedType.Struct)] ref object StatusCode, [In, Out] ref bool Cancel);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0xe1)]
          void PrintTemplateInstantiation([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0xe2)]
          void PrintTemplateTeardown([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0xe3)]
          void UpdatePageStatus([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In, MarshalAs(UnmanagedType.Struct)] ref object nPage, [In, MarshalAs(UnmanagedType.Struct)] ref object fDone);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x110)]
          void PrivacyImpactedStateChange([In] bool bImpacted);
          [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x111)]
          void NewWindow3([In, Out, MarshalAs(UnmanagedType.IDispatch)] ref object ppDisp, [In, Out] ref bool Cancel, [In] uint dwFlags, [In, MarshalAs(UnmanagedType.BStr)] string bstrUrlContext, [In, MarshalAs(UnmanagedType.BStr)] string bstrUrl);
        }

        [ComImport, SuppressUnmanagedCodeSecurity, TypeLibType(TypeLibTypeFlags.FOleAutomation | (TypeLibTypeFlags.FDual | TypeLibTypeFlags.FHidden)), Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E")]
        public interface IWebBrowser2
        {
          [DispId(100)]
          void GoBack();
          [DispId(0x65)]
          void GoForward();
          [DispId(0x66)]
          void GoHome();
          [DispId(0x67)]
          void GoSearch();
          [DispId(0x68)]
          void Navigate([In] string Url, [In] ref object flags, [In] ref object targetFrameName, [In] ref object postData, [In] ref object headers);
          [DispId(-550)]
          void Refresh();
          [DispId(0x69)]
          void Refresh2([In] ref object level);
          [DispId(0x6a)]
          void Stop();
          [DispId(200)]
          object Application { [return: MarshalAs(UnmanagedType.IDispatch)] get; }
          [DispId(0xc9)]
          object Parent { [return: MarshalAs(UnmanagedType.IDispatch)] get; }
          [DispId(0xca)]
          object Container { [return: MarshalAs(UnmanagedType.IDispatch)] get; }
          [DispId(0xcb)]
          object Document { [return: MarshalAs(UnmanagedType.IDispatch)] get; }
          [DispId(0xcc)]
          bool TopLevelContainer { get; }
          [DispId(0xcd)]
          string Type { get; }
          [DispId(0xce)]
          int Left { get; set; }
          [DispId(0xcf)]
          int Top { get; set; }
          [DispId(0xd0)]
          int Width { get; set; }
          [DispId(0xd1)]
          int Height { get; set; }
          [DispId(210)]
          string LocationName { get; }
          [DispId(0xd3)]
          string LocationURL { get; }
          [DispId(0xd4)]
          bool Busy { get; }
          [DispId(300)]
          void Quit();
          [DispId(0x12d)]
          void ClientToWindow(out int pcx, out int pcy);
          [DispId(0x12e)]
          void PutProperty([In] string property, [In] object vtValue);
          [DispId(0x12f)]
          object GetProperty([In] string property);
          [DispId(0)]
          string Name { get; }
          [DispId(-515)]
          int HWND { get; }
          [DispId(400)]
          string FullName { get; }
          [DispId(0x191)]
          string Path { get; }
          [DispId(0x192)]
          bool Visible { get; set; }
          [DispId(0x193)]
          bool StatusBar { get; set; }
          [DispId(0x194)]
          string StatusText { get; set; }
          [DispId(0x195)]
          int ToolBar { get; set; }
          [DispId(0x196)]
          bool MenuBar { get; set; }
          [DispId(0x197)]
          bool FullScreen { get; set; }
          [DispId(500)]
          void Navigate2([In] ref object URL, [In] ref object flags, [In] ref object targetFrameName, [In] ref object postData, [In] ref object headers);
          [DispId(0x1f5)]
          NativeMethods.OLECMDF QueryStatusWB([In] NativeMethods.OLECMDID cmdID);
          [DispId(0x1f6)]
          void ExecWB([In] NativeMethods.OLECMDID cmdID, [In] NativeMethods.OLECMDEXECOPT cmdexecopt, ref object pvaIn, IntPtr pvaOut);
          [DispId(0x1f7)]
          void ShowBrowserBar([In] ref object pvaClsid, [In] ref object pvarShow, [In] ref object pvarSize);
          [DispId(-525)]
          WebBrowserReadyState ReadyState { get; }
          [DispId(550)]
          bool Offline { get; set; }
          [DispId(0x227)]
          bool Silent { get; set; }
          [DispId(0x228)]
          bool RegisterAsBrowser { get; set; }
          [DispId(0x229)]
          bool RegisterAsDropTarget { get; set; }
          [DispId(0x22a)]
          bool TheaterMode { get; set; }
          [DispId(0x22b)]
          bool AddressBar { get; set; }
          [DispId(0x22c)]
          bool Resizable { get; set; }

          [DispId(271)]
          void NavigateError(
              [In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
              [In] ref object URL, [In] ref object frame,
              [In] ref object statusCode, [In, Out] ref bool cancel);
        }

        // Interop definition for IOleCommandTarget. 
        [ComImport(), ComVisible(true),
        Guid("B722BCCB-4E68-101B-A2BC-00AA00404770"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleCommandTarget
        {

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int QueryStatus(
                [In] IntPtr pguidCmdGroup,
                [In, MarshalAs(UnmanagedType.U4)] uint cCmds,
                [In, Out, MarshalAs(UnmanagedType.Struct)] ref tagOLECMD prgCmds,
                //This parameter must be IntPtr, as it can be null
                [In, Out] IntPtr pCmdText);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int Exec(
                //[In] ref Guid pguidCmdGroup,
                //have to be IntPtr, since null values are unacceptable
                //and null is used as default group!
                [In] IntPtr pguidCmdGroup,
                [In, MarshalAs(UnmanagedType.U4)] uint nCmdID,
                [In, MarshalAs(UnmanagedType.U4)] uint nCmdexecopt,
                [In] IntPtr pvaIn,
                [In, Out] IntPtr pvaOut);
        }

        [ComVisible(true),InterfaceType(ComInterfaceType.InterfaceIsIUnknown),Guid("FC4801A3-2BA9-11CF-A229-00AA003D7352")]
        public interface IObjectWithSite
        {
            [PreserveSig]
            int SetSite([MarshalAs(UnmanagedType.IUnknown)]object site);

            [PreserveSig]
            int GetSite(ref Guid guid, out IntPtr ppvSite);
        }

        [ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown),GuidAttribute("3050f3f0-98b5-11cf-bb82-00aa00bdce0b")]
        public interface ICustomDoc
        {
        }

        [ComImport,Guid("00000112-0000-0000-C000-000000000046"),InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleObject
        {
            void SetClientSite(IOleClientSite pClientSite);
            void GetClientSite(IOleClientSite ppClientSite);
            void SetHostNames(object szContainerApp, object szContainerObj);
            void Close(uint dwSaveOption);
            void SetMoniker(uint dwWhichMoniker, object pmk);
            void GetMoniker(uint dwAssign, uint dwWhichMoniker, object ppmk);
            void InitFromData(IDataObject pDataObject, bool fCreation, uint dwReserved);
            void GetClipboardData(uint dwReserved, IDataObject ppDataObject);
            void DoVerb(uint iVerb, uint lpmsg, object pActiveSite,uint lindex, uint hwndParent, uint lprcPosRect);
            void EnumVerbs(object ppEnumOleVerb);
            void Update();
            void IsUpToDate();
            void GetUserClassID(uint pClsid);
            void GetUserType(uint dwFormOfType, uint pszUserType);
            void SetExtent(uint dwDrawAspect, uint psizel);
            void GetExtent(uint dwDrawAspect, uint psizel);
            void Advise(object pAdvSink, uint pdwConnection);
            void Unadvise(uint dwConnection);
            void EnumAdvise(object ppenumAdvise);
            void GetMiscStatus(uint dwAspect, uint pdwStatus);
            void SetColorScheme(object pLogpal);
        };

        [ComImport,Guid("00000118-0000-0000-C000-000000000046"),InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleClientSite
        {
            void SaveObject();
            void GetMoniker(uint dwAssign, uint dwWhichMoniker, object ppmk);
            void GetContainer(object ppContainer);
            void ShowObject();
            void OnShowWindow(bool fShow);
            void RequestNewObjectLayout();
        }

        [ComImport,Guid("b722bcc7-4e68-101b-a2bc-00aa00404770"),InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleDocumentSite
        {
            void ActivateMe(ref object pViewToActivate);
        }

        [ComImport,Guid("C4D244B0-D43E-11CF-893B-00AA00BDCE1A"),InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDocHostShowUI
        {
            //     HRESULT ShowMessage(
            //            [in] HWND hwnd,
            //            [in] LPOLESTR lpstrText,
            //            [in] LPOLESTR lpstrCaption,
            //            [in] DWORD dwType,
            //            [in] LPOLESTR lpstrHelpFile,
            //            [in] DWORD dwHelpContext,
            //            [out] LRESULT * plResult);
            //    HRESULT ShowHelp(
            //            [in] HWND hwnd,
            //            [in] LPOLESTR pszHelpFile,
            //            [in] UINT uCommand,
            //            [in] DWORD dwData,
            //            [in] POINT ptMouse,
            //            [out] IDispatch * pDispatchObjectHit);

            [PreserveSig]
            uint ShowMessage(IntPtr hwnd,
              [MarshalAs(UnmanagedType.BStr)] string lpstrText,
              [MarshalAs(UnmanagedType.BStr)] string lpstrCaption,
              uint dwType,
              [MarshalAs(UnmanagedType.BStr)] string lpstrHelpFile,
              uint dwHelpContext,
              out int lpResult);

            [PreserveSig]
            uint ShowHelp(IntPtr hwnd, [MarshalAs(UnmanagedType.BStr)] string pszHelpFile,
              uint uCommand, uint dwData,
              tagPOINT ptMouse,
              [MarshalAs(UnmanagedType.IDispatch)] object pDispatchObjectHit);
        }

        #region IHtmlWindow2 Interface

        [ComVisible(true), ComImport()]
        [TypeLibType((short)4160)] //TypeLibTypeFlags.FDispatchable
        [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch)]
        [Guid("332c4427-26cb-11d0-b483-00c04fd90119")]
        public interface IHTMLWindow2
        {
            [DispId(HTMLDispIDs.DISPID_IHTMLFRAMESCOLLECTION2_ITEM)]
            object item([In] object pvarIndex);

            [DispId(HTMLDispIDs.DISPID_IHTMLFRAMESCOLLECTION2_LENGTH)]
            int length { get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_FRAMES)]
            IHTMLFramesCollection2 frames { [return: MarshalAs(UnmanagedType.Interface)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_DEFAULTSTATUS)]
            string defaultStatus { set; [return: MarshalAs(UnmanagedType.BStr)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_STATUS)]
            string status { set; [return: MarshalAs(UnmanagedType.BStr)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_SETTIMEOUT)]
            int setTimeout([In] string expression, [In] int msec, [In] ref object language);

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_CLEARTIMEOUT)]
            void clearTimeout([In] int timerID);

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_ALERT)]
            void alert([In, MarshalAs(UnmanagedType.BStr)] string message); //default value ""

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_CONFIRM)]
            bool confirm([In, MarshalAs(UnmanagedType.BStr)] string message);

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_PROMPT)]
            //default for message = ""
            //default for defstr = "undefined"
            object prompt([In, MarshalAs(UnmanagedType.BStr)] string message,
                [In, MarshalAs(UnmanagedType.BStr)] string defstr);

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_IMAGE)]
            object Image { get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_LOCATION)]
            object location { get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_HISTORY)]
            object history { get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_CLOSE)]
            void close();

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_OPENER)]
            object opener { set; get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_NAVIGATOR)]
            IOmNavigator navigator { get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_NAME)]
            string name { set; get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_PARENT)]
            IHTMLWindow2 parent { [return: MarshalAs(UnmanagedType.Interface)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_OPEN)]
            IHTMLWindow2 open([In] string url, [In] string name, [In] string features, [In, MarshalAs(UnmanagedType.VariantBool)] bool replace);

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_SELF)]
            IHTMLWindow2 self { [return: MarshalAs(UnmanagedType.Interface)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_TOP)]
            IHTMLWindow2 top { [return: MarshalAs(UnmanagedType.Interface)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_WINDOW)]
            IHTMLWindow2 window { [return: MarshalAs(UnmanagedType.Interface)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_NAVIGATE)]
            void navigate([In, MarshalAs(UnmanagedType.BStr)] string url);

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_ONFOCUS)]
            object onfocus { set; get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_ONBLUR)]
            object onblur { set; get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_ONLOAD)]
            object onload { set; get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_ONBEFOREUNLOAD)]
            object onbeforeunload { set; get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_ONUNLOAD)]
            object onunload { set; get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_ONHELP)]
            object onhelp { set; get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_ONERROR)]
            object onerror { set; get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_ONRESIZE)]
            object onresize { set; get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_ONSCROLL)]
            object onscroll { set; get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_DOCUMENT)]
            IHTMLDocument2 document { [return: MarshalAs(UnmanagedType.Interface)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_EVENT)]
            IHTMLEventObj eventobj { [return: MarshalAs(UnmanagedType.Interface)] get; } //event

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2__NEWENUM)]
            object _newEnum { [return: MarshalAs(UnmanagedType.IUnknown)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_SHOWMODALDIALOG)]
            object showModalDialog([In, MarshalAs(UnmanagedType.BStr)] string dialog,
                [In] ref object varArgIn, [In] ref object varOptions);

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_SHOWHELP)]
            void showHelp([In, MarshalAs(UnmanagedType.BStr)] string helpURL,
                [In] object helpArg,
                [In, MarshalAs(UnmanagedType.BStr)] string features);

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_SCREEN)]
            object screen { get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_OPTION)]
            object Option { get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_FOCUS)]
            void focus();

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_CLOSED)]
            bool closed { get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_BLUR)]
            void blur();

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_SCROLL)]
            void scroll([In] int x, [In] int y);

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_CLIENTINFORMATION)]
            object clientInformation { get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_SETINTERVAL)]
            int setInterval([In] string expression, [In] int msec, [In] ref object language);

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_CLEARINTERVAL)]
            void clearInterval([In] int timerID);

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_OFFSCREENBUFFERING)]
            object offscreenBuffering { set; get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_EXECSCRIPT)]
            object execScript([In] string code, [In] string language); //default language JScript

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_TOSTRING)]
            string toString();

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_SCROLLBY)]
            void scrollBy([In] int x, [In] int y);

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_SCROLLTO)]
            void scrollTo([In] int x, [In] int y);

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_MOVETO)]
            void moveTo([In] int x, [In] int y);

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_MOVEBY)]
            void moveBy([In] int x, [In] int y);

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_RESIZETO)]
            void resizeTo([In] int x, [In] int y);

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_RESIZEBY)]
            void resizeBy([In] int x, [In] int y);

            [DispId(HTMLDispIDs.DISPID_IHTMLWINDOW2_EXTERNAL)]
            object external { [return: MarshalAs(UnmanagedType.IDispatch)] get; }

        }

        #region IHTMLEventObj2 Interface
        [ComVisible(true), ComImport()]
        [TypeLibType((short)4160)] //TypeLibTypeFlags.FDispatchable
        [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch)]
        [Guid("3050f48B-98b5-11cf-bb82-00aa00bdce0b")]
        public interface IHTMLEventObj2
        {
            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_SETATTRIBUTE)]
            void setAttribute([MarshalAs(UnmanagedType.BStr)] string strAttributeName, object AttributeValue, int lFlags);

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_GETATTRIBUTE)]
            object getAttribute([MarshalAs(UnmanagedType.BStr)] string strAttributeName, int lFlags);

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_REMOVEATTRIBUTE)]
            [return: MarshalAs(UnmanagedType.VariantBool)]
            bool removeAttribute([MarshalAs(UnmanagedType.BStr)] string strAttributeName, int lFlags);

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_PROPERTYNAME)]
            string propertyName { set; [return: MarshalAs(UnmanagedType.BStr)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_BOOKMARKS)]
            object bookmarks { set; [return: MarshalAs(UnmanagedType.Interface)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_RECORDSET)]
            object recordset { set; [return: MarshalAs(UnmanagedType.Interface)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_DATAFLD)]
            string dataFld { set; [return: MarshalAs(UnmanagedType.BStr)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_BOUNDELEMENTS)]
            object boundElements { set; [return: MarshalAs(UnmanagedType.Interface)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_REPEAT)]
            bool repeat { set; [return: MarshalAs(UnmanagedType.VariantBool)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_SRCURN)]
            string srcUrn { set; [return: MarshalAs(UnmanagedType.BStr)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_SRCELEMENT)]
            IHTMLElement SrcElement { set; [return: MarshalAs(UnmanagedType.Interface)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_ALTKEY)]
            bool AltKey { set; get; }
            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_CTRLKEY)]
            bool CtrlKey { set; get; }
            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_SHIFTKEY)]
            bool ShiftKey { set; get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_FROMELEMENT)]
            IHTMLElement FromElement { set; [return: MarshalAs(UnmanagedType.Interface)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_TOELEMENT)]
            IHTMLElement ToElement { set; [return: MarshalAs(UnmanagedType.Interface)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_BUTTON)]
            int Button { set; get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_TYPE)]
            string EventType { set; [return: MarshalAs(UnmanagedType.BStr)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_QUALIFIER)]
            string Qualifier { set; [return: MarshalAs(UnmanagedType.BStr)] get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_REASON)]
            int reason { get; set; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_X)]
            int x { get; set; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_Y)]
            int y { get; set; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_CLIENTX)]
            int ClientX { get; set; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_CLIENTY)]
            int clientY { set; get; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_OFFSETX)]
            int offsetX { get; set; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_OFFSETY)]
            int offsetY { get; set; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_SCREENX)]
            int screenX { get; set; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_SCREENY)]
            int screenY { get; set; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_SRCFILTER)]
            object srcFilter { [return: MarshalAs(UnmanagedType.IDispatch)] get; set; }

            [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ2_DATATRANSFER)]
            object dataTransfer { [return: MarshalAs(UnmanagedType.IDispatch)] get; }
        }

        #endregion
        #endregion
    }

    #region
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OLECMDTEXT
    {
        public uint cmdtextf;
        public uint cwActual;
        public uint cwBuf;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        public char rgwz;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct OLECMD
    {
        public uint cmdID;
        public uint cmdf;
    }

    [ComVisible(true), StructLayout(LayoutKind.Sequential)]
    public struct tagOLECMD
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint cmdID;
        [MarshalAs(UnmanagedType.U4)]
        public uint cmdf;
    }
    #endregion
}
