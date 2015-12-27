using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apocalypse
{
    class Core
    {
        public static Core Instance { get; private set; }

        public static void Init(MainForm mform)
        {
            Instance = new Core(mform);
        }

        private Core(MainForm mf)
        {
            this.MainFormManager = new MainFormManage(mf);
        }

        public readonly MainFormManage MainFormManager;
        public class MainFormManage
        {
            readonly MainForm mf;
            public MainFormManage(MainForm mf)
            {
                this.mf = mf;
            }

            public void AppendChildForm(Reveal.Forms.DockForm ddf)
            {
                mf.AppendChild(ddf, WeifenLuo.WinFormsUI.Docking.DockState.Unknown);
            }

            public void AppendChildForm(Reveal.Forms.DockForm ddf,WeifenLuo.WinFormsUI.Docking.DockState state)
            {
                mf.AppendChild(ddf, state);
            }

            public Reveal.Forms.DockForm[] GetChildren()
            {
                return mf.GetChildren().Cast<Reveal.Forms.DockForm>().ToArray();
            }

            public void CreateNewProject()
            {
                mf.CreateNewProj();
            }

            public void CreateNewStage(Data.Project parent)
            {
                mf.CreateNewStage(parent);
            }
        }
    }
}
