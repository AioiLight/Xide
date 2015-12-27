namespace Apocalypse.Dialogs.NewProj
{
    partial class NewProjPage_1
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
            this.projectName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.isAppendSln = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.isCreateSln = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "プロジェクトの名前";
            // 
            // projectName
            // 
            this.projectName.Location = new System.Drawing.Point(99, 45);
            this.projectName.Name = "projectName";
            this.projectName.Size = new System.Drawing.Size(338, 19);
            this.projectName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(252, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "新しく作成するプロジェクトの設定を入力してください。";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.isAppendSln);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.isCreateSln);
            this.groupBox1.Location = new System.Drawing.Point(5, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 120);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "作成種別";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 90);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(282, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "このプロジェクトを現在編集中のソリューションへ追加します。";
            // 
            // isAppendSln
            // 
            this.isAppendSln.AutoSize = true;
            this.isAppendSln.Checked = true;
            this.isAppendSln.Location = new System.Drawing.Point(6, 68);
            this.isAppendSln.Name = "isAppendSln";
            this.isAppendSln.Size = new System.Drawing.Size(182, 16);
            this.isAppendSln.TabIndex = 2;
            this.isAppendSln.TabStop = true;
            this.isAppendSln.Text = "開いているソリューションへ追加(&A)";
            this.isAppendSln.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 40);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(309, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "このプロジェクトを含めるために、新しいソリューションを作成します。";
            // 
            // isCreateSln
            // 
            this.isCreateSln.AutoSize = true;
            this.isCreateSln.Location = new System.Drawing.Point(6, 18);
            this.isCreateSln.Name = "isCreateSln";
            this.isCreateSln.Size = new System.Drawing.Size(161, 16);
            this.isCreateSln.TabIndex = 0;
            this.isCreateSln.Text = "新しいソリューションを作成(&S)";
            this.isCreateSln.UseVisualStyleBackColor = true;
            // 
            // NewProjPage_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.projectName);
            this.Controls.Add(this.label1);
            this.Name = "NewProjPage_1";
            this.WizardTitle = "新しいプロジェクトの作成";
            this.Load += new System.EventHandler(this.NewProjPage_1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox projectName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton isCreateSln;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton isAppendSln;
        private System.Windows.Forms.Label label4;
    }
}
