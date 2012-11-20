using System;

using DotNet.Common.Utility;

namespace DotNet.Common.Configuration
{
    /// <summary>
    /// 异常辅助工具类。
    /// </summary>
    public class ConfigThrowHelper
    {
        /// <summary>
        /// 抛出异常。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        public static void ThrowConfigException(string message, params object[] parameters)
        {
			throw new ConfigException(StringHelper.FormatWith(message, parameters));
        }

        /// <summary>
        /// </summary>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <exception cref="ConfigException">
        /// </exception>
        public static void ThrowConfigException(Exception innerException, string message, params object[] parameters)
        {
			throw new ConfigException(StringHelper.FormatWith(message, parameters), innerException);
        }
    }
}