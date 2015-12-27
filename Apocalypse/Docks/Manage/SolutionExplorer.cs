using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Apocalypse.Docks.Manage
{
    public partial class SolutionExplorer : Reveal.Forms.DockForm
    {

        public event Action UpdateSlnExpState;
        public event Action<bool> UpdateProjectPointing;
        public event Action<bool> UpdateMapPointing;

        public bool ProjectPointing
        {
            get
            {
                var cNode = solutionTree.SelectedNode as CallbackNode;
                return cNode != null && cNode.ItemType == CallbackNode.ItemTypes.Project;
            }
        }

        public bool MapPointing
        {
            get
            {
                var cNode = solutionTree.SelectedNode as CallbackNode;
                return cNode != null && cNode.ItemType == CallbackNode.ItemTypes.Map;
            }
        }
        protected override string GetPersistString()
        {
            return "SolutionExplorer";
        }

        public SolutionExplorer()
        {
            InitializeComponent();
            SolutionHolder.Instance.SolutionUpdated += new Action(Instance_SolutionUpdated);
        }

        void Instance_SolutionUpdated()
        {
            UpdateSolutionTree();
        }

        void UpdateSolutionTree()
        {
            if (SolutionHolder.Instance.OpeningSolution == null) return;
            solutionTree.SuspendLayout();
            solutionTree.Nodes.Clear();
            var sln = SolutionHolder.Instance.OpeningSolution;
            CallbackNode slnTreeNode = new CallbackNode("ソリューション '" + sln.SolutionName + "'(" + sln.Projects.Length.ToString() + " プロジェクト)", 0, 0);
            slnTreeNode.ItemType = CallbackNode.ItemTypes.Solution;
            slnTreeNode.CMSCluster = new SlnCMSCluster();
            foreach (var proj in sln.Projects)
            {
                CallbackNode projNode = new CallbackNode(proj.ProjName, 1, 1);
                projNode.ItemType = CallbackNode.ItemTypes.Project;
                projNode.CMSCluster = new ProjCMSCluster(proj);
                int cnt = 0;
                foreach (var map in proj.Map)
                {
                    cnt++;
                    CallbackNode mapNode = new CallbackNode(map.LinkedStageData.Name + " " + cnt.ToString(), 2, 2);
                    mapNode.ItemType = CallbackNode.ItemTypes.Map;
                    projNode.Nodes.Add(mapNode);
                }
                foreach (var script in proj.ScriptFiles)
                {
                    CallbackNode scriptNode = new CallbackNode(Path.GetFileName(script), 3, 3);
                    scriptNode.ItemType = CallbackNode.ItemTypes.Script;
                    projNode.Nodes.Add(scriptNode);
                }
                slnTreeNode.Nodes.Add(projNode);
                projNode.NodeFont = new Font(solutionTree.Font, FontStyle.Bold);
            }
            solutionTree.Nodes.Add(slnTreeNode);
            solutionTree.ResumeLayout();
            UpdateState();
        }

        private void xtoolRefresh_Click(object sender, EventArgs e)
        {
            UpdateSolutionTree();
        }

        private void solutionTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var cnn = e.Node as CallbackNode;
                if (cnn != null)
                    cnn.OnDoubleClick(e.Location);
            }
        }

        private void solutionTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var cnn = e.Node as CallbackNode;
                if (cnn != null)
                    cnn.OnRightClick(this.solutionTree, e.Location);
            }
        }

        private void solutionTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateState();
        }

        public Data.Project CurrentParentProject()
        {
            var cn = solutionTree.SelectedNode as CallbackNode;
            if (cn == null || cn.ItemType != CallbackNode.ItemTypes.Project)
                return null;
            return ((ProjCMSCluster)cn.CMSCluster).ParentProj;
        }

        private void UpdateState()
        {
            xtoolRefresh.Enabled = SolutionHolder.Instance.OpeningSolution != null;
            xtoolResEdit.Enabled = xtoolRefresh.Enabled;
            var cNode = solutionTree.SelectedNode as CallbackNode;
            xtoolCallDesigner.Enabled = cNode != null && cNode.ItemType == CallbackNode.ItemTypes.Map;
            xtoolCallTextEditor.Enabled = cNode != null && (cNode.ItemType == CallbackNode.ItemTypes.Script || cNode.ItemType == CallbackNode.ItemTypes.Map);
            xtoolProjTest.Enabled = cNode != null &&
                (cNode.ItemType == CallbackNode.ItemTypes.Project ||
                cNode.ItemType == CallbackNode.ItemTypes.Script ||
                cNode.ItemType == CallbackNode.ItemTypes.Map);
            if (UpdateProjectPointing != null)
                UpdateProjectPointing.Invoke(cNode != null && cNode.ItemType == CallbackNode.ItemTypes.Project);
            if (UpdateMapPointing != null)
                UpdateMapPointing.Invoke(cNode != null && cNode.ItemType == CallbackNode.ItemTypes.Map);
            if (UpdateSlnExpState != null)
                UpdateSlnExpState.Invoke();
        }
    }

    public class CallbackNode : TreeNode
    {

        public CallbackNode(string text, int closePict, int openPict)
            : base(text, closePict, openPict)
        {
            this.ItemType = ItemTypes.Other;
        }

        public enum ItemTypes { Solution, Project, Map, Script, Other };

        public ItemTypes ItemType { get; set; }

        public ContextMenuStripCluster CMSCluster = null;
        public void OnRightClick(Control parent, Point p)
        {
            if(CMSCluster != null)
                CMSCluster.context.Show(parent, p);
        }

        public event Action<Point> DoubleClick;
        public void OnDoubleClick(Point p)
        {
            if (DoubleClick != null)
                DoubleClick.Invoke(p);
        }
    }

    public abstract class ContextMenuStripCluster
    {
        public abstract ContextMenuStrip context { get; }
    }

    public class SlnCMSCluster : ContextMenuStripCluster
    {
        ContextMenuStrip cms;
        public SlnCMSCluster()
        {
            List<ToolStripItem> items = new List<ToolStripItem>();
            items.Add(new ToolStripMenuItem(
                "リソースの管理",
                Properties.Resources.resources));
            items.Add(new ToolStripSeparator());
            items.Add(new ToolStripMenuItem(
                "新規プロジェクトの追加",
                Properties.Resources.proj_add,
                new EventHandler((o, e) => Core.Instance.MainFormManager.CreateNewProject())));
            cms = new ContextMenuStrip();
            cms.Items.AddRange(items.ToArray());
        }

        public override ContextMenuStrip context
        {
            get { return cms; }
        }
    }

    public class ProjCMSCluster : ContextMenuStripCluster
    {
        Data.Project parent;
        ContextMenuStrip cms;
        public ProjCMSCluster(Data.Project proj)
        {
            parent = proj;
            List<ToolStripItem> items = new List<ToolStripItem>();
            items.Add(new ToolStripMenuItem(
                "新規ステージの追加",
                Properties.Resources.map_add,
                new EventHandler((o, e) => Core.Instance.MainFormManager.CreateNewStage(parent))));
            cms = new ContextMenuStrip();
            cms.Items.AddRange(items.ToArray());
        }
        public Data.Project ParentProj { get { return parent; } }

        public override ContextMenuStrip context
        {
            get { return cms; }
        }
    }
}
