/*******************************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author: Allen Wang(Allen.G.Wang@newegg.com) 
 * Create Date: 12/23/2008 
 * Description:
 *          
 * Revision History:
 *      Date         Author               Description
 * 
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet.Common.Diagnostics
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// 查打原始异常
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>原始的异常</returns>
        public static Exception FindSourceException(Exception ex)
        {
            Exception ex1 = ex;
            while (ex1 != null)
            {
                ex = ex1;
                ex1 = ex1.InnerException;
            }
            return ex;
        }

        /// <summary>
        /// 从异常树种查找指定类型的异常
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="expectedExceptionType">期待的异常类型</param>
        /// <returns>所要求的异常，如果找不到，返回null</returns>
        public static Exception FindSourceException(Exception ex, Type expectedExceptionType)
        {
            while (ex != null)
            {
                if (ex.GetType() == expectedExceptionType)
                {
                    return ex;
                }
                ex = ex.InnerException;
            }
            return null;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ex"></param>
		/// <returns></returns>
		public static string GetMessage(Exception ex)
		{
			List<Exception> exceptions = GetAllExceptions(ex);
			StringBuilder sb = new StringBuilder();
			foreach (Exception everyEx in exceptions)
			{
				sb.AppendLine(everyEx.Message);
			}

			return sb.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ex"></param>
		/// <returns></returns>
		public static string GetStackTrace(Exception ex)
		{
			List<Exception> exceptions = GetAllExceptions(ex);
			StringBuilder sb = new StringBuilder();

			foreach (Exception everyEx in exceptions)
			{
				sb.AppendFormat("[{0}: {1}]\r\n", everyEx.GetType().Name, everyEx.Message);
				sb.AppendLine(everyEx.StackTrace);
				sb.AppendLine("");
			}

			return sb.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ex"></param>
		/// <returns></returns>
		public static List<Exception> GetAllExceptions(Exception ex)
		{
			List<Exception> exceptions = new List<Exception>();
			while (ex != null)
			{
				exceptions.Add(ex);
				ex = ex.InnerException;
			}

			exceptions.Reverse();
			return exceptions;
		}
    }
}