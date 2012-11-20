using System;
using System.Collections.Generic;
using System.Text;

namespace ExtendedWebBrowser2
{
  /// <summary>
  /// This enum represents the possible browser commands
  /// </summary>
  [Flags]
  enum BrowserCommands
  {
    /// <summary>
    /// Used when no commans are available
    /// </summary>
    None = 0,
    Home = 1,
    Search = 2,
    Back = 4,
    Forward = 8,
    Stop = 16,
    Reload = 32,
    Print = 64,
    PrintPreview = 128
  }
}
