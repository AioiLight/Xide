using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Apocalypse.Docks.Editor.Browser
{
    public partial class Browser : Reveal.Forms.DocumentDockForm
    {
        ToolStrip toolStrip;
        public override ToolStrip[] Toolbars
        {
            get
            {
                return new[] { toolStrip };
            }
        }
        Reveal.Forms.UIReflectController uic = new Reveal.Forms.UIReflectController();
        public override Reveal.Forms.UIReflectController UIController
        {
            get
            {
                return uic;
            }
        }
        public Browser()
        {
            InitializeComponent();
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.StatusTextChanged += new EventHandler(webBrowser_StatusTextChanged);
            toolStrip = new ToolStrip();
            var brwNaviGoBack = new ToolStripButton("戻る", Properties.Resources.nav_back, new EventHandler((o, e) => webBrowser.GoBack()));
            brwNaviGoBack.Enabled = false;
            toolStrip.Items.Add(brwNaviGoBack);
            var brwNaviGoForward = new ToolStripButton("進む", Properties.Resources.nav_forward, new EventHandler((o, e) => webBrowser.GoForward()));
            brwNaviGoForward.Enabled = false;
            toolStrip.Items.Add(brwNaviGoForward);
            var brwNaviRefresh = new ToolStripButton("更新", Properties.Resources.arrow_refresh, new EventHandler((o, e) => webBrowser.Refresh()));
            toolStrip.Items.Add(brwNaviRefresh);
            webBrowser.CanGoBackChanged += new EventHandler((o, e) => brwNaviGoBack.Enabled = webBrowser.CanGoBack);
            webBrowser.CanGoForwardChanged += new EventHandler((o, e) => brwNaviGoForward.Enabled = webBrowser.CanGoForward);
            uic.SetEnabled(Reveal.Forms.UIReflectController.UIElements.Deletable, false);
            uic.SetEnabled(Reveal.Forms.UIReflectController.UIElements.EditEnabled, false);
            uic.SetEnabled(Reveal.Forms.UIReflectController.UIElements.Redoable, false);
            uic.SetEnabled(Reveal.Forms.UIReflectController.UIElements.Savable, false);
            uic.SetEnabled(Reveal.Forms.UIReflectController.UIElements.SelectAllCapable, false);
            uic.SetEnabled(Reveal.Forms.UIReflectController.UIElements.SelectAllCapable, false);
            uic.SetEnabled(Reveal.Forms.UIReflectController.UIElements.Undoable, false);
        }

        ToolStripStatusLabel tssl = new ToolStripStatusLabel() { Text = "", Spring = true };
        ToolStripProgressBar tspb = new ToolStripProgressBar() { Value = 0, Maximum = 100, Alignment = ToolStripItemAlignment.Right, Visible = false };
        public override ToolStripItem[] StatusItems
        {
            get { return new ToolStripItem[] { tssl, tspb }; }
        }

        void webBrowser_CanGoBackChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        string init = "";
        public Browser(string target)
            : this()
        {
            init = target;
        }

        private void browseUri_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!browseUri.Items.Contains(browseUri.Text))
                    browseUri.Items.Add(browseUri.Text);
                webBrowser.Navigate(browseUri.Text);
            }
        }

        private void Browser_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(init))
                webBrowser.Navigate(init);
        }

        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            tspb.Visible = true;
            browseUri.Text = e.Url.OriginalString;
        }

        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            this.tspb.Visible = true;
            browseUri.Text = e.Url.OriginalString;
            if (!browseUri.Items.Contains(e.Url.OriginalString))
                browseUri.Items.Add(e.Url.OriginalString);
        }

        private void webBrowser_BeforeNewWindow(object sender, K.Controls.WebBrowserExtendedNavigatingEventArgs e)
        {
            e.Cancel = true;
            Core.Instance.MainFormManager.AppendChildForm(new Browser(e.Url));
        }

        private void browseUri_SelectedIndexChanged(object sender, EventArgs e)
        {
            webBrowser.Navigate(browseUri.Text);
        }

        private void webBrowser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            this.tspb.Value = (int)(e.CurrentProgress * 100 / e.MaximumProgress);
        }

        void webBrowser_StatusTextChanged(object sender, EventArgs e)
        {
            this.StatusString = webBrowser.StatusText;
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.tspb.Visible = false;
            this.BuildTitle();
        }

        public void BuildTitle()
        {
            StringBuilder sb = new StringBuilder(10);
            sb.Append("[");
            if (!string.IsNullOrEmpty(webBrowser.Document.Title))
                sb.Append(webBrowser.Document.Title);
            else
                sb.Append("不明");
            sb.Append("] - ブラウザ");
            this.Text = sb.ToString();
        }

    }
}
