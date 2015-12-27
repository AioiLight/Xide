using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Apocalypse.Dialogs.NewProj
{
    public partial class NewProjPage_2 : Apocalypse.Dialogs.NewProj.NewProjPageBase
    {
        public NewProjPage_2()
        {
            InitializeComponent();
            runtimes = RuntimeManager.Instance.GetRuntimes();
            foreach (var i in runtimes)
            {
                var r = RuntimeManager.Instance.GetRuntime(i);
                runtimeList.Items.Add(new ListViewItem(new[]{
                    r.RuntimeFileName,r.Name,r.Version.ToString()
                }));
            }
        }
        string[] runtimes;

        public string SolutionName
        {
            get { return solutionName.Text; }
            set { solutionName.Text = value; }
        }

        public string SolutionDir
        {
            get { return createToDir.Text; }
            set { createToDir.Text = value; }
        }

        public string RuntimeName
        {
            get
            {
                if (runtimeList.SelectedIndices.Count == 0)
                    return null;
                else
                    return runtimes[runtimeList.SelectedIndices[0]];
            }
            set
            {
                if (value == null) return;
                int v = Array.FindIndex<string>(runtimes, new Predicate<string>((s) => s == value));
                if (v == -1)
                    throw new InvalidOperationException("Item not found.");
                runtimeList.Items[v].Selected = true;
            }
        }

        private void browseDir_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = createToDir.Text;
                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    createToDir.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
