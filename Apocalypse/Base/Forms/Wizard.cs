using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Apocalypse.Base.Forms
{
    public partial class Wizard : Form
    {
        public Wizard()
        {
            InitializeComponent();
        }

        private void Wizard_Load(object sender, EventArgs e)
        {
            wpb = MovePage(MoveDirection.Next);
        }

        protected bool NextEnabled
        {
            get { return buttonNext.Enabled; }
            set { buttonNext.Enabled = value; }
        }

        protected bool PrevEnabled
        {
            get { return buttonPrev.Enabled; }
            set { buttonPrev.Enabled = value; }
        }

        protected bool CancelEnabled
        {
            get { return buttonCancel.Enabled; }
            set { buttonCancel.Enabled = value; }
        }

        protected void SetCompleteMode()
        {
            CancelEnabled = false;
            NextEnabled = true;
            PrevEnabled = false;
            buttonNext.Text = "完了";
        }

        protected enum MoveDirection { Next, Previous };

        protected virtual WizardPageBase MovePage(MoveDirection dir)
        {
            return null;
        }

        WizardPageBase _wpb = null;
        WizardPageBase wpb
        {
            get { return this._wpb; }
            set
            {
                this.SuspendLayout();
                mainPanel.SuspendLayout();
                titleLabel.Text = "";
                if (value != null)
                {
                    titleLabel.Text = value.WizardTitle;
                    mainPanel.Controls.Add(value);
                    value.Dock = DefaultDockStyle;
                }
                if (_wpb != null)
                {
                    mainPanel.Controls.Remove(_wpb);
                    _wpb.Dispose();
                    _wpb = null;
                }
                _wpb = value;
                mainPanel.ResumeLayout();
                this.ResumeLayout();
            }
        }

        protected virtual DockStyle DefaultDockStyle { get { return DockStyle.Fill; } }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            wpb = MovePage(MoveDirection.Previous);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            wpb = MovePage(MoveDirection.Next);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected virtual void PanelPaint(object sender, PaintEventArgs e) { }
    }
}
