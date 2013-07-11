using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fantasy.Tools.Text;

namespace Fantasy.Tools.CssResolve
{
    public class CssResolver
    {
        public CssResolver()
        {
            rsComment = ReadComment;
            rsStyleName = ReadStyleName;
            rsPropertyName = ReadPropertyName;
            rsPropertyText = ReadPropertyText;
            rsStyleText = ReadStyleText;
        }

        public CssResolver(params string[] propertyNames) : this()
        {
            cssContext.AddFilters(propertyNames);
        }

        public IList<CssStyle> Resolve(string content)
        {
            cssContext.Reset();
            var state = rsStyleName;
            foreach (var c in content)
            {
                state = state(c);
                cssContext.LogPosition(c);  //add after calculate state
            }
            return cssContext.Styles;
        }

        #region Inner Members

        StateReader rsComment, rsStyleName, rsStyleText, rsPropertyName, rsPropertyText;
        CssContext cssContext = new CssContext();

        StateReader ReadStyleName(char c)
        {
            if (c == '{') return rsStyleText;
            if (c == '/')
            {
                cssContext.PreviousState = rsStyleName;
                return rsComment;
            }
            if (Char.IsSeparator(c) || Char.IsWhiteSpace(c))
            {
                if (!cssContext.IgnoreSpace) cssContext.AppendStyleName(c);
            }
            else
            {
                cssContext.IgnoreSpace = false;
                cssContext.LogLineNumber();
                cssContext.AppendStyleName(c);
            }
            return rsStyleName;
        }

        StateReader ReadStyleText(char c)
        {
            if (c == '}')
            {
                cssContext.BreakFromStyle();
                return rsStyleName;
            }

            if (c == '/')
            {
                cssContext.PreviousState = rsStyleText;
                return rsComment;
            }

            if (Char.IsWhiteSpace(c) || Char.IsSeparator(c)) return rsStyleText;
            else
            {
                if (cssContext.Match(c)) cssContext.AppendPropertyName(c);
                else cssContext.IgnoreProperty = true;
            }
            return rsPropertyName;
        }

        StateReader ReadPropertyName(char c)
        {
            if (c == ':')
            {
                if (!cssContext.Matched) cssContext.IgnoreProperty = true;
                cssContext.IgnoreSpace = true;
                return rsPropertyText;
            }

            if (Char.IsWhiteSpace(c) || Char.IsSeparator(c))
            {
                cssContext.IgnoreProperty = true;
            }
            else if (!cssContext.IgnoreProperty)
            {
                if (cssContext.Match(c)) cssContext.AppendPropertyName(c);
                else cssContext.IgnoreProperty = true;
            }
            return rsPropertyName;
        }

        StateReader ReadPropertyText(char c)
        {
            if (c == ';')
            {
                cssContext.BreakFromProperty();
                return rsStyleText;
            }
            if (c == '}')
            {
                cssContext.BreakFromProperty();
                cssContext.BreakFromStyle();
                return rsStyleName;
            }
            if (c == '/')
            {
                cssContext.PreviousState = rsPropertyText;
                return rsComment;
            }

            if (!cssContext.IgnoreProperty)
            {
                if (Char.IsWhiteSpace(c) || Char.IsSeparator(c))
                {
                    if (!cssContext.IgnoreSpace) cssContext.AppendPropertyText(c);
                }
                else
                {
                    cssContext.IgnoreSpace = false;
                    cssContext.AppendPropertyText(c);
                }
            }
            return rsPropertyText;
        }

        StateReader ReadComment(char c)
        {
            if (cssContext.IsComment)
            {
                if (cssContext.CommentTail.Match(c) == MatchResult.Success)
                {
                    cssContext.IsComment = false;
                    return cssContext.PreviousState;
                }
                else return rsComment;
            }
            else
            {
                if (c == '*')     //Only '/' occurs
                {
                    cssContext.IsComment = true;
                    return rsComment;
                }
                else
                {
                    if (cssContext.PreviousState == rsPropertyText)   //Property text may have file path containing '/'
                    {
                        cssContext.AppendPropertyText('/');
                        cssContext.AppendPropertyText(c);
                    }
                    return cssContext.PreviousState;
                }
            }
        }
        #endregion
    }
}
