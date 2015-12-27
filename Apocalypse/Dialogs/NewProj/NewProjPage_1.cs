using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Apocalypse.Dialogs.NewProj
{
    public partial class NewProjPage_1 : Apocalypse.Dialogs.NewProj.NewProjPageBase
    {
        public NewProjPage_1()
        {
            InitializeComponent();
        }

        public string ProjectName
        {
            get { return projectName.Text; }
            set { projectName.Text = value; }
        }

        public bool IsCreateSln
        {
            get { return isCreateSln.Checked; }
            set
            {
                if (!value || isAppendSln.Enabled)
                    isAppendSln.Checked = !value;
                else
                    isCreateSln.Checked = true;
            }
        }

        private void NewProjPage_1_Load(object sender, EventArgs e)
        {
            if (SolutionHolder.Instance.OpeningSolution == null)
            {
                label5.Enabled = false;
                isAppendSln.Enabled = false;
                isCreateSln.Checked = true;
            }
        }
    }
}
