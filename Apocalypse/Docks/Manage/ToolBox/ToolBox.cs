using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Apocalypse.Docks.Manage.ToolBox
{
    public partial class ToolBox : Reveal.Forms.DockForm
    {

        protected override string GetPersistString()
        {
            return "ToolBox";
        }
        public ToolBox()
        {
            InitializeComponent();
        }

        private void ToolBox_Paint(object sender, PaintEventArgs e)
        {
            TextRenderer.DrawText(
                e.Graphics,
                "利用可能なアイテムはありません",
                this.Font,
                this.ClientRectangle,
                Color.Gray);
        }
    }
}
