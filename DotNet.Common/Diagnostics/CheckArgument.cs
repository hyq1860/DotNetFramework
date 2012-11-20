using System;
using System.IO;
using System.Text;
using DotNet.Common.IO;
using DotNet.Common.Utility;
using DotNet.Common.Resources;

namespace DotNet.Common.Diagnostics
{
    /// <summary>
    /// 校验工具类。
    /// </summary>
    public partial class Check
    {
        internal Check()
        {
        }

        /// <summary>
        /// 对输入参数进行校验。
        /// </summary>
        public class Argument
        {
            internal Argument()
            {
            }

            /// <summary>
            /// 验证参数值是否为Null。
            /// </summary>
            /// <param name="argument">参数。</param>
            /// <param name="argumentName">参数名。</param>
            public static void IsNotNull(string argumentName, object argument)
            {
                IsNotNull(argumentName, argument, R.CannotBeNull);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="argumentName"></param>
            /// <param name="argument"></param>
            /// <param name="message"></param>
            /// <param name="parameters"></param>
            public static void IsNotNull(string argumentName, object argument, string message, params object[] parameters)
            {
                if (argument == null)
                {
					throw new ArgumentNullException(argumentName, StringHelper.FormatWith(message, parameters));
                }
            }

            /// <summary>
            /// 验证参数值是否为有效的非空Guid。
            /// </summary>
            /// <param name="argument">参数。</param>
            /// <param name="argumentName">参数名。</param>
            public static void IsNotEmpty(string argumentName, Guid argument)
            {
                if (argument == Guid.Empty)
                {
                    throw new ArgumentException(R.CannotBeEmpty, argumentName);
                }
            }

            /// <summary>
            /// 验证参数值是否为有效的非空字符串。
            /// </summary>
            /// <param name="argument">参数。</param>
            /// <param name="argumentName">参数名。</param>
            public static void IsNotEmpty(string argumentName, string argument)
            {
                if (StringHelper.IsEmpty(argument))
                {
                    throw new ArgumentException(R.CannotBeEmpty, argumentName);
                }
            }

            #region IsAssignableFrom

            /// <summary>
            /// 验证指定的对象是否可以从指定 Type 的实例分配。如果在不可以，则断言失败。
            /// 断言失败时将显示一则默认设置的消息。
            /// </summary>
            /// <param name="paramName">参数的名称。</param>
            /// <param name="value">要验证的对象。</param>
            /// <param name="expectedType">期望要验证的对象是从该类型的一种继承。</param>
            public static void IsAssignableFrom(string paramName, object value, Type expectedType)
            {
                IsAssignableFrom(paramName, value, expectedType, R.UnexpectedType);
            }

            /// <summary>
            /// 验证指定的对象是否可以从指定 Type 的实例分配。如果在不可以，则断言失败。
            /// 断言失败时将显示一则消息。
            /// </summary>
            /// <param name="value">要验证的对象。</param>
            /// <param name="expectedType">期望要验证的对象是从该类型的一种继承。</param>
            /// <param name="paramName">参数的名称。</param>
            /// <param name="message">断言失败时显示的消息。</param>
            public static void IsAssignableFrom(string paramName, object value, Type expectedType, string message)
            {
                if (value == null)
                {
                    throw new ArgumentNullException(paramName);
                }

                if (expectedType == null)
                {
                    throw new ArgumentNullException("expectedType");
                }

                if (!value.GetType().IsAssignableFrom(expectedType))
                {
                    throw new ArgumentException(StringHelper.FormatWith(message, value.GetType().FullName, expectedType.FullName));
                }
            }

            #endregion

            #region IsTrue

            /// <summary>
            /// 验证指定的条件是否为 true。如果该条件为 false，则断言失败。
            /// </summary>
            /// <param name="condition">要验证的条件为 true。</param>
            /// <param name="paramName">参数的名称。</param>
            /// <exception cref="ArgumentException">condition 的计算结果为 false。</exception>
            public static void IsTrue(bool condition, string paramName)
            {
                IsTrue(condition, paramName, R.ConditionNotTrue, null);
            }

            /// <summary>
            /// 验证指定的条件是否为 true。如果该条件为 false，则断言失败。断言失败时将显示一则消息。
            /// </summary>
            /// <param name="condition">要验证的条件为 true。</param>
            /// <param name="paramName">参数的名称。</param>
            /// <param name="message">断言失败时显示的消息。</param>
            /// <exception cref="ArgumentException">condition 的计算结果为 false。</exception>
            public static void IsTrue(bool condition, string paramName, string message)
            {
                IsTrue(condition, paramName, message, null);
            }

            /// <summary>
            /// 验证指定的条件是否为 true。如果该条件为 false，则断言失败。断言失败时将显示一则消息，并向该消息应用指定的格式。
            /// </summary>
            /// <param name="condition">要验证的条件为 true。</param>
            /// <param name="paramName">参数的名称。</param>
            /// <param name="message">断言失败时显示的消息。</param>
            /// <param name="parameters">设置 message 格式时使用的参数的数组。</param>
            /// <exception cref="ArgumentException">condition 的计算结果为 false。</exception>
            public static void IsTrue(bool condition, string paramName, string message, params object[] parameters)
            {
                if (!condition)
                {
                    throw new ArgumentException(StringHelper.FormatWith(message, parameters), paramName);
                }
            }

            #endregion

            #region IsFalse

            /// <summary>
            /// 验证指定的条件是否为 true。如果该条件为 false，则断言失败。
            /// </summary>
            /// <param name="condition">要验证的条件为 true。</param>
            /// <param name="paramName">参数的名称。</param>
            /// <exception cref="ArgumentException">condition 的计算结果为 false。</exception>
            public static void IsFalse(bool condition, string paramName)
            {
                IsFalse(condition, paramName, R.ConditionNotFalse, null);
            }

            /// <summary>
            /// 验证指定的条件是否为 true。如果该条件为 false，则断言失败。断言失败时将显示一则消息。
            /// </summary>
            /// <param name="condition">要验证的条件为 true。</param>
            /// <param name="paramName">参数的名称。</param>
            /// <param name="message">断言失败时显示的消息。</param>
            /// <exception cref="ArgumentException">condition 的计算结果为 false。</exception>
            public static void IsFalse(bool condition, string paramName, string message)
            {
                IsFalse(condition, paramName, message, null);
            }

            /// <summary>
            /// 验证指定的条件是否为 true。如果该条件为 false，则断言失败。断言失败时将显示一则消息，并向该消息应用指定的格式。
            /// </summary>
            /// <param name="condition">要验证的条件为 true。</param>
            /// <param name="paramName">参数的名称。</param>
            /// <param name="message">断言失败时显示的消息。</param>
            /// <param name="parameters">设置 message 格式时使用的参数的数组。</param>
            /// <exception cref="ArgumentException">condition 的计算结果为 false。</exception>
            public static void IsFalse(bool condition, string paramName, string message, params object[] parameters)
            {
                if (!condition)
                {
                    throw new ArgumentException(StringHelper.FormatWith(message, parameters), paramName);
                }
            }

            #endregion

            #region [ File ]

            /// <summary>
            /// 验证参数值是否为有效的文件路径。
            /// </summary>
            /// <param name="file">文件。</param>
            /// <param name="argumentName">参数名。</param>
            public static void FileMustBeExists(string file, string argumentName)
            {
                if (string.IsNullOrEmpty(file) || !File.Exists(PathUtils.GetFullPath(file)))
                {
                    throw new ArgumentException(StringHelper.FormatWith(R.FileNotFound, file), argumentName);
                }
            }

            /// <summary>
            /// 验证参数值是否全部为有效的文件路径。
            /// </summary>
            public static void FilesMustBeExists(string[] files, string argumentName)
            {
                bool exists = true;
                StringBuilder allFileNames = new StringBuilder();
                StringBuilder notFoundFileNames = new StringBuilder();

                if (files == null)
                {
                    exists = false;
                }
                else
                {
                    foreach (string file in files)
                    {
                        allFileNames.AppendFormat("{0};", file);
                        if (!File.Exists(PathUtils.GetFullPath(file)))
                        {
                            notFoundFileNames.AppendFormat("{0};", file);
                            exists = false;
                        }
                    }
                }

                if (exists == false)
                {
                    throw new ArgumentException(
                        StringHelper.FormatWith(R.FilesNotFullFound, allFileNames.ToString(), notFoundFileNames.ToString()), argumentName);
                }
            }

            #endregion
            
            #region [ IsEnumType ]
            
            /// <summary>
            /// 
            /// </summary>
            /// <param name="argumentName"></param>
            /// <param name="argumentType"></param>
            public static void IsEnumType(string argumentName, Type argumentType)
            {
				if (!TypeHelper.IsEnumType(argumentType))
				{
					ThrowHelper.ThrowArgumentException(argumentName, R.TypeIsNotEnumType, argumentType.FullName);
				}
            }
            
            #endregion
        }
    }
}