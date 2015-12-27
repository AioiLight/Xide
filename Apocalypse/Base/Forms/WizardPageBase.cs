using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Apocalypse.Base.Forms
{
    public partial class WizardPageBase : UserControl
    {
        [Browsable(true)]
        public string WizardTitle { get; set; }

        public WizardPageBase()
        {
            InitializeComponent();
        }
    }
}
