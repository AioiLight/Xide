using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

//Mainform file menu handlers
namespace Apocalypse
{
    public partial class MainForm
    {
        public void CreateNewProj()
        {
            using (var wiz = new Dialogs.NewProj.NewProjWiz())
            {
                if (wiz.ShowDialog() == DialogResult.OK)
                {
                    if (wiz.IsCreateSolution)
                    {
                        SolutionHolder.Instance.OpeningSolution = wiz.CreateSolution();
                    }
                    else
                    {
                        if (SolutionHolder.Instance.OpeningSolution == null)
                            throw new InvalidOperationException("Solution have not opened.");
                        SolutionHolder.Instance.OpeningSolution.AddProject(wiz.CreateProj());
                    }
                    SolutionHolder.Instance.SolutionUpdateNotify();
                }
            }
        }

        private void menuNewProject_Click(object sender, EventArgs e)
        {
            CreateNewProj();
        }

        public void CreateNewStage(Data.Project parent)
        {
            if (SolutionHolder.Instance.OpeningSolution == null)
                throw new InvalidOperationException("Solution have not opened.");
            using (var cns = new Dialogs.NewStage())
            {
                if (cns.ShowDialog() == DialogResult.OK)
                {
                    if (SolutionHolder.Instance.OpeningSolution == null)
                        throw new InvalidOperationException("Solution have not opened.");
                    parent.AddMap(new Apocalypse.Data.Map() { LinkedStageData = cns.SelectedStageData });
                    SolutionHolder.Instance.SolutionUpdateNotify();
                }
            }
        }

        private void menuNewStage_Click(object sender, EventArgs e)
        {
            var proj = solutionExplorer.CurrentParentProject();
            if (proj == null)
                throw new InvalidOperationException("Can't create stage");
            CreateNewStage(proj);
        }

        private void menuOpenSolution_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Xide ソリューション(*.xsn)|*.xsn";
                ofd.InitialDirectory = Path.Combine(Application.StartupPath, Definition.SolutionDir);
                ofd.Multiselect = false;
                ofd.Title = "ソリューションを開く";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string slnfile = ofd.FileName;
                    if (Path.GetExtension(slnfile) == ".xsn")
                    {
                        SolutionHolder.Instance.OpeningSolution = Data.Solution.SolutionReader(slnfile);
                        SolutionHolder.Instance.SolutionUpdateNotify();
                    }
                    else
                    {
                        MessageBox.Show(
                            "このファイルはソリューションではありません。",
                            "オープン エラー", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }
        }


        private void menuOpenFile_Click(object sender, EventArgs e)
        {

        }

        private void menuCloseWindow_Click(object sender, EventArgs e)
        {
            if (mainDock.Documents != null)
            {
                foreach (var doc in mainDock.Documents)
                {
                    var acd = doc as WeifenLuo.WinFormsUI.Docking.DockContent;
                    if (acd != null)
                        acd.Close();
                }
            }
        }

        private void menuCloseSolution_Click(object sender, EventArgs e)
        {

        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            var acd = mainDock.ActiveDocument as Reveal.Forms.DocumentDockForm;
            if (acd != null)
            {
                if (acd.EditingFile == null)
                    this.menuSaveAs_Click(sender, e);
                else
                    acd.SaveThis(acd.EditingFile);
            }
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            var acd = mainDock.ActiveDocument as Reveal.Forms.DocumentDockForm;
            if (acd != null)
            {
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Title = "ファイルの保存";
                    if (acd.EditingFile != null)
                        sfd.Filter =
                            "既定の拡張子(*" + Path.GetExtension(acd.EditingFile) + "|*" +
                            Path.GetExtension(acd.EditingFile) + "|" +
                            "テキスト ファイル(*.txt)|*.txt|任意の拡張子|*";
                    else
                        sfd.Filter =
                            "テキスト ファイル(*.txt)|*.txt|任意の拡張子|*";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        acd.EditingFile = sfd.FileName;
                        acd.SaveThis(sfd.FileName);
                    }

                }
            }
        }

        private void menuSaveAll_Click(object sender, EventArgs e)
        {
            if (mainDock.Documents != null)
            {
                var enume = from acd in mainDock.Documents.Cast<Reveal.Forms.DocumentDockForm>()
                            where acd.UIController != null && acd.UIController.GetEnabled(Reveal.Forms.UIReflectController.UIElements.Savable)
                            select acd;
                foreach (var acd in enume)
                {
                    if (acd.EditingFile == null)
                        this.menuSaveAs_Click(sender, e);
                    else
                        acd.SaveThis(acd.EditingFile);
                }
            }
        }

        private void menuOutputStage_Click(object sender, EventArgs e)
        {

        }

        private void menuOutputPicture_Click(object sender, EventArgs e)
        {

        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
