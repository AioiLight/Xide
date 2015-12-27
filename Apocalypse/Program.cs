using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Office2007Renderer;

namespace Apocalypse
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ToolStripManager.Renderer = new Office2007Renderer.Office2007Renderer(new Office2007BlueColorTable()); 
            Application.Run(new MainForm());
        }
    }
}
