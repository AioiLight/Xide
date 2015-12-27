using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Reveal.UIElems
{
    public class Chip
    {
        public Manage.ChipData[] LinkedData { get; set; }
        public virtual void Draw(PaintEventArgs pe, int layer, Rectangle target, ref Image source)
        {
            //if (LinkedData.Length <= layer)
            //    throw new InvalidOperationException("Layer " + layer.ToString() + " is not supported.");
            //var ccd = this.LinkedData[layer];
            //target.X -= ccd.CenterLT.X;
            //target.Y -= ccd.CenterLT.Y;
            //target.Width = ccd.DrawClipRect.Width;
            //target.Height = ccd.DrawClipRect.Height;
            //pe.Graphics.DrawImage(source,target,ccd.DrawClipRect,GraphicsUnit.Pixel);
            //if (ccd.Overlap != null)
            //    pe.Graphics.DrawImage(ccd.Overlap, target, new Rectangle(0, 0, target.Width, target.Height), GraphicsUnit.Pixel);
        }
    }
}
