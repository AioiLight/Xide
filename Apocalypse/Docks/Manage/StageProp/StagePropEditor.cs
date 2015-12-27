using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using WeifenLuo.WinFormsUI.Docking;

namespace Apocalypse.Docks.Manage.StageProp
{
    public partial class StagePropEditor : Reveal.Forms.DockForm
    {
        protected override string GetPersistString()
        {
            return "StagePropEditor";
        }

        public StagePropEditor()
        {
            this.DockAreas = DockAreas.DockBottom | DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.Float;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            HidePropertyGridToolbarButton(this.propGrid);
            /*
            string clsnm;
            object obj = PropClassAsmFactory.CreateInstance("table.xpd", out clsnm);
            this.propGrid.SelectedObject = obj;
            System.Diagnostics.Debug.WriteLine(clsnm);
             * */
            base.OnLoad(e);
        }

        private void HidePropertyGridToolbarButton(PropertyGrid pg)
        {
            const string FieldName = "toolStrip";
            var info = pg.GetType().GetField(FieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var bar = info.GetValue(pg) as ToolStrip;
            foreach (ToolStripItem b in bar.Items)
                b.Visible = b.Enabled && b.Visible && !(b is ToolStripSeparator);
        }

    }
}
