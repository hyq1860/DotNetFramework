using System;
using System.Collections.Generic;
using System.Text;

namespace ExtendedWebBrowser2
{
  /// <summary>
  /// Represents the filter level op the pop-up blocker
  /// </summary>
  enum PopupBlockerFilterLevel
  {
    /// <summary>
    /// No pop-ups are blocked
    /// </summary>
    None = 0,
    /// <summary>
    /// Pop-ups of secure sites are allowed
    /// </summary>
    Low,
    /// <summary>
    /// Most pop-ups are blocked, unless the Ctrl key is pressed
    /// </summary>
    Medium,
    /// <summary>
    /// All pop-ups are blocked, unless the Ctrl key is pressed
    /// </summary>
    High
  }
}
