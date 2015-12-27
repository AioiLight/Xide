using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Apocalypse.Dialogs
{
    public partial class NewStage : Form
    {
        public NewStage()
        {
            InitializeComponent();
        }

        private void NewStage_Load(object sender, EventArgs e)
        {
            stageSelectCombo.Items.Clear();
            if (SolutionHolder.Instance.OpeningSolution == null)
                throw new InvalidOperationException("Solution have not opened.");
            stageSelectCombo.Items.AddRange(SolutionHolder.Instance.OpeningSolution.UsingRuntimePackage.Stages);
            stageSelectCombo.SelectedIndex = 0;
        }

        public Data.StageData SelectedStageData
        {
            get { return (Data.StageData)stageSelectCombo.SelectedItem; }
        }
    }
}
