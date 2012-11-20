using System;

namespace DotNet.Common.Logging
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true, Inherited=true)]
    public class LogCategoryAttribute : Attribute
    {
        public string Category { get; private set; }

        public LogCategoryAttribute(string category)
        {
            Category = category;
        }
    }
}