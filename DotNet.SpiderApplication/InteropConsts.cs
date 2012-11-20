using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace InteropConsts
{
	public sealed class HResults
	{
		public const int NOERROR		= 0;
		public const int S_OK			= 0;
		public const int S_FALSE		= 1;
		public const int E_PENDING		= unchecked((int)0x8000000A);
		public const int E_HANDLE		= unchecked((int)0x80070006);
		public const int E_NOTIMPL		= unchecked((int)0x80004001);
		public const int E_NOINTERFACE	= unchecked((int)0x80004002);
		public const int E_POINTER		= unchecked((int)0x80004003);
		public const int E_ABORT		= unchecked((int)0x80004004);
		public const int E_FAIL			= unchecked((int)0x80004005);
		public const int E_OUTOFMEMORY	= unchecked((int)0x8007000E);
		public const int E_ACCESSDENIED	= unchecked((int)0x80070005);
		public const int E_UNEXPECTED	= unchecked((int)0x8000FFFF);
		public const int E_FLAGS		= unchecked((int)0x1000);
		public const int E_INVALIDARG	= unchecked((int)0x80070057);
	}

	public sealed class IID_CLSIDS
	{
		public static Guid IID_IInternetHostSecurityManager = new Guid("3af280b6-cb3f-11d0-891e-00c04fb6bfc4");
		public static Guid IID_IInternetSecurityManager     = new Guid("79EAC9EE-BAF9-11CE-8C82-00AA004BA90B");
		public static Guid IID_IInternetSecurityManagerEx   = new Guid("F164EDF1-CC7C-4f0d-9A94-34222625C393");
		public static Guid IID_IPropertyBag                 = new Guid("55272A00-42CB-11CE-8135-00AA004BB851");
		public static Guid IID_IUnknown                     = new Guid("00000000-0000-0000-C000-000000000046");
	}

	public enum WinInetErrors : int
	{
		HTTP_STATUS_CONTINUE = 100, //The request can be continued.
		HTTP_STATUS_SWITCH_PROTOCOLS = 101, //The server has switched protocols in an upgrade header.
		HTTP_STATUS_OK = 200, //The request completed successfully.
		HTTP_STATUS_CREATED = 201, //The request has been fulfilled and resulted in the creation of a new resource.
		HTTP_STATUS_ACCEPTED = 202, //The request has been accepted for processing, but the processing has not been completed.
		HTTP_STATUS_PARTIAL = 203, //The returned meta information in the entity-header is not the definitive set available from the origin server.
		HTTP_STATUS_NO_CONTENT = 204, //The server has fulfilled the request, but there is no new information to send back.
		HTTP_STATUS_RESET_CONTENT = 205, //The request has been completed, and the client program should reset the document view that caused the request to be sent to allow the user to easily initiate another input action.
		HTTP_STATUS_PARTIAL_CONTENT = 206, //The server has fulfilled the partial GET request for the resource.
		HTTP_STATUS_AMBIGUOUS = 300, //The server couldn't decide what to return.
		HTTP_STATUS_MOVED = 301, //The requested resource has been assigned to a new permanent URI (Uniform Resource Identifier), and any future references to this resource should be done using one of the returned URIs.
		HTTP_STATUS_REDIRECT = 302, //The requested resource resides temporarily under a different URI (Uniform Resource Identifier).
		HTTP_STATUS_REDIRECT_METHOD = 303, //The response to the request can be found under a different URI (Uniform Resource Identifier) and should be retrieved using a GET HTTP verb on that resource.
		HTTP_STATUS_NOT_MODIFIED = 304, //The requested resource has not been modified.
		HTTP_STATUS_USE_PROXY = 305, //The requested resource must be accessed through the proxy given by the location field.
		HTTP_STATUS_REDIRECT_KEEP_VERB = 307, //The redirected request keeps the same HTTP verb. HTTP/1.1 behavior.

		HTTP_STATUS_BAD_REQUEST = 400,
		HTTP_STATUS_DENIED = 401,
		HTTP_STATUS_PAYMENT_REQ = 402,
		HTTP_STATUS_FORBIDDEN = 403,
		HTTP_STATUS_NOT_FOUND = 404,
		HTTP_STATUS_BAD_METHOD = 405,
		HTTP_STATUS_NONE_ACCEPTABLE = 406,
		HTTP_STATUS_PROXY_AUTH_REQ = 407,
		HTTP_STATUS_REQUEST_TIMEOUT = 408,
		HTTP_STATUS_CONFLICT = 409,
		HTTP_STATUS_GONE = 410,
		HTTP_STATUS_LENGTH_REQUIRED = 411,
		HTTP_STATUS_PRECOND_FAILED = 412,
		HTTP_STATUS_REQUEST_TOO_LARGE = 413,
		HTTP_STATUS_URI_TOO_LONG = 414,
		HTTP_STATUS_UNSUPPORTED_MEDIA = 415,
		HTTP_STATUS_RETRY_WITH = 449,
		HTTP_STATUS_SERVER_ERROR = 500,
		HTTP_STATUS_NOT_SUPPORTED = 501,
		HTTP_STATUS_BAD_GATEWAY = 502,
		HTTP_STATUS_SERVICE_UNAVAIL = 503,
		HTTP_STATUS_GATEWAY_TIMEOUT = 504,
		HTTP_STATUS_VERSION_NOT_SUP = 505,

		ERROR_INTERNET_ASYNC_THREAD_FAILED = 12047,    //The application could not start an asynchronous thread.
		ERROR_INTERNET_BAD_AUTO_PROXY_SCRIPT = 12166,    //There was an error in the automatic proxy configuration script.
		ERROR_INTERNET_BAD_OPTION_LENGTH = 12010,    //The length of an option supplied to InternetQueryOption or InternetSetOption is incorrect for the type of option specified.
		ERROR_INTERNET_BAD_REGISTRY_PARAMETER = 12022,    //A required registry value was located but is an incorrect type or has an invalid value.
		ERROR_INTERNET_CANNOT_CONNECT = 12029,    //The attempt to connect to the server failed.
		ERROR_INTERNET_CHG_POST_IS_NON_SECURE = 12042,    //The application is posting and attempting to change multiple lines of text on a server that is not secure.
		ERROR_INTERNET_CLIENT_AUTH_CERT_NEEDED = 12044,    //The server is requesting client authentication.
		ERROR_INTERNET_CLIENT_AUTH_NOT_SETUP = 12046,    //Client authorization is not set up on this computer.
		ERROR_INTERNET_CONNECTION_ABORTED = 12030,    //The connection with the server has been terminated.
		ERROR_INTERNET_CONNECTION_RESET = 12031,    //The connection with the server has been reset.
		ERROR_INTERNET_DIALOG_PENDING = 12049,    //Another thread has a password dialog box in progress.
		ERROR_INTERNET_DISCONNECTED = 12163,    //The Internet connection has been lost.
		ERROR_INTERNET_EXTENDED_ERROR = 12003,    //An extended error was returned from the server. This is typically a string or buffer containing a verbose error message. Call InternetGetLastResponseInfo to retrieve the error text.
		ERROR_INTERNET_FAILED_DUETOSECURITYCHECK = 12171,    //The function failed due to a security check.
		ERROR_INTERNET_FORCE_RETRY = 12032,    //The function needs to redo the request.
		ERROR_INTERNET_FORTEZZA_LOGIN_NEEDED = 12054,    //The requested resource requires Fortezza authentication.
		ERROR_INTERNET_HANDLE_EXISTS = 12036,    //The request failed because the handle already exists.
		ERROR_INTERNET_HTTP_TO_HTTPS_ON_REDIR = 12039,    //The application is moving from a non-SSL to an SSL connection because of a redirect.
		ERROR_INTERNET_HTTPS_HTTP_SUBMIT_REDIR = 12052,    //The data being submitted to an SSL connection is being redirected to a non-SSL connection.
		ERROR_INTERNET_HTTPS_TO_HTTP_ON_REDIR = 12040,    //The application is moving from an SSL to an non-SSL connection because of a redirect.
		ERROR_INTERNET_INCORRECT_FORMAT = 12027,    //The format of the request is invalid.
		ERROR_INTERNET_INCORRECT_HANDLE_STATE = 12019,    //The requested operation cannot be carried out because the handle supplied is not in the correct state.
		ERROR_INTERNET_INCORRECT_HANDLE_TYPE = 12018,    //The type of handle supplied is incorrect for this operation.
		ERROR_INTERNET_INCORRECT_PASSWORD = 12014,    //The request to connect and log on to an FTP server could not be completed because the supplied password is incorrect.
		ERROR_INTERNET_INCORRECT_USER_NAME = 12013,    //The request to connect and log on to an FTP server could not be completed because the supplied user name is incorrect.
		ERROR_INTERNET_INSERT_CDROM = 12053,    //The request requires a CD-ROM to be inserted in the CD-ROM drive to locate the resource requested.
		ERROR_INTERNET_INTERNAL_ERROR = 12004,    //An internal error has occurred.
		ERROR_INTERNET_INVALID_CA = 12045,    //The function is unfamiliar with the Certificate Authority that generated the server's certificate.
		ERROR_INTERNET_INVALID_OPERATION = 12016,    //The requested operation is invalid.
		ERROR_INTERNET_INVALID_OPTION = 12009,    //A request to InternetQueryOption or InternetSetOption specified an invalid option value.
		ERROR_INTERNET_INVALID_PROXY_REQUEST = 12033,    //The request to the proxy was invalid.
		ERROR_INTERNET_INVALID_URL = 12005,    //The URL is invalid.
		ERROR_INTERNET_ITEM_NOT_FOUND = 12028,    //The requested item could not be located.
		ERROR_INTERNET_LOGIN_FAILURE = 12015,    //The request to connect and log on to an FTP server failed.
		ERROR_INTERNET_LOGIN_FAILURE_DISPLAY_ENTITY_BODY = 12174,    //The MS-Logoff digest header has been returned from the Web site. This header specifically instructs the digest package to purge credentials for the associated realm. This error will only be returned if INTERNET_ERROR_MASK_LOGIN_FAILURE_DISPLAY_ENTITY_BODY has been set.
		ERROR_INTERNET_MIXED_SECURITY = 12041,    //The content is not entirely secure. Some of the content being viewed may have come from unsecured servers.
		ERROR_INTERNET_NAME_NOT_RESOLVED = 12007,    //The server name could not be resolved.
		ERROR_INTERNET_NEED_MSN_SSPI_PKG = 12173,    //Not currently implemented.
		ERROR_INTERNET_NEED_UI = 12034,    //A user interface or other blocking operation has been requested.
		ERROR_INTERNET_NO_CALLBACK = 12025,    //An asynchronous request could not be made because a callback function has not been set.
		ERROR_INTERNET_NO_CONTEXT = 12024,    //An asynchronous request could not be made because a zero context value was supplied.
		ERROR_INTERNET_NO_DIRECT_ACCESS = 12023,    //Direct network access cannot be made at this time.
		ERROR_INTERNET_NOT_INITIALIZED = 12172,    //Initialization of the WinINet API has not occurred. Indicates that a higher-level function, such as InternetOpen, has not been called yet.
		ERROR_INTERNET_NOT_PROXY_REQUEST = 12020,    //The request cannot be made via a proxy.
		ERROR_INTERNET_OPERATION_CANCELLED = 12017,    //The operation was canceled, usually because the handle on which the request was operating was closed before the operation completed.
		ERROR_INTERNET_OPTION_NOT_SETTABLE = 12011,    //The requested option cannot be set, only queried.
		ERROR_INTERNET_OUT_OF_HANDLES = 12001,    //No more handles could be generated at this time.
		ERROR_INTERNET_POST_IS_NON_SECURE = 12043,    //The application is posting data to a server that is not secure.
		ERROR_INTERNET_PROTOCOL_NOT_FOUND = 12008,    //The requested protocol could not be located.
		ERROR_INTERNET_PROXY_SERVER_UNREACHABLE = 12165,    //The designated proxy server cannot be reached.
		ERROR_INTERNET_REDIRECT_SCHEME_CHANGE = 12048,    //The function could not handle the redirection, because the scheme changed (for example, HTTP to FTP).
		ERROR_INTERNET_REGISTRY_VALUE_NOT_FOUND = 12021,    //A required registry value could not be located.
		ERROR_INTERNET_REQUEST_PENDING = 12026,    //The required operation could not be completed because one or more requests are pending.
		ERROR_INTERNET_RETRY_DIALOG = 12050,    //The dialog box should be retried.
		ERROR_INTERNET_SEC_CERT_CN_INVALID = 12038,    //SSL certificate common name (host name field) is incorrect—for example, if you entered www.server.com and the common name on the certificate says www.different.com.
		ERROR_INTERNET_SEC_CERT_DATE_INVALID = 12037,    //SSL certificate date that was received from the server is bad. The certificate is expired.
		ERROR_INTERNET_SEC_CERT_ERRORS = 12055,    //The SSL certificate contains errors.
		ERROR_INTERNET_SEC_CERT_NO_REV = 12056,
		ERROR_INTERNET_SEC_CERT_REV_FAILED = 12057,
		ERROR_INTERNET_SEC_CERT_REVOKED = 12170,    //SSL certificate was revoked.
		ERROR_INTERNET_SEC_INVALID_CERT = 12169,    //SSL certificate is invalid.
		ERROR_INTERNET_SECURITY_CHANNEL_ERROR = 12157,    //The application experienced an internal error loading the SSL libraries.
		ERROR_INTERNET_SERVER_UNREACHABLE = 12164,    //The Web site or server indicated is unreachable.
		ERROR_INTERNET_SHUTDOWN = 12012,    //WinINet support is being shut down or unloaded.
		ERROR_INTERNET_TCPIP_NOT_INSTALLED = 12159,    //The required protocol stack is not loaded and the application cannot start WinSock.
		ERROR_INTERNET_TIMEOUT = 12002,    //The request has timed out.
		ERROR_INTERNET_UNABLE_TO_CACHE_FILE = 12158,    //The function was unable to cache the file.
		ERROR_INTERNET_UNABLE_TO_DOWNLOAD_SCRIPT = 12167,    //The automatic proxy configuration script could not be downloaded. The INTERNET_FLAG_MUST_CACHE_REQUEST flag was set.

		INET_E_INVALID_URL = unchecked((int)0x800C0002),
		INET_E_NO_SESSION = unchecked((int)0x800C0003),
		INET_E_CANNOT_CONNECT = unchecked((int)0x800C0004),
		INET_E_RESOURCE_NOT_FOUND = unchecked((int)0x800C0005),
		INET_E_OBJECT_NOT_FOUND = unchecked((int)0x800C0006),
		INET_E_DATA_NOT_AVAILABLE = unchecked((int)0x800C0007),
		INET_E_DOWNLOAD_FAILURE = unchecked((int)0x800C0008),
		INET_E_AUTHENTICATION_REQUIRED = unchecked((int)0x800C0009),
		INET_E_NO_VALID_MEDIA = unchecked((int)0x800C000A),
		INET_E_CONNECTION_TIMEOUT = unchecked((int)0x800C000B),
		INET_E_DEFAULT_ACTION = unchecked((int)0x800C0011),
		INET_E_INVALID_REQUEST = unchecked((int)0x800C000C),
		INET_E_UNKNOWN_PROTOCOL = unchecked((int)0x800C000D),
		INET_E_QUERYOPTION_UNKNOWN = unchecked((int)0x800C0013),
		INET_E_SECURITY_PROBLEM = unchecked((int)0x800C000E),
		INET_E_CANNOT_LOAD_DATA = unchecked((int)0x800C000F),
		INET_E_CANNOT_INSTANTIATE_OBJECT = unchecked((int)0x800C0010),
		INET_E_REDIRECT_FAILED = unchecked((int)0x800C0014),
		INET_E_REDIRECT_TO_DIR = unchecked((int)0x800C0015),
		INET_E_CANNOT_LOCK_REQUEST = unchecked((int)0x800C0016),
		INET_E_USE_EXTEND_BINDING = unchecked((int)0x800C0017),
		INET_E_TERMINATED_BIND = unchecked((int)0x800C0018),
		INET_E_ERROR_FIRST = unchecked((int)0x800C0002),
		INET_E_CODE_DOWNLOAD_DECLINED = unchecked((int)0x800C0100),
		INET_E_RESULT_DISPATCHED = unchecked((int)0x800C0200),
		INET_E_CANNOT_REPLACE_SFP_FILE = unchecked((int)0x800C0300),

		HTTP_COOKIE_DECLINED = 12162,    //The HTTP cookie was declined by the server.
		HTTP_COOKIE_NEEDS_CONFIRMATION = 12161,    //The HTTP cookie requires confirmation.
		HTTP_DOWNLEVEL_SERVER = 12151,    //The server did not return any headers.
		HTTP_HEADER_ALREADY_EXISTS = 12155,    //The header could not be added because it already exists.
		HTTP_HEADER_NOT_FOUND = 12150,    //The requested header could not be located.
		HTTP_INVALID_HEADER = 12153,    //The supplied header is invalid.
		HTTP_INVALID_QUERY_REQUEST = 12154,    //The request made to HttpQueryInfo is invalid.
		HTTP_INVALID_SERVER_RESPONSE = 12152,    //The server response could not be parsed.
		HTTP_NOT_REDIRECTED = 12160,    //The HTTP request was not redirected.
		HTTP_REDIRECT_FAILED = 12156,    //The redirection failed because either the scheme changed (for example, HTTP to FTP) or all attempts made to redirect failed (default is five attempts).
		HTTP_REDIRECT_NEEDS_CONFIRMATION = 12168    //The redirection requires user confirmation.
	}

	public enum URLPOLICY : uint
	{
		// Permissions 
		ALLOW = 0x00,
		QUERY = 0x01,
		DISALLOW = 0x03,

		ACTIVEX_CHECK_LIST = 0x00010000,
		CREDENTIALS_SILENT_LOGON_OK = 0x00000000,
		CREDENTIALS_MUST_PROMPT_USER = 0x00010000,
		CREDENTIALS_CONDITIONAL_PROMPT = 0x00020000,
		CREDENTIALS_ANONYMOUS_ONLY = 0x00030000,
		AUTHENTICATE_CLEARTEXT_OK = 0x00000000,
		AUTHENTICATE_CHALLENGE_RESPONSE = 0x00010000,
		AUTHENTICATE_MUTUAL_ONLY = 0x00030000,
		JAVA_PROHIBIT = 0x00000000,
		JAVA_HIGH = 0x00010000,
		JAVA_MEDIUM = 0x00020000,
		JAVA_LOW = 0x00030000,
		JAVA_CUSTOM = 0x00800000,
		CHANNEL_SOFTDIST_PROHIBIT = 0x00010000,
		CHANNEL_SOFTDIST_PRECACHE = 0x00020000,
		CHANNEL_SOFTDIST_AUTOINSTALL = 0x00030000,

		// For each action specified above the system maintains
		// a set of policies for the action. 
		// The only policies supported currently are permissions (i.e. is something allowed)
		// and logging status. 
		// IMPORTANT: If you are defining your own policies don't overload the meaning of the
		// loword of the policy. You can use the hiword to store any policy bits which are only
		// meaningful to your action.
		// For an example of how to do this look at the URLPOLICY_JAVA above

		// Notifications are not done when user already queried.
		NOTIFY_ON_ALLOW = 0x10,
		NOTIFY_ON_DISALLOW = 0x20,

		// Logging is done regardless of whether user was queried.
		LOG_ON_ALLOW = 0x40,
		LOG_ON_DISALLOW = 0x80,
		DONTCHECKDLGBOX = 0x100
	}

// migrated from urlmon.h

	public enum URLACTION : uint
	{
// The zone manager maintains policies for a set of standard actions. 
// These actions are identified by integral values (called action indexes)
// specified below.

// Minimum legal value for an action    
MIN                                          = 0x00001000,

DOWNLOAD_MIN                                 = 0x00001000,
DOWNLOAD_SIGNED_ACTIVEX                      = 0x00001001,
DOWNLOAD_UNSIGNED_ACTIVEX                    = 0x00001004,
DOWNLOAD_CURR_MAX                            = 0x00001004,
DOWNLOAD_MAX                                 = 0x000011FF,

ACTIVEX_MIN                                  = 0x00001200,
ACTIVEX_RUN                                  = 0x00001200,
URLPOLICY_ACTIVEX_CHECK_LIST                           = 0x00010000,
ACTIVEX_OVERRIDE_OBJECT_SAFETY               = 0x00001201, // aggregate next four,
ACTIVEX_OVERRIDE_DATA_SAFETY                 = 0x00001202, //
ACTIVEX_OVERRIDE_SCRIPT_SAFETY               = 0x00001203, //
SCRIPT_OVERRIDE_SAFETY                       = 0x00001401, //
ACTIVEX_CONFIRM_NOOBJECTSAFETY               = 0x00001204, //
ACTIVEX_TREATASUNTRUSTED                     = 0x00001205,
ACTIVEX_NO_WEBOC_SCRIPT                      = 0x00001206,
ACTIVEX_OVERRIDE_REPURPOSEDETECTION          = 0x00001207,
ACTIVEX_OVERRIDE_OPTIN                       = 0x00001208,
ACTIVEX_SCRIPTLET_RUN                        = 0x00001209,
ACTIVEX_DYNSRC_VIDEO_AND_ANIMATION           = 0x0000120A, //
ACTIVEX_OVERRIDE_DOMAINLIST                  = 0x0000120B,
ACTIVEX_CURR_MAX                             = 0x0000120B,
ACTIVEX_MAX                                  = 0x000013ff,

SCRIPT_MIN                                   = 0x00001400,
SCRIPT_RUN                                   = 0x00001400,
SCRIPT_JAVA_USE                              = 0x00001402,
SCRIPT_SAFE_ACTIVEX                          = 0x00001405,
CROSS_DOMAIN_DATA                            = 0x00001406,
SCRIPT_PASTE                                 = 0x00001407,
ALLOW_XDOMAIN_SUBFRAME_RESIZE                = 0x00001408,
SCRIPT_XSSFILTER                             = 0x00001409,
SCRIPT_CURR_MAX                              = 0x00001409,
SCRIPT_MAX                                   = 0x000015ff,

HTML_MIN                                     = 0x00001600,
HTML_SUBMIT_FORMS                            = 0x00001601, // aggregate next two
HTML_SUBMIT_FORMS_FROM                       = 0x00001602, //
HTML_SUBMIT_FORMS_TO                         = 0x00001603, //
HTML_FONT_DOWNLOAD                           = 0x00001604,
HTML_JAVA_RUN                                = 0x00001605, // derive from Java custom policy
HTML_USERDATA_SAVE                           = 0x00001606,
HTML_SUBFRAME_NAVIGATE                       = 0x00001607,
HTML_META_REFRESH                            = 0x00001608,
HTML_MIXED_CONTENT                           = 0x00001609,
HTML_INCLUDE_FILE_PATH                       = 0x0000160A,
HTML_MAX                                     = 0x000017ff,

SHELL_MIN                                    = 0x00001800,
SHELL_INSTALL_DTITEMS                        = 0x00001800,
SHELL_MOVE_OR_COPY                           = 0x00001802,
SHELL_FILE_DOWNLOAD                          = 0x00001803,
SHELL_VERB                                   = 0x00001804,
SHELL_WEBVIEW_VERB                           = 0x00001805,
SHELL_SHELLEXECUTE                           = 0x00001806,
//#if (_WIN32_IE >= _WIN32_IE_IE60SP2)
SHELL_EXECUTE_HIGHRISK                       = 0x00001806,
SHELL_EXECUTE_MODRISK                        = 0x00001807,
SHELL_EXECUTE_LOWRISK                        = 0x00001808,
SHELL_POPUPMGR                               = 0x00001809,
SHELL_RTF_OBJECTS_LOAD                       = 0x0000180A,
SHELL_ENHANCED_DRAGDROP_SECURITY             = 0x0000180B,
SHELL_EXTENSIONSECURITY                      = 0x0000180C,
SHELL_SECURE_DRAGSOURCE                      = 0x0000180D,
//#endif //(_WIN32_IE >= _WIN32_IE_IE60SP2)
//#if (_WIN32_IE >= _WIN32_IE_WIN7)
SHELL_REMOTEQUERY                            = 0x0000180E,
SHELL_PREVIEW                                = 0x0000180F,
//#endif //(_WIN32_IE >= _WIN32_IE_WIN7)
SHELL_CURR_MAX                               = 0x0000180F,
SHELL_MAX                                    = 0x000019ff,

NETWORK_MIN                                  = 0x00001A00,

CREDENTIALS_USE                              = 0x00001A00,
URLPOLICY_CREDENTIALS_SILENT_LOGON_OK        = 0x00000000,
URLPOLICY_CREDENTIALS_MUST_PROMPT_USER       = 0x00010000,
URLPOLICY_CREDENTIALS_CONDITIONAL_PROMPT     = 0x00020000,
URLPOLICY_CREDENTIALS_ANONYMOUS_ONLY         = 0x00030000,

AUTHENTICATE_CLIENT                          = 0x00001A01,
URLPOLICY_AUTHENTICATE_CLEARTEXT_OK          = 0x00000000,
URLPOLICY_AUTHENTICATE_CHALLENGE_RESPONSE    = 0x00010000,
URLPOLICY_AUTHENTICATE_MUTUAL_ONLY           = 0x00030000,


COOKIES                                      = 0x00001A02,
COOKIES_SESSION                              = 0x00001A03,

CLIENT_CERT_PROMPT                           = 0x00001A04,

COOKIES_THIRD_PARTY                          = 0x00001A05,
COOKIES_SESSION_THIRD_PARTY                  = 0x00001A06,

COOKIES_ENABLED                              = 0x00001A10,

NETWORK_CURR_MAX                             = 0x00001A10,
NETWORK_MAX                                  = 0x00001Bff,


JAVA_MIN                                     = 0x00001C00,
JAVA_PERMISSIONS                             = 0x00001C00,
URLPOLICY_JAVA_PROHIBIT                      = 0x00000000,
URLPOLICY_JAVA_HIGH                          = 0x00010000,
URLPOLICY_JAVA_MEDIUM                        = 0x00020000,
URLPOLICY_JAVA_LOW                           = 0x00030000,
URLPOLICY_JAVA_CUSTOM                        = 0x00800000,
JAVA_CURR_MAX                                = 0x00001C00,
JAVA_MAX                                     = 0x00001Cff,


// The following Infodelivery actions should have no default policies
// in the registry.  They assume that no default policy means fall
// back to the global restriction.  If an admin sets a policy per
// zone, then it overrides the global restriction.

INFODELIVERY_MIN                           = 0x00001D00,
INFODELIVERY_NO_ADDING_CHANNELS            = 0x00001D00,
INFODELIVERY_NO_EDITING_CHANNELS           = 0x00001D01,
INFODELIVERY_NO_REMOVING_CHANNELS          = 0x00001D02,
INFODELIVERY_NO_ADDING_SUBSCRIPTIONS       = 0x00001D03,
INFODELIVERY_NO_EDITING_SUBSCRIPTIONS      = 0x00001D04,
INFODELIVERY_NO_REMOVING_SUBSCRIPTIONS     = 0x00001D05,
INFODELIVERY_NO_CHANNEL_LOGGING            = 0x00001D06,
INFODELIVERY_CURR_MAX                      = 0x00001D06,
INFODELIVERY_MAX                           = 0x00001Dff,
CHANNEL_SOFTDIST_MIN                       = 0x00001E00,
CHANNEL_SOFTDIST_PERMISSIONS               = 0x00001E05,
URLPOLICY_CHANNEL_SOFTDIST_PROHIBIT          = 0x00010000,
URLPOLICY_CHANNEL_SOFTDIST_PRECACHE          = 0x00020000,
URLPOLICY_CHANNEL_SOFTDIST_AUTOINSTALL       = 0x00030000,
CHANNEL_SOFTDIST_MAX                       = 0x00001Eff,
//#if (_WIN32_IE >= _WIN32_IE_IE80)
DOTNET_USERCONTROLS                        = 0x00002005,
//#endif //(_WIN32_IE >= _WIN32_IE_IE80)
//#if (_WIN32_IE >= _WIN32_IE_IE60SP2)
BEHAVIOR_MIN                               = 0x00002000,
BEHAVIOR_RUN                               = 0x00002000,
URLPOLICY_BEHAVIOR_CHECK_LIST                        = 0x00010000,

// The following actions correspond to the Feature options above.
// However, they are NOT in the same order.
FEATURE_MIN                                = 0x00002100,
FEATURE_MIME_SNIFFING                      = 0x00002100,
FEATURE_ZONE_ELEVATION                     = 0x00002101,
FEATURE_WINDOW_RESTRICTIONS                = 0x00002102,
FEATURE_SCRIPT_STATUS_BAR                  = 0x00002103,
FEATURE_FORCE_ADDR_AND_STATUS              = 0x00002104,
FEATURE_BLOCK_INPUT_PROMPTS                = 0x00002105,
FEATURE_DATA_BINDING                       = 0x00002106,

AUTOMATIC_DOWNLOAD_UI_MIN                  = 0x00002200,
AUTOMATIC_DOWNLOAD_UI                      = 0x00002200,
AUTOMATIC_ACTIVEX_UI                       = 0x00002201,

ALLOW_RESTRICTEDPROTOCOLS                = 0x00002300,

//#endif //(_WIN32_IE >= _WIN32_IE_IE60SP2)
//#if (_WIN32_IE >= _WIN32_IE_IE70)
// Whether to do the Anti-Phishing check.
ALLOW_APEVALUATION                       = 0x00002301,

// The following ExpressAPP and XPS actions are trumped by registry in
// case of Internet Explorer upgrade from IE 6.0 which honors registry.
WINDOWS_BROWSER_APPLICATIONS             = 0x00002400,
XPS_DOCUMENTS                            = 0x00002401,
LOOSE_XAML                               = 0x00002402,
LOWRIGHTS                                = 0x00002500,
// The following action belong to WinFX Bootstrapper
WINFX_SETUP                              = 0x00002600,

INPRIVATE_BLOCKING                       = 0x00002700,
	}

	public enum tagURLZONE
	{
		URLZONE_INVALID = -1,
		URLZONE_PREDEFINED_MIN = 0,
		URLZONE_LOCAL_MACHINE = 0,
		URLZONE_INTRANET = URLZONE_LOCAL_MACHINE + 1,
		URLZONE_TRUSTED = URLZONE_INTRANET + 1,
		URLZONE_INTERNET = URLZONE_TRUSTED + 1,
		URLZONE_UNTRUSTED = URLZONE_INTERNET + 1,
		URLZONE_PREDEFINED_MAX = 999,
		URLZONE_USER_MIN = 1000,
		URLZONE_USER_MAX = 10000
	}

	public enum _URLZONEREG
	{
		URLZONEREG_DEFAULT = 0,
		URLZONEREG_HKLM = URLZONEREG_DEFAULT + 1,
		URLZONEREG_HKCU = URLZONEREG_HKLM + 1
	}

	public enum tagOLECONTF
	{
		OLECONTF_EMBEDDINGS = 1,
		OLECONTF_LINKS = 2,
		OLECONTF_OTHERS = 4,
		OLECONTF_ONLYUSER = 8,
		OLECONTF_ONLYIFRUNNING = 16
	}

	public enum OLEMISC	: uint
	{
		INVISIBLEATRUNTIME = 0x00000400,
		ALWAYSRUN          = 0x00000800,
		ACTSLIKEBUTTON     = 0x00001000,
		ACTSLIKELABEL      = 0x00002000,
		NOUIACTIVATE       = 0x00004000,
		ALIGNABLE          = 0x00008000,
		SIMPLEFRAME        = 0x00010000,
		SETCLIENTSITEFIRST = 0x00020000,
		IMEMODE            = 0x00040000
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct SIZE
	{
		[MarshalAs(UnmanagedType.I4)]
		public int cx;
		[MarshalAs(UnmanagedType.I4)]
		public int cy;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct SIZEL
	{
		public int cx;
		public int cy;
	}

	[ComVisible(true), StructLayout(LayoutKind.Sequential)]
	public struct tagDOCHOSTUIINFO
	{
		[MarshalAs(UnmanagedType.U4)]
		public uint cbSize;
		[MarshalAs(UnmanagedType.U4)]
		public uint dwFlags;
		[MarshalAs(UnmanagedType.U4)]
		public uint dwDoubleClick;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string pchHostCss;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string pchHostNS;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct tagOIFI
	{
		[MarshalAs(UnmanagedType.U4)]
		public uint cb;
		[MarshalAs(UnmanagedType.Bool)]
		public bool fMDIApp;
		public IntPtr hwndFrame;
		public IntPtr hAccel;
		[MarshalAs(UnmanagedType.U4)]
		public uint cAccelEntries;

	}

	[ComVisible(true), StructLayout(LayoutKind.Sequential)]
	public struct tagOLECMD
	{
		[MarshalAs(UnmanagedType.U4)]
		public uint cmdID;
		[MarshalAs(UnmanagedType.U4)]
		public uint cmdf;
	}

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct LOGPALETTE
    {
        public ushort palVersion;
        public ushort palNumEntries;
        public IntPtr palPalEntry;
    }

    [Flags]
    public enum XcpHostOptions
    {
      FreezeOnInitialFrame = 0x01,
      DisableFullScreen = 0x02,
      DisableManagedExecution = 0x08,
      EnableCrossDomainDownloads = 0x10,
      UseCustomAppDomain = 0x020,
      DisableNetworking = 0x040,  
      DisableScriptCallouts = 0x080,
      EnableHtmlDomAccess = 0x100,
      EnableScriptableObjectAccess = 0x200
    }

    [ComVisible(true), StructLayout(LayoutKind.Sequential)]
    public struct tagRECT
    {
        [MarshalAs(UnmanagedType.I4)]
        public int left;
        [MarshalAs(UnmanagedType.I4)]
        public int top;
        [MarshalAs(UnmanagedType.I4)]
        public int right;
        [MarshalAs(UnmanagedType.I4)]
        public int bottom;
    }

    [ComVisible(true), StructLayout(LayoutKind.Sequential)]
    public struct tagMSG
    {
        public IntPtr hwnd;
        [MarshalAs(UnmanagedType.I4)]
        public int message;
        public IntPtr wParam;
        public IntPtr lParam;
        [MarshalAs(UnmanagedType.I4)]
        public int time;
        // pt was a by-value POINT structure
        [MarshalAs(UnmanagedType.I4)]
        public int pt_x;
        [MarshalAs(UnmanagedType.I4)]
        public int pt_y;
        //public tagPOINT pt;
    }

    [ComVisible(true), StructLayout(LayoutKind.Sequential)]
    public struct tagSIZE
    {
        [MarshalAs(UnmanagedType.I4)]
        public int cx;
        [MarshalAs(UnmanagedType.I4)]
        public int cy;
        //public tagSIZE(int cx, int cy)
        //{
        //    this.cx = cx;
        //    this.cy = cy;
        //}
    }

    public enum OLEDOVERB : int
    {
        OLEIVERB_DISCARDUNDOSTATE = -6,
        OLEIVERB_HIDE = -3,
        OLEIVERB_INPLACEACTIVATE = -5,
        OLECLOSE_NOSAVE = 1,
        OLEIVERB_OPEN = -2,
        OLEIVERB_PRIMARY = 0,
        OLEIVERB_PROPERTIES = -7,
        OLEIVERB_SHOW = -1,
        OLEIVERB_UIACTIVATE = -4
    }
}
