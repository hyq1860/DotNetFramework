using System;
using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.Core;
using SharpWorkbench.Core.ViewContent;
using SharpWorkbench.Core.Workbench;

namespace SharpWorkbench.UI.ViewContent
{
    public class ImageDisplayBinding : IDisplayBinding
    {
        #region IDisplayBinding Members

        public bool CanCreateContentForFile(string fileName)
        {
            fileName = fileName.ToLower();
            return fileName.EndsWith(".jpg")
                || fileName.EndsWith(".jpeg")
                || fileName.EndsWith(".bmp")
                || fileName.EndsWith(".png")
                || fileName.EndsWith(".gif")
                || fileName.EndsWith(".tif");
        }

        public IViewContent CreateContentForFile(string fileName)
        {
            ImageDisplayBindingWrapper imgDisplay = new ImageDisplayBindingWrapper();
            imgDisplay.Load(fileName);
            return imgDisplay;
        }

        #endregion
    }

    public class ImageDisplayBindingWrapper : AbstractViewContent
    {
        
        Panel _panel;

        public override Control Control
        {
            get
            {
                return _panel;
            }
        }

        public override void Load(string fileName)
        {
            Image img = new Bitmap(fileName);
            PictureBox pb = new PictureBox();
            pb.Image = img;
            pb.Size = new Size(img.Width, img.Height);
            pb.Location = new Point(1, 1);
            pb.BorderStyle = BorderStyle.FixedSingle;

            _panel = new Panel();
            _panel.AutoScroll = true;
            _panel.Dock = DockStyle.Fill;
            _panel.TabIndex = 1;
            _panel.Controls.Add(pb);

            FileName = fileName;
            TitleName = System.IO.Path.GetFileName(fileName);
        }

        public override void Dispose()
        {
            _panel.Controls.Clear();
            _panel = null;
        }
    }
}
