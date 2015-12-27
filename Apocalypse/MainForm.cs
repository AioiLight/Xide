using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Apocalypse
{
    public partial class MainForm : Form
    {
        DockPanel mainDock = null;
        public MainForm()
        {
            InitializeComponent();
            mainDock = new DockPanel();
            mainDock.Dock = DockStyle.Fill;
            mainDock.DocumentStyle = DocumentStyle.DockingWindow;
            mainDock.Parent = mainContainer.ContentPanel;
            mainDock.ShowDocumentIcon = true;
            mainDock.ActiveContentChanged += new EventHandler(mainDock_ActiveContentChanged);
            mainDock.ActiveDocumentChanged += new EventHandler(mainDock_ActiveDocumentChanged);
            UIReflectEvHandler = new EventHandler(UIController_DesignChanged);
            StatusUpdatedEvHandler = new Action<string>(StatusUpdated);
            SolutionHolder.Instance.SolutionUpdated += new Action(() =>
            {
                bool isOpeningSln = SolutionHolder.Instance.OpeningSolution != null;
                menuCloseSolution.Enabled = isOpeningSln;
                menuResMan.Enabled = isOpeningSln;
                menuDebug.Visible = isOpeningSln;
            });
            Core.Init(this);

            System.Diagnostics.Debug.WriteLine(string.Join(",", RuntimeManager.Instance.GetRuntimes()));
        }

        #region ReservedForms
        Docks.Manage.OverView overView = new Apocalypse.Docks.Manage.OverView();
        Docks.Manage.SolutionExplorer solutionExplorer = new Apocalypse.Docks.Manage.SolutionExplorer();
        Docks.Manage.StageProp.StagePropEditor stageProp = new Apocalypse.Docks.Manage.StageProp.StagePropEditor();
        Docks.Manage.ToolBox.ToolBox toolBox = new Apocalypse.Docks.Manage.ToolBox.ToolBox();
        #endregion

        #region Boot
        protected override void OnShown(EventArgs e)
        {
            this.AppendChild(overView, DockState.Unknown);
            this.AppendChild(solutionExplorer, DockState.Unknown);
            solutionExplorer.UpdateProjectPointing += new Action<bool>((b) =>
            {
                menuNewStage.Enabled = b;
                toolNewStage.Enabled = b;
                menuTestProj.Enabled = b || solutionExplorer.MapPointing;
            });
            solutionExplorer.UpdateMapPointing += new Action<bool>((b) =>
            {
                menuTestMap.Enabled = b;
                menuTestProj.Enabled = b || solutionExplorer.ProjectPointing;
            });
            this.AppendChild(stageProp, DockState.Unknown);
            this.AppendChild(toolBox, DockState.Unknown);
            this.AppendChild(new Docks.Editor.Designer.Designer(), DockState.Document);
            this.AppendChild(new Docks.Editor.Text.TextEditor(), DockState.Document);
            base.OnShown(e);
        }
        #endregion 

        #region ExternalSupport

        List<DockContent> dockContents = new List<DockContent>();
        public void AppendChild(DockContent dock, DockState dockstate)
        {
            if (dockstate == DockState.Hidden) return;
            dockContents.Add(dock);
            dock.FormClosed += new FormClosedEventHandler((o, e) => dockContents.Remove(dock));
            if (dockstate == DockState.Unknown)
                dock.Show(this.mainDock);
            else
                dock.Show(this.mainDock, dockstate);
        }
        public DockContent[] GetChildren()
        {
            return dockContents.ToArray();
        }

        #endregion
        #region InternalAutomation

        EventHandler UIReflectEvHandler;
        Reveal.Forms.DocumentDockForm prevDock = null;
        List<ToolStrip> externalToolStrips = new List<ToolStrip>();
        void mainDock_ActiveDocumentChanged(object sender, EventArgs e)
        {
            this.SuspendLayout();
            if (prevDock != null)
            {
                if (externalToolStrips.Count != 0)
                {
                    foreach (var item in externalToolStrips)
                    {
                        var parent = item.Parent as ToolStripPanel;
                        if (parent != null)
                            parent.Controls.Remove(item);
                    }
                    externalToolStrips.Clear();
                }
                menuTool.DropDownItems.Clear();
                if (prevDock.UIController != null)
                    prevDock.UIController.DesignChanged -= UIReflectEvHandler;
            }
            prevDock = mainDock.ActiveDocument as Reveal.Forms.DocumentDockForm;
            if (prevDock != null)
            {
                if (prevDock.UIController != null)
                    prevDock.UIController.DesignChanged += UIReflectEvHandler;
                SetUIEnabled();
                if (prevDock.Toolbars != null && prevDock.Toolbars.Length != 0)
                    foreach (var item in prevDock.Toolbars)
                    {
                        externalToolStrips.Add(item);
                        mainContainer.TopToolStripPanel.Join(item,mainContainer.TopToolStripPanel.Rows.Length);
                    }
                if (prevDock.ToolItems != null && prevDock.ToolItems.Length != 0)
                    menuTool.DropDownItems.AddRange(prevDock.ToolItems);
            }
            this.ResumeLayout();
        }

        void SetUIEnabled()
        {
            if (prevDock != null && prevDock.UIController != null)
            {
                menuSave.Enabled = prevDock.UIController.GetEnabled(Reveal.Forms.UIReflectController.UIElements.Savable);
                menuEdit.Visible = prevDock.UIController.GetEnabled(Reveal.Forms.UIReflectController.UIElements.EditEnabled);
                menuDelete.Enabled = prevDock.UIController.GetEnabled(Reveal.Forms.UIReflectController.UIElements.Deletable);
                menuRedo.Enabled = prevDock.UIController.GetEnabled(Reveal.Forms.UIReflectController.UIElements.Redoable);
                menuSelectAll.Enabled = prevDock.UIController.GetEnabled(Reveal.Forms.UIReflectController.UIElements.SelectAllCapable);
                menuUndo.Enabled = prevDock.UIController.GetEnabled(Reveal.Forms.UIReflectController.UIElements.Undoable);
                menuTool.Visible = prevDock.UIController.GetEnabled(Reveal.Forms.UIReflectController.UIElements.ToolEnabled);
                menuShowSrc.Enabled = prevDock.UIController.GetEnabled(Reveal.Forms.UIReflectController.UIElements.CodeEditable);
                menuShowGEdit.Enabled = prevDock.UIController.GetEnabled(Reveal.Forms.UIReflectController.UIElements.VisualEditable);
            }
            else
            {
                menuSave.Enabled = false;
                menuEdit.Visible = false;
                menuDelete.Enabled = false;
                menuRedo.Enabled = false;
                menuSelectAll.Enabled = false;
                menuUndo.Enabled = false;
                menuTool.Visible = false;
                menuShowSrc.Enabled = false;
                menuShowGEdit.Enabled = false;
            }
            menuSaveAs.Enabled = menuSave.Enabled;
            toolSave.Enabled = menuSave.Enabled;
            toolRedo.Enabled = menuRedo.Enabled;
            toolUndo.Enabled = menuUndo.Enabled;
        }

        void UIController_DesignChanged(object sender, EventArgs e)
        {
            this.SetUIEnabled();
        }

        void StatusUpdated(string newStatus)
        {
            if (string.IsNullOrEmpty(newStatus))
                mainStatusText.Text = "完了";
            else
                mainStatusText.Text = newStatus;
        }

        Reveal.Forms.DockForm prevActiveDock = null;
        Action<string> StatusUpdatedEvHandler;
        List<ToolStripItem> externalStatusItems = new List<ToolStripItem>();
        void mainDock_ActiveContentChanged(object sender, EventArgs e)
        {
            mainStatus.SuspendLayout();
            if (prevActiveDock != null)
            {
                if (externalStatusItems.Count != 0)
                {
                    foreach (var item in externalStatusItems)
                        mainStatus.Items.Remove(item);
                    externalStatusItems.Clear();
                }
                prevActiveDock.StatusStringUpdated -= StatusUpdatedEvHandler;
            }
            prevActiveDock = this.mainDock.ActiveContent as Reveal.Forms.DockForm;
            if (prevActiveDock != null)
            {
                if (prevActiveDock.StatusItems != null && prevActiveDock.StatusItems.Length != 0)
                    foreach (var item in prevActiveDock.StatusItems)
                    {
                        externalStatusItems.Add(item);
                        mainStatus.Items.Add(item);
                    }
                prevActiveDock.StatusStringUpdated += StatusUpdatedEvHandler;
                StatusUpdated(prevActiveDock.StatusString);
            }
            mainStatus.ResumeLayout();
        }

        #endregion

        private void menuAbout_Click(object sender, EventArgs e)
        {
            using (var ab = new Dialogs.About())
            {
                ab.ShowDialog();
            }
        }

    }
}
