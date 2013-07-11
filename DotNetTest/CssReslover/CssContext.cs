using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fantasy.Tools.Text;

namespace Fantasy.Tools.CssResolve
{
    class CssContext
    {
        public void AppendStyleName(char c)
        {
            this.styleName.Add(c);
        }

        public void AppendPropertyName(char c)
        {
            this.propertyName.Add(c);
        }

        public void AppendPropertyText(char c)
        {
            if (this.propertyText.Count < 1) propertyTextPosition = position;   //record start position for replacement
            this.propertyText.Add(c);
        }

        internal void BreakFromStyle()
        {
            if (properties.Count > 0)
            {
                Styles.Add(new CssStyle { Name = styleName.Trim(), LineNumber = styleLineNumber, Properties = properties });
                properties = new List<PropertyText>();
            }
            else styleName.Clear();
        }

        internal void BreakFromProperty()
        {
            if (Matched)
            {
                properties.Add(new PropertyText
                {
                    Name = propertyName.Trim(),
                    Text = propertyText.Trim(),
                    StartPosition = propertyTextPosition
                });
            }
            else
            {
                propertyName.Clear();
                propertyText.Clear();
            }
            IgnoreProperty = false;
            IgnoreSpace = true;

            if (!noFilter)
            {
                foreach (var item in detectors)
                {
                    item.Index = 0;
                    item.Enabled = true;
                }
            }
        }
        /// <summary>
        /// Reset context variables
        /// </summary>
        public void Reset()
        {
            this.Styles.Clear();
            this.styleName.Clear();
            this.properties.Clear();
            this.propertyName.Clear();
            this.propertyText.Clear();
            this.PreviousState = null;
            this.successDetector = null;

            IgnoreSpace = true; 
            IgnoreProperty = false; 
            IsComment = false;

            position = 0;
            propertyTextPosition = 0;
            styleLineNumber = 1;
            lineNumber = 1;      
        }

        /// <summary>
        /// Whether or not continue to try match the property 
        /// </summary>
        public bool IgnoreProperty;
        /// <summary>
        /// 
        /// </summary>
        public bool IgnoreSpace = true;
        //for comments
        public bool IsComment;
        /// <summary>
        /// Indicate a comment section is ending
        /// </summary>
        public readonly NameDetector CommentTail = new NameDetector("*/");
        public StateReader PreviousState;

        #region Record/Filters
        public IList<CssStyle> Styles = new List<CssStyle>();
        IList<PropertyText> properties = new List<PropertyText>();
        IList<char> styleName = new List<char>();
        IList<char> propertyName = new List<char>();
        IList<char> propertyText = new List<char>();
        /// <summary>
        /// current char's position in css; 
        /// selected property text position
        /// current char's line number
        /// selected style's line number
        /// </summary>
        Int32 position, propertyTextPosition, lineNumber = 1, styleLineNumber = 1;
        NameDetector[] detectors;
        NameDetector successDetector;
        /// <summary>
        /// Don't filter properties
        /// </summary>
        Boolean noFilter = true;
        /// <summary>
        /// Whether the property name is matched
        /// </summary>
        public Boolean Matched
        {
            get
            {
                return noFilter || successDetector != null;
            }
        }

        public void AddFilters(params string[] keys)
        {
            if (keys.Length < 1) throw new ArgumentNullException();
            detectors = new NameDetector[keys.Length];

            for (int i = 0; i < keys.Length; i++)
            {
                detectors[i] = new NameDetector(keys[i]);
            }
            noFilter = false;
        }

        public void LogPosition(char c)
        {
            position++;
            if (c == '\n') lineNumber++;
        }

        public void LogLineNumber()
        {
            styleLineNumber = lineNumber;
        }
        /// <summary>
        /// Detect whether the current character matches CSS Resolving Context
        /// </summary>
        /// <param name="c">The character to match</param>
        public Boolean Match(char c)
        {
            if (noFilter) return true;
            NameDetector currentDetector = null;
            Boolean hasDetector = false;  //Indicates whether or not there's available property name dectector

            for (int i = detectors.Length - 1; i >= 0; i--)
            {
                var tmpDetector = detectors[i];
                if (!tmpDetector.Enabled) continue;
                
                var result = detectors[i].Match(c);
                if (result != MatchResult.Continue)
                {
                    tmpDetector.Enabled = false;
                    if (result == MatchResult.Success) currentDetector = detectors[i];
                }
                else if (!hasDetector) hasDetector = true;
            }
            successDetector = currentDetector;
            return currentDetector != null || hasDetector;    //$$$
        }
        #endregion
    }
}
