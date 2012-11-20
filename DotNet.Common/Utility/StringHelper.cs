using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace DotNet.Common.Utility
{
    public static class StringHelper
    {
        /// <summary>
        /// Encodes a string to be represented as a string literal. The format
        /// is essentially a JSON string.
        /// 
        /// The string returned includes outer quotes 
        /// Example Output: "Hello \"Rick\"!\r\nRock on"
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string EncodeJsString(string s)
        {
            StringBuilder sb = new StringBuilder( );
            sb.Append( "\"" );
            foreach ( char c in s )
            {
                switch ( c )
                {
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        int i = (int)c;
                        if (i < 32 || i > 127)
                        {
                            sb.AppendFormat("\\u{0:X04}", i);
                        }
                        else
                        {
                            sb.Append(c);
                        }

                        break;
                }
            }
            sb.Append("\"");

            return sb.ToString();
        }

        /// <summary>
        /// Check String is null or empty (length is zero or all chars in this string are space)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmpty(string input)
        {
            return string.IsNullOrEmpty(input) || input.Trim().Length == 0;
        }

        /// <summary>
        /// Check String is not null or  not empty 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(string input)
        {
            return string.IsNullOrEmpty(input);
        }

        /// <summary>
        /// Gets the left string.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="leftLength">Length of the left.</param>
        /// <returns></returns>
        public static string GetLeftString(string description, int leftLength)
        {
            if (IsEmpty(description) || description.Length <= leftLength)
            {
                return description;
            }

            return description.Substring(0, leftLength);
        }

        /// <summary>
        /// Gets the right string.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="rightLength">Length of the right.</param>
        /// <returns></returns>
        public static string GetRightString(string description, int rightLength)
        {
            if (IsEmpty(description) || description.Length <= rightLength)
            {
                return description;
            }

            return description.Substring(description.Length - rightLength);
        }

        /// <summary>
        /// </summary>
        /// <param name="strLength">
        /// The str length.
        /// </param>
        /// <returns>
        /// </returns>
        public static string GetRandomStr(int strLength)
        {
            return GetRandomStr(strLength, 0);
        }

        /// <summary>
        /// </summary>
        /// <param name="strLength">
        /// The str length.
        /// </param>
        /// <param name="randomSeed">
        /// The random seed.
        /// </param>
        /// <returns>
        /// </returns>
        public static string GetRandomStr(int strLength, int randomSeed)
        {
            string strBaseStr = "1,2,3,4,5,6,7,8,9,0,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] baseStrList = strBaseStr.Split(',');

            Random randomObject = null;
            if (randomSeed > 0)
            {
                randomObject = new Random(randomSeed);
            }
            else
            {
                randomObject = new Random();
            }

            string strResult = "";
            while (strResult.Length < strLength)
            {
                strResult = strResult + baseStrList[randomObject.Next(baseStrList.Length)];
            }

            return strResult;
        }

        /// <summary>
        /// 冒泡排序
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static string[] BubbleSort(string[] r)
        {
            for (int i = 0; i < r.Length; i++)
            {
                bool flag = false;
                for (int j = r.Length - 2; j >= i; j--)
                {
                    if (string.CompareOrdinal(r[j + 1], r[j]) < 0)
                    {
                        string str = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = str;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    return r;
                }
            }
            return r;
        }

        /// <summary>
        /// </summary>
        /// <param name="beCheckedStr">
        /// The be checked str.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool IsInt(string beCheckedStr)
        {
            if (string.IsNullOrEmpty(beCheckedStr)) return false;

            int tempInt;
            return int.TryParse(beCheckedStr, out tempInt);
        }

        /// <summary>
        /// </summary>
        /// <param name="beCheckedStr">
        /// The be checked str.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool IsDecimal(string beCheckedStr)
        {
            if (string.IsNullOrEmpty(beCheckedStr))
            {
                return false;
            }

            decimal tempDecimal;
            return decimal.TryParse(beCheckedStr, out tempDecimal);
        }

        /// <summary>
        /// </summary>
        /// <param name="beCheckedStr">
        /// The be checked str.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool IsDouble(string beCheckedStr)
        {
            if (string.IsNullOrEmpty(beCheckedStr))
            {
                return false;
            }

            double tempDouble;
            return double.TryParse(beCheckedStr, out tempDouble);
        }

        public static bool IsDateTime(string beCheckedStr)
        {
            if (string.IsNullOrEmpty(beCheckedStr))
            {
                return false;
            }

            DateTime tempDateTime;
            return DateTime.TryParse(beCheckedStr, out tempDateTime);
        }

        public static string GetStringValue(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }

        public static int GetIntValue(object obj)
        {
            return obj == null ? 0 : GetIntValue(obj.ToString());
        }

        /// <summary>
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <returns>
        /// </returns>
        public static int GetIntValue(string str)
        {
            return IsInt(str) ? int.Parse(str) : 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// </returns>
        public static decimal GetDecimalValue(object obj)
        {
            return obj == null ? 0 : GetDecimalValue(obj.ToString());
        }

        /// <summary>
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <returns>
        /// </returns>
        public static decimal GetDecimalValue(string str)
        {
            return IsDecimal(str) ? decimal.Parse(str) : 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <returns>
        /// </returns>
        public static DateTime GetDateTimeValue(string str)
        {
            return IsDateTime(str) ? DateTime.Parse(str) : DateTimeHelper.MinValue;
        }

        /// <summary>
        /// </summary>
        /// <param name="beChangeArray">
        /// The be change array.
        /// </param>
        /// <returns>
        /// </returns>
        public static string[] ChangeIntArrayToStringArray(int[] beChangeArray)
        {
            if (beChangeArray == null)
            {
                return new string[0];
            }

            string[] strList = new string[beChangeArray.Length];
            int I = 0;

            foreach (int curInt in beChangeArray)
            {
                strList[I] = curInt.ToString();
                I++;
            }

            return strList;
        }

        /// <summary>
        /// </summary>
        /// <param name="beChangeArray">
        /// The be change array.
        /// </param>
        /// <returns>
        /// </returns>
        public static int[] ChangeStringArrayToIntArray(string[] beChangeArray)
        {
            if (beChangeArray == null) return new int[0];

            int[] intList = new int[beChangeArray.Length];
            int I = 0;
            int tempInt = 0;

            foreach (string curStr in beChangeArray)
            {
                intList[I] = 0;
                tempInt = 0;
                if (int.TryParse(curStr, out tempInt))
                {
                    intList[I] = tempInt;
                }
                I++;
            }

            return intList;
        }

        public static decimal[] ChangeStringArrayToDecimalArray(string[] beChangeArray)
        {
            if (beChangeArray == null) return new decimal[0];

            decimal[] decimalList = new decimal[beChangeArray.Length];
            int I = 0;
            decimal tempDecimal = 0;

            foreach (string curStr in beChangeArray)
            {
                decimalList[I] = 0;
                tempDecimal = 0;
                if (decimal.TryParse(curStr, out tempDecimal))
                {
                    decimalList[I] = tempDecimal;
                }

                I++;
            }

            return decimalList;
        }

        /// <summary>
        /// </summary>
        /// <param name="beChangeList">
        /// The be change list.
        /// </param>
        /// <param name="linkStr">
        /// The link str.
        /// </param>
        /// <returns>
        /// </returns>
        public static string ChangeStringListToStr(List<string> beChangeList, string linkStr)
        {
            StringBuilder tempResult = new StringBuilder();

            int I = 1;
            foreach (string curStr in beChangeList)
            {
                tempResult.Append(curStr);
                if (I < beChangeList.Count)
                {
                    tempResult.Append(linkStr);
                }

                I++;
            }

            return tempResult.ToString();
        }

        public static List<string> ChangeIntListToStringList(List<int> beChangeList)
        {
            if (beChangeList == null || beChangeList.Count <= 0) return new List<string>();

            List<string> strList = new List<string>();

            foreach (int curInt in beChangeList)
            {
                strList.Add(curInt.ToString());
            }

            return strList;
        }

        public static List<int> ChangeStringListToIntList(List<string> beChangeList)
        {
            if (beChangeList == null || beChangeList.Count <= 0) return new List<int>();

            List<int> intList = new List<int>();

            foreach (string curStr in beChangeList)
            {
                if (IsInt(curStr))
                {
                    intList.Add(int.Parse(curStr));
                }
            }

            return intList;
        }

        /// <summary>
        /// 字符串格式化。
        /// </summary>
        /// <param name="target">要格式化的字符串。</param>
        /// <param name="args">参数。</param>
        /// <returns>格式化的字符串。</returns>
        public static string FormatWith(string target, params object[] args)
        {
            return IsEmpty(target) ? string.Empty : string.Format(CultureInfo.CurrentCulture, target, args == null ? new object[] { } : args);
        }

        public static string CutString(this string str, int len)
        {
            if (str == null || str.Length == 0 || len <= 0)
            {
                return string.Empty;
            }

            int l = str.Length;

            #region 计算长度
            int clen = 0;
            while (clen < len && clen < l)
            {
                //每遇到一个中文，则将目标长度减一。
                if ((int)str[clen] > 128) { len--; }
                clen++;
            }
            #endregion

            if (clen < l)
            {
                return str.Substring(0, clen) + "...";
            }
            else
            {
                return str;
            }
        }

    }
}
