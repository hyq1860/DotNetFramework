// -----------------------------------------------------------------------
// <copyright file="StatusLableWithBar.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.SpiderApplication.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;

    ///
    /// A toolstrip label with incorporated barchart.
    ///
    [System.ComponentModel.DesignerCategory("code")]
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.StatusStrip)]
    public class ToolStripStatusLabelWithBar : ToolStripStatusLabel
    {
        /// Constructor.
        public ToolStripStatusLabelWithBar()
        {
            AutoSize = false;
        }

        private decimal m_dValue = 0;

        // Current progress
        private Color m_BarColor = Color.Blue;

        // Color of progress meter
        private int m_BarHeight = 0;

        /// The progress value.
        [DefaultValue(0)]
        public decimal Value
        {
            get
            {
                return m_dValue;
            }

            set
            {
                if (m_dValue > Maximum)
                    m_dValue = Maximum;
                else if (m_dValue < Minimum)
                    m_dValue = Minimum;
                else
                    m_dValue = value;
            }
        }




        decimal m_dMaximum = 100;

        /// The maximum value of the progress.
        public decimal Maximum
        {
            get { return m_dMaximum; }
            set { m_dMaximum = value; }
        }

        decimal m_dMinimum = 0;

        /// The minimum value of the progress.
        public decimal Minimum
        {
            get { return m_dMinimum; }
            set { m_dMinimum = value; }
        }


        /// Paint Function.
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Decimal dPercent = (m_dValue / (Maximum - Minimum));
            SolidBrush brush = new SolidBrush(BarColor);
            Rectangle rect = e.ClipRectangle;
            rect.Width = (int)(rect.Width * dPercent);
            int height = m_BarHeight;
            if (height == 0)
            {
                height = rect.Height;
            }

            rect.Y = ((rect.Height - height) / 2);
            rect.Height = height;
            // Draw the progress meter.
            g.FillRectangle(brush, rect);
            brush.Dispose();
            base.OnPaint(e);
        }


        ///
        /// Colour of the bar.
        ///
        [DefaultValue(typeof(Color), "Blue")]
        public Color BarColor
        {
            get
            {
                return m_BarColor;
            }
            set
            {
                m_BarColor = value;
                // Invalidate the control to get a repaint.
                this.Invalidate();
            }
        }

        /// The height of the bar (Must be less than height of component.
        [DefaultValue(0)]
        public int BarHeight
        {
            get
            {
                return m_BarHeight;
            }

            set
            {
                if (value > Size.Height)
                {
                    m_BarHeight = Size.Height;
                }
                else if (value <= 0)
                {
                    m_BarHeight = this.Size.Height;
                }
                else
                {
                    m_BarHeight = value;
                }
            }
        }
    }
}
