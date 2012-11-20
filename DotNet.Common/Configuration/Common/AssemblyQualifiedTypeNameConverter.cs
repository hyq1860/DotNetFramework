using System;
using System.ComponentModel;
using System.Configuration;

using DotNet.Common.Diagnostics;
using DotNet.Common.Resources;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// Represents a configuration converter that converts a string to <see cref="Type"/> based on a fully qualified name.
    /// </summary>
    public class AssemblyQualifiedTypeNameConverter : ConfigurationConverterBase
    {
        /// <summary>
        /// Returns the assembly qualified name for the passed in Type.
        /// </summary>
        /// <param name="context">The container representing this System.ComponentModel.TypeDescriptor.</param>
        /// <param name="culture">Culture info for assembly</param>
        /// <param name="value">Value to convert.</param>
        /// <param name="destinationType">Type to convert to.</param>
        /// <returns>Assembly Qualified Name as a string</returns>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value != null)
            {
                Type typeValue = value as Type;
                if (typeValue == null)
                {
                    ThrowHelper.ThrowArgumentException("value", R.CanNotConvertType, value.GetType().FullName);
                }

                if (typeValue != null) return (typeValue).AssemblyQualifiedName;
            }
            return null;
        }

        /// <summary>
        /// Returns a type based on the assembly qualified name passed in as data.
        /// </summary>
        /// <param name="context">The container representing this System.ComponentModel.TypeDescriptor.</param>
        /// <param name="culture">Culture info for assembly.</param>
        /// <param name="value">Data to convert.</param>
        /// <returns>Type of the data</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            string stringValue = (string)value;
            if (!string.IsNullOrEmpty(stringValue))
            {
                Type result = Type.GetType(stringValue, false);
                if (result == null)
                {
                    ThrowHelper.ThrowArgumentException("value", R.InvalidTypeName, stringValue);
                }

                return result;
            }
            return null;
        }
    }
}