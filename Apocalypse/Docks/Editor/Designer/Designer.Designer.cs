namespace Apocalypse.Docks.Editor.Designer
{
    partial class Designer
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
            this.vertScroll = new K.Controls.VScrollBarPlus();
            this.horzScroll = new K.Controls.HScrollBarPlus();
            this.panel1 = new System.Windows.Forms.Panel();
            this.designFace = new Apocalypse.Docks.Editor.Designer.DesignFace();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // vertScroll
            // 
            this.vertScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vertScroll.AutoEnabledControl = true;
            this.vertScroll.Location = new System.Drawing.Point(692, 0);
            this.vertScroll.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.vertScroll.Maximum = 0;
            this.vertScroll.Name = "vertScroll";
            this.vertScroll.Size = new System.Drawing.Size(16, 427);
            this.vertScroll.TabIndex = 1;
            // 
            // horzScroll
            // 
            this.horzScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.horzScroll.AutoEnabledControl = true;
            this.horzScroll.Location = new System.Drawing.Point(0, 427);
            this.horzScroll.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.horzScroll.Maximum = 0;
            this.horzScroll.Name = "horzScroll";
            this.horzScroll.Size = new System.Drawing.Size(692, 16);
            this.horzScroll.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.designFace);
            this.panel1.Controls.Add(this.vertScroll);
            this.panel1.Controls.Add(this.horzScroll);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(708, 443);
            this.panel1.TabIndex = 3;
            // 
            // designFace
            // 
            this.designFace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.designFace.Location = new System.Drawing.Point(0, 0);
            this.designFace.Name = "designFace";
            this.designFace.Size = new System.Drawing.Size(692, 427);
            this.designFace.TabIndex = 3;
            // 
            // Designer
            // 
            this.ClientSize = new System.Drawing.Size(708, 443);
            this.Controls.Add(this.panel1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "Designer";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
            this.Text = "デザイナ";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private K.Controls.VScrollBarPlus vertScroll;
        private K.Controls.HScrollBarPlus horzScroll;
        private System.Windows.Forms.Panel panel1;
        private DesignFace designFace;
    }
}
