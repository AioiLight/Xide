using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Apocalypse.Docks.Manage
{
    public partial class OverView : Reveal.Forms.DockForm
    {
        protected override string GetPersistString()
        {
            return "OverView";
        }
        public OverView()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
    }
}
