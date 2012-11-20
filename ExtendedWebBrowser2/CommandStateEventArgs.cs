using System;
using System.Collections.Generic;
using System.Text;

namespace ExtendedWebBrowser2
{
  /// <summary>
  /// Represents event information for the main form, when the command state of the active browser changes
  /// </summary>
  class CommandStateEventArgs : EventArgs
  {
    /// <summary>
    /// Creates a new instance of the <see cref="CommandStateEventArgs"/> class
    /// </summary>
    /// <param name="commands">A list of commands that are available</param>
    public CommandStateEventArgs(BrowserCommands commands)
    {
      _commands = commands;
    }
    private BrowserCommands _commands;

    /// <summary>
    /// Gets a list of commands that are available
    /// </summary>
    public BrowserCommands BrowserCommands
    {
      get { return _commands; }
    }
  }
}
