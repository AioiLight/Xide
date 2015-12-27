using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace K.Snippets
{
    public static partial class WinForms
    {
        public class ToolStripInfo : IComparable<ToolStripInfo>
        {
            public string Name = "";
            public Point Location = Point.Empty;
            public bool isVisible = false;

            public ToolStripInfo(ToolStrip ts)
            {
                this.Name = ts.Name;
                this.Location = ts.Location;
                this.isVisible = ts.Visible;
            }

            public ToolStripInfo()
            {
            }

            public int CompareTo(ToolStripInfo other)
            {
                if (this.Location.X == other.Location.X)
                {
                    return this.Location.Y - other.Location.Y;
                }
                return this.Location.X - other.Location.X;
            }
        }

        public static Dictionary<string,List<List<ToolStripInfo>>> GetState(Form parent)
        {
            //owner内のToolStripPanelを探す
            List<Control> toolStripPanels = new List<Control>();
            FindControls(typeof(ToolStripPanel), parent.Controls,
                ref toolStripPanels);
            Dictionary<string, List<List<ToolStripInfo>>> retDict = new Dictionary<string, List<List<ToolStripInfo>>>();
            foreach (ToolStripPanel tsp in toolStripPanels)
            {
                if (string.IsNullOrEmpty(tsp.Name)) continue;

                List<List<ToolStripInfo>> rowsList =
                    new List<List<ToolStripInfo>>();

                foreach (ToolStripPanelRow r in tsp.Rows)
                {
                    //ToolStripPanelの列内のToolStripの情報を収集
                    List<ToolStripInfo> toolStripNames =
                        new List<ToolStripInfo>();
                    foreach (Control con in r.Controls)
                    {
                        if (con is ToolStrip &&
                            !string.IsNullOrEmpty(con.Name))
                        {
                            toolStripNames.Add(
                                new ToolStripInfo((ToolStrip)con));
                        }
                    }
                    //列内の順番を並び替え
                    toolStripNames.Sort();
                    rowsList.Add(toolStripNames);
                }

                //ToolStripPanelごとに保存する
                retDict.Add(parent.GetType().FullName + "." + tsp.Name, rowsList);
            }
            return retDict;
        }

        public static void ReflectState(Form parent, Dictionary<string, List<List<ToolStripInfo>>> infodict)
        {
            //owner内のToolStripPanelを探す
            List<Control> toolStripPanels = new List<Control>();
            FindControls(typeof(ToolStripPanel), parent.Controls,
                ref toolStripPanels);

            foreach (ToolStripPanel tsp in toolStripPanels)
            {
                if (string.IsNullOrEmpty(tsp.Name)) continue;

                //ToolStripPanelの情報を読み込む
                var rowsList = infodict[parent.GetType().FullName + "." + tsp.Name];

                //ToolStripPanel内のToolStripを一時的にすべて削除する
                Dictionary<string, ToolStrip> toolstrips =
                    new Dictionary<string, ToolStrip>();
                foreach (Control c in tsp.Controls)
                {
                    toolstrips.Add(c.Name, (ToolStrip)c);
                }
                tsp.Controls.Clear();

                for (int i = 0; i < rowsList.Count; i++)
                {
                    foreach (ToolStripInfo info in rowsList[i])
                    {
                        //位置を設定するToolStripを探す
                        ToolStrip ts = null;
                        if (toolstrips.ContainsKey(info.Name))
                        {
                            ts = toolstrips[info.Name];
                        }
                        else
                        {
                            Control[] tss = parent.Controls.Find(info.Name, true);
                            if ((tss != null) && (tss.Length == 1)
                                && (tss[0] is ToolStrip))
                            {
                                ts = (ToolStrip)tss[0];
                            }
                        }
                        //ToolStripの位置を変更する
                        if (ts != null)
                        {
                            tsp.Join(ts, info.Location);
                            ts.Visible = info.isVisible;
                        }
                    }
                }
            }
        }

        private static void FindControls(Type findType,
            Control.ControlCollection conts, ref List<Control> foundList)
        {
            foreach (Control c in conts)
            {
                if (findType.IsAssignableFrom(c.GetType()))
                {
                    foundList.Add(c);
                }
                if (c.Controls.Count > 0)
                {
                    FindControls(findType, c.Controls, ref foundList);
                }
            }
        }
    }
}
