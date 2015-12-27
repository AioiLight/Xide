using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Apocalypse.Docks.Editor.Text
{
    public partial class TextRuler : UserControl
    {
        public TextRuler()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private int _selectIndex = 0;
        public int SelectedIndex
        {
            get { return _selectIndex; }
            set
            {
                _selectIndex = value;
                Refresh();
            }
        }

        public delegate int IndexToPointResolver();

        public IndexToPointResolver GetHeader = null;

        protected override void OnPaint(PaintEventArgs e)
        {
            int cWidth = TextRenderer.MeasureText(
                e.Graphics,
                " ",
                this.Font,
                new Size(),
                TextFormatFlags.NoPadding).Width;

            using (Brush b = new SolidBrush(this.BackColor))
                e.Graphics.FillRectangle(b, this.ClientRectangle);
            using (Brush back = new SolidBrush(this.ForeColor))
            {
                e.Graphics.FillRectangle(back, new Rectangle(0, this.Height - 1, this.Width, 1));
                if (GetHeader == null) return;
                int x = 0;
                Rectangle drawRect = new Rectangle();
                int cp = GetHeader.Invoke();
                try
                {
                    while (cp <= this.Width)
                    {
                        if (cp < 0)
                        {
                            x++;
                            cp += cWidth;
                            continue;
                        }
                        if (x % 10 == 0)
                        {
                            using (var f = new Font(this.Font.FontFamily, 8, FontStyle.Regular))
                            {
                                TextRenderer.DrawText(
                                    e.Graphics,
                                    x.ToString(),
                                    f,
                                    new Rectangle(cp, 0, this.Width, this.Height),
                                    this.ForeColor,
                                    TextFormatFlags.Left | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
                            }
                            drawRect = new Rectangle(cp, 0, 1, this.Height);
                        }
                        else
                            drawRect = new Rectangle(cp, this.Height / 4 * 3, 1, this.Height / 4);
                        e.Graphics.FillRectangle(back, drawRect);
                        if (x == this.SelectedIndex)
                        {
                            using (Brush over = new SolidBrush(Color.FromArgb(100, Color.Red)))
                                e.Graphics.FillRectangle(over, new Rectangle(cp, 0, cWidth, this.Height));
                        }
                        x++;
                        cp += cWidth;
                    }
                }
                catch (ArgumentOutOfRangeException)
                { }
            }
        }
    }
}
