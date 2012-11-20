
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet.Data
{
    /// <summary>
    /// An exception that occurred when there is no connectionStringName specified in the configuration file.
    /// </summary>
    public class DatabaseNotSpecifiedException : ApplicationException
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class DataCommandFileNotSpecifiedException : ApplicationException
    {
    }

    /// <summary>
    /// An exception that occurred when a DataCommand file does not exist or cannot be deserialized.
    /// </summary>
    public class DataCommandFileLoadException : ApplicationException
    {
        public DataCommandFileLoadException(string fileName)
            : base( "Error: DataCommand file " + fileName + " not found or is invalid." )
        {
        }
    }

    public class DuplicateCommandNameException : ApplicationException
    {
        public DuplicateCommandNameException(string fileName, string commandName)
            : base( "Error: DataCommand file " + fileName + " has a same command name" + commandName + " now." )
        {
        }

    }
}
