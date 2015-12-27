namespace Apocalypse.Docks.Manage
{
    partial class SolutionExplorer
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolutionExplorer));
            this.slnExpToolbar = new System.Windows.Forms.ToolStrip();
            this.xtoolRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.xtoolCallDesigner = new System.Windows.Forms.ToolStripButton();
            this.xtoolCallTextEditor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.xtoolResEdit = new System.Windows.Forms.ToolStripButton();
            this.xtoolProjTest = new System.Windows.Forms.ToolStripButton();
            this.solutionTree = new System.Windows.Forms.TreeView();
            this.slnListImages = new System.Windows.Forms.ImageList(this.components);
            this.slnExpToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // slnExpToolbar
            // 
            this.slnExpToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.slnExpToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xtoolRefresh,
            this.toolStripSeparator1,
            this.xtoolCallDesigner,
            this.xtoolCallTextEditor,
            this.toolStripSeparator3,
            this.xtoolResEdit,
            this.xtoolProjTest});
            this.slnExpToolbar.Location = new System.Drawing.Point(0, 0);
            this.slnExpToolbar.Name = "slnExpToolbar";
            this.slnExpToolbar.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.slnExpToolbar.Size = new System.Drawing.Size(275, 25);
            this.slnExpToolbar.TabIndex = 0;
            // 
            // xtoolRefresh
            // 
            this.xtoolRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xtoolRefresh.Enabled = false;
            this.xtoolRefresh.Image = global::Apocalypse.Properties.Resources.arrow_refresh;
            this.xtoolRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xtoolRefresh.Name = "xtoolRefresh";
            this.xtoolRefresh.Size = new System.Drawing.Size(23, 22);
            this.xtoolRefresh.Text = "表示内容を更新";
            this.xtoolRefresh.Click += new System.EventHandler(this.xtoolRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // xtoolCallDesigner
            // 
            this.xtoolCallDesigner.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xtoolCallDesigner.Enabled = false;
            this.xtoolCallDesigner.Image = global::Apocalypse.Properties.Resources.map_edit;
            this.xtoolCallDesigner.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xtoolCallDesigner.Name = "xtoolCallDesigner";
            this.xtoolCallDesigner.Size = new System.Drawing.Size(23, 22);
            this.xtoolCallDesigner.Text = "マップデザイナで編集";
            // 
            // xtoolCallTextEditor
            // 
            this.xtoolCallTextEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xtoolCallTextEditor.Enabled = false;
            this.xtoolCallTextEditor.Image = global::Apocalypse.Properties.Resources.draw_pencil;
            this.xtoolCallTextEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xtoolCallTextEditor.Name = "xtoolCallTextEditor";
            this.xtoolCallTextEditor.Size = new System.Drawing.Size(23, 22);
            this.xtoolCallTextEditor.Text = "テキストエディタで編集";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // xtoolResEdit
            // 
            this.xtoolResEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xtoolResEdit.Enabled = false;
            this.xtoolResEdit.Image = global::Apocalypse.Properties.Resources.resources;
            this.xtoolResEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xtoolResEdit.Name = "xtoolResEdit";
            this.xtoolResEdit.Size = new System.Drawing.Size(23, 22);
            this.xtoolResEdit.Text = "プロジェクトのリソースを管理...";
            // 
            // xtoolProjTest
            // 
            this.xtoolProjTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xtoolProjTest.Enabled = false;
            this.xtoolProjTest.Image = global::Apocalypse.Properties.Resources.proj_test;
            this.xtoolProjTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xtoolProjTest.Name = "xtoolProjTest";
            this.xtoolProjTest.Size = new System.Drawing.Size(23, 22);
            this.xtoolProjTest.Text = "プロジェクトのテスト実行";
            // 
            // solutionTree
            // 
            this.solutionTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.solutionTree.ImageIndex = 0;
            this.solutionTree.ImageList = this.slnListImages;
            this.solutionTree.Location = new System.Drawing.Point(0, 25);
            this.solutionTree.Name = "solutionTree";
            this.solutionTree.SelectedImageIndex = 0;
            this.solutionTree.ShowNodeToolTips = true;
            this.solutionTree.Size = new System.Drawing.Size(275, 404);
            this.solutionTree.TabIndex = 1;
            this.solutionTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.solutionTree_NodeMouseDoubleClick);
            this.solutionTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.solutionTree_AfterSelect);
            this.solutionTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.solutionTree_NodeMouseClick);
            // 
            // slnListImages
            // 
            this.slnListImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("slnListImages.ImageStream")));
            this.slnListImages.TransparentColor = System.Drawing.Color.Transparent;
            this.slnListImages.Images.SetKeyName(0, "solution.png");
            this.slnListImages.Images.SetKeyName(1, "proj.png");
            this.slnListImages.Images.SetKeyName(2, "map.png");
            this.slnListImages.Images.SetKeyName(3, "script.png");
            // 
            // SolutionExplorer
            // 
            this.ClientSize = new System.Drawing.Size(275, 429);
            this.Controls.Add(this.solutionTree);
            this.Controls.Add(this.slnExpToolbar);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HideOnClose = true;
            this.Name = "SolutionExplorer";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft;
            this.Text = "ソリューション エクスプローラ";
            this.slnExpToolbar.ResumeLayout(false);
            this.slnExpToolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip slnExpToolbar;
        private System.Windows.Forms.ToolStripButton xtoolCallDesigner;
        private System.Windows.Forms.ToolStripButton xtoolCallTextEditor;
        private System.Windows.Forms.TreeView solutionTree;
        private System.Windows.Forms.ToolStripButton xtoolRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton xtoolProjTest;
        private System.Windows.Forms.ImageList slnListImages;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton xtoolResEdit;
    }
}
