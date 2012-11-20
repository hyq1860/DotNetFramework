// -----------------------------------------------------------------------
// <copyright file="Utf8StringWriter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;

namespace DotNet.Common.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Utf8StringWriter"/> class.
        /// </summary>
        /// <param name="sb">
        /// The sb.
        /// </param>
        public Utf8StringWriter(StringBuilder sb) : base(sb)
        {
        }
    }
}
