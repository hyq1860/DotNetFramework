using System;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Collections;
using System.Text;

using InteropConsts;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;

namespace InteropInterfaces
{
	#region IInternetSecurityManager Interface

	[ComVisible(true), ComImport,
	GuidAttribute("79EAC9EE-BAF9-11CE-8C82-00AA004BA90B"),
	InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IInternetSecurityManager
	{
		[return: MarshalAs(UnmanagedType.I4)]
		[PreserveSig]
		int SetSecuritySite(
			[In] IntPtr pSite);

		[return: MarshalAs(UnmanagedType.I4)]
		[PreserveSig]
		int GetSecuritySite(
			out IntPtr pSite);

		[return: MarshalAs(UnmanagedType.I4)]
		[PreserveSig]
		int MapUrlToZone(
			[In, MarshalAs(UnmanagedType.LPWStr)] string pwszUrl,
			out UInt32 pdwZone,
			[In] UInt32 dwFlags);

		[return: MarshalAs(UnmanagedType.I4)]
		[PreserveSig]
		int GetSecurityId(
			[In, MarshalAs(UnmanagedType.LPWStr)] string pwszUrl,
			[Out] IntPtr pbSecurityId, [In, Out] ref UInt32 pcbSecurityId,
			[In] ref UInt32 dwReserved);

		[return: MarshalAs(UnmanagedType.I4)]
		[PreserveSig]
		int ProcessUrlAction(
			[In, MarshalAs(UnmanagedType.LPWStr)] string pwszUrl,
			UInt32 dwAction,
			IntPtr pPolicy, UInt32 cbPolicy,
			IntPtr pContext, UInt32 cbContext,
			UInt32 dwFlags,
			UInt32 dwReserved);

		[return: MarshalAs(UnmanagedType.I4)]
		[PreserveSig]
		int QueryCustomPolicy(
			[In, MarshalAs(UnmanagedType.LPWStr)] string pwszUrl,
			ref Guid guidKey,
			out IntPtr ppPolicy, out UInt32 pcbPolicy,
			IntPtr pContext, UInt32 cbContext,
			UInt32 dwReserved);

		[return: MarshalAs(UnmanagedType.I4)]
		[PreserveSig]
		int SetZoneMapping(
			UInt32 dwZone,
			[In, MarshalAs(UnmanagedType.LPWStr)] string lpszPattern,
			UInt32 dwFlags);

		[return: MarshalAs(UnmanagedType.I4)]
		[PreserveSig]
		int GetZoneMappings(
			[In] UInt32 dwZone,
			out System.Runtime.InteropServices.ComTypes.IEnumString ppenumString,
			[In] UInt32 dwFlags);
	}

	#endregion
	
	#region IOleObject Interface



	#endregion




	#region IServiceProvider Interface

    [ComImport, ComVisible(true)]
    [Guid("6d5140c1-7436-11ce-8034-00aa006009fa")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IServiceProvider
    {
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int QueryService(
            [In] ref Guid guidService,
            [In] ref Guid riid,
            [Out] out IntPtr ppvObject);
    }

	#endregion


    #region IHostDialogHelper Interface

    [ComImport, ComVisible(true)]
    [Guid("53DEC138-A51E-11d2-861E-00C04FA35C89")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IHostDialogHelper
    {
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int ShowHTMLDialog(
            [In] IntPtr hwndParent,
            [In, MarshalAs(UnmanagedType.Interface)] System.Runtime.InteropServices.ComTypes.IMoniker pMk,
            [In] ref object pvarArgIn,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pchOptions,
            [In, Out] ref object pvarArgOut,
            [In, MarshalAs(UnmanagedType.IUnknown)] object punkHost);
    }

    #endregion
}
