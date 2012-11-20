using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet.Data
{
    /// <summary>
    /// Exception raised when a postcondition fails.
    /// </summary>
    [Serializable]
    public sealed class PostconditionException : DesignByContractException
    {
        /// <summary>
        /// Postcondition Exception.
        /// </summary>
        public PostconditionException() { }
        /// <summary>
        /// Postcondition Exception.
        /// </summary>
        public PostconditionException(string message) : base(message) { }
        /// <summary>
        /// Postcondition Exception.
        /// </summary>
        public PostconditionException(string message, Exception inner) : base(message, inner) { }
    }
}
