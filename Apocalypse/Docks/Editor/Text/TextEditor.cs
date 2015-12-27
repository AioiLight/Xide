using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Apocalypse.Docks.Editor.Text
{
    public partial class TextEditor : Reveal.Forms.DocumentDockForm
    {
        public TextEditor()
        {
            InitializeComponent();
        }

        private void TextEditor_Load(object sender, EventArgs e)
        {
            ruler.GetHeader = new TextRuler.IndexToPointResolver(() => editor.GetPositionFromIndex(0).X);
        }

        private void editor_CaretMoved(object sender, EventArgs e)
        {
            int x, y;
            editor.GetLineColumnIndexFromCharIndex(editor.CaretIndex, out y, out x);
            ruler.SelectedIndex = x;
        }

        private void TextEditor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            ruler.Refresh();
        }

        private void editor_MouseMove(object sender, MouseEventArgs e)
        {
            ruler.Refresh();
        }
    }
}
