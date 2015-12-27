namespace Apocalypse.Dialogs.NewProj
{
    partial class NewProjPage_2
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.solutionName = new System.Windows.Forms.TextBox();
            this.createToDir = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.browseDir = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.runtimeList = new System.Windows.Forms.ListView();
            this.fileNameHeader = new System.Windows.Forms.ColumnHeader();
            this.nameHeader = new System.Windows.Forms.ColumnHeader();
            this.verHeader = new System.Windows.Forms.ColumnHeader();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "作成するソリューションの設定を入力してください。";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "ソリューション名";
            // 
            // solutionName
            // 
            this.solutionName.Location = new System.Drawing.Point(101, 44);
            this.solutionName.Name = "solutionName";
            this.solutionName.Size = new System.Drawing.Size(336, 19);
            this.solutionName.TabIndex = 2;
            // 
            // createToDir
            // 
            this.createToDir.Location = new System.Drawing.Point(101, 69);
            this.createToDir.Name = "createToDir";
            this.createToDir.Size = new System.Drawing.Size(307, 19);
            this.createToDir.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "作成先ディレクトリ";
            // 
            // browseDir
            // 
            this.browseDir.Location = new System.Drawing.Point(414, 67);
            this.browseDir.Name = "browseDir";
            this.browseDir.Size = new System.Drawing.Size(23, 23);
            this.browseDir.TabIndex = 5;
            this.browseDir.Text = "...";
            this.browseDir.UseVisualStyleBackColor = true;
            this.browseDir.Click += new System.EventHandler(this.browseDir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.runtimeList);
            this.groupBox1.Location = new System.Drawing.Point(7, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(430, 171);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "利用するランタイムの選択";
            // 
            // runtimeList
            // 
            this.runtimeList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fileNameHeader,
            this.nameHeader,
            this.verHeader});
            this.runtimeList.FullRowSelect = true;
            this.runtimeList.GridLines = true;
            this.runtimeList.Location = new System.Drawing.Point(6, 18);
            this.runtimeList.MultiSelect = false;
            this.runtimeList.Name = "runtimeList";
            this.runtimeList.Size = new System.Drawing.Size(418, 147);
            this.runtimeList.TabIndex = 0;
            this.runtimeList.UseCompatibleStateImageBehavior = false;
            this.runtimeList.View = System.Windows.Forms.View.Details;
            // 
            // fileNameHeader
            // 
            this.fileNameHeader.Text = "ファイル名";
            this.fileNameHeader.Width = 120;
            // 
            // nameHeader
            // 
            this.nameHeader.Text = "名前";
            this.nameHeader.Width = 200;
            // 
            // verHeader
            // 
            this.verHeader.Text = "バージョン";
            // 
            // NewProjPage_2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.browseDir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.createToDir);
            this.Controls.Add(this.solutionName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "NewProjPage_2";
            this.WizardTitle = "新しいソリューションの作成";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox solutionName;
        private System.Windows.Forms.TextBox createToDir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button browseDir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView runtimeList;
        private System.Windows.Forms.ColumnHeader fileNameHeader;
        private System.Windows.Forms.ColumnHeader nameHeader;
        private System.Windows.Forms.ColumnHeader verHeader;
    }
}
