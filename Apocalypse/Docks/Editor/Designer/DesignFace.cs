using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Apocalypse.Docks.Editor.Designer
{
    public partial class DesignFace : UserControl
    {
        public Size ActualSize { get; private set; }
        public Size ActualMapChipCount { get; private set; }
        public Point _topLeftPoint = new Point();
        public Point ScrollTopLeftPoint
        {
            get { return _topLeftPoint; }
            set { _topLeftPoint = value; }
        }

        public string[][] MapData { get; set; }
        public string ImageSource { get; private set; }

        DesignFaceRenderer dr;
        public DesignFace()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.Opaque, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            dr = new DesignFaceRenderer(this);
        }

        public void Init(string imageSource, string[][] mapData)
        {
            this.MapData = mapData;
            this.ImageSource = imageSource;
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            dr.Render(e);
            base.OnPaint(e);
        }
    }
}
