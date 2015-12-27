using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Reveal.Forms
{
    public class DockForm : DockContent
    {
        public virtual ToolStripItem[] StatusItems { get { return null; } }

        private string _statusString = null;
        public string StatusString
        {
            get { return _statusString; }
            set
            {
                _statusString = value;
                if (StatusStringUpdated != null)
                    StatusStringUpdated.Invoke(value);
            }
        }
        public event Action<string> StatusStringUpdated;
    }
}
