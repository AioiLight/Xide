namespace Apocalypse.Docks.Editor.Text
{
    partial class TextEditor
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
            this.editor = new Sgry.Azuki.Windows.AzukiControl();
            this.ruler = new Apocalypse.Docks.Editor.Text.TextRuler();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // editor
            // 
            this.editor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.editor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editor.DrawingOption = ((Sgry.Azuki.DrawingOption)(((((Sgry.Azuki.DrawingOption.DrawsFullWidthSpace | Sgry.Azuki.DrawingOption.DrawsTab)
                        | Sgry.Azuki.DrawingOption.DrawsEol)
                        | Sgry.Azuki.DrawingOption.HighlightCurrentLine)
                        | Sgry.Azuki.DrawingOption.ShowsLineNumber)));
            this.editor.FirstVisibleLine = 0;
            this.editor.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.editor.Location = new System.Drawing.Point(1, 15);
            this.editor.Margin = new System.Windows.Forms.Padding(0);
            this.editor.Name = "editor";
            this.editor.Size = new System.Drawing.Size(501, 243);
            this.editor.TabIndex = 0;
            this.editor.TabWidth = 8;
            this.editor.ViewWidth = 1050;
            this.editor.CaretMoved += new System.EventHandler(this.editor_CaretMoved);
            this.editor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.editor_MouseMove);
            // 
            // ruler
            // 
            this.ruler.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ruler.Dock = System.Windows.Forms.DockStyle.Top;
            this.ruler.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ruler.ForeColor = System.Drawing.Color.DarkCyan;
            this.ruler.Location = new System.Drawing.Point(1, 1);
            this.ruler.Name = "ruler";
            this.ruler.SelectedIndex = 0;
            this.ruler.Size = new System.Drawing.Size(501, 14);
            this.ruler.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Controls.Add(this.editor);
            this.panel1.Controls.Add(this.ruler);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(1);
            this.panel1.Size = new System.Drawing.Size(503, 259);
            this.panel1.TabIndex = 2;
            // 
            // TextEditor
            // 
            this.ClientSize = new System.Drawing.Size(503, 259);
            this.Controls.Add(this.panel1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KeyPreview = true;
            this.Name = "TextEditor";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
            this.Text = "テキストエディタ";
            this.Load += new System.EventHandler(this.TextEditor_Load);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.TextEditor_PreviewKeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sgry.Azuki.Windows.AzukiControl editor;
        private TextRuler ruler;
        private System.Windows.Forms.Panel panel1;
    }
}
