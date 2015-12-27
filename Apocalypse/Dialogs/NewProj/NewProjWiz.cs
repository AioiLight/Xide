using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Apocalypse.Base.Forms;
using System.IO;

namespace Apocalypse.Dialogs.NewProj
{
    public partial class NewProjWiz : Apocalypse.Base.Forms.Wizard
    {
        public NewProjWiz()
        {
            InitializeComponent();
            isCreateSolution = false;
            solutionName = "NewSolution";
            solutionDir = Path.Combine(Application.StartupPath,Definition.SolutionDir);
            projectName = "NewProject";
            runtimeName = RuntimeManager.Instance.GetRuntimes()[0];
        }

        protected override void PanelPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(
                Properties.Resources.newproj256,
                new Point(-70, 13)
                );
            base.PanelPaint(sender, e);
        }

        protected override DockStyle DefaultDockStyle
        {
            get
            {
                return DockStyle.Right;
            }
        }

        int iCnt = 0;

        bool isCreateSolution;
        public bool IsCreateSolution { get { return isCreateSolution; } }
        string solutionName;
        string solutionDir;
        string projectName;
        string runtimeName;

        WizardPageBase prevPage = null;
        protected override WizardPageBase MovePage(Wizard.MoveDirection dir)
        {
            switch (iCnt)
            {
                case 1:
                    projectName = ((NewProjPage_1)prevPage).ProjectName;
                    isCreateSolution = ((NewProjPage_1)prevPage).IsCreateSln;
                    break;
                case 2:
                    solutionName = ((NewProjPage_2)prevPage).SolutionName;
                    solutionDir = ((NewProjPage_2)prevPage).SolutionDir;
                    runtimeName = ((NewProjPage_2)prevPage).RuntimeName;
                    break;
                default:
                    break;
            }
            if (dir == MoveDirection.Next)
                iCnt++;
            else
                iCnt--;
            switch (iCnt)
            {
                case 1:
                    PrevEnabled = false;
                    prevPage = new NewProjPage_1() { ProjectName = projectName, IsCreateSln = isCreateSolution };
                    break;
                case 2:
                    PrevEnabled = true;
                    prevPage = new NewProjPage_2() { SolutionName = solutionName, SolutionDir = solutionDir, RuntimeName = runtimeName };
                    break;
                case 3:
                    SetCompleteMode();
                    return new NewProjPage_Finish();
                case 4:
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;
                default:
                    throw new InvalidOperationException("Invalid wizard page no. :" + iCnt.ToString());
            }
            return prevPage;
        }

        public Data.Solution CreateSolution()
        {
            var sln = new Data.Solution();
            sln._runtime = runtimeName;
            sln.SolutionName = solutionName;
            sln.SolutionDirectory = solutionDir;
            sln.Projects = new[] { CreateProj() };
            sln.Resources = sln.UsingRuntimePackage.Resources.Clone() as string[];
            return sln;
        }

        public Data.Project CreateProj()
        {
            var proj = new Data.Project();
            proj.ProjName = projectName;
            return proj;
        }
    }
}
