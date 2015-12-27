using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;

namespace Reveal.Manage
{
    [XmlRoot("chipsdef")]
    public class ChipDataContainer
    {
        [XmlArray("chips"), XmlArrayItem("chip")]
        public ChipData[] ChipDatas = null;
        [XmlAttribute("xdpictpath")]
        public string ExtraDrawSrc = "[ExtraDraw画像ファイルのパスを記述]";

        public static ChipDataContainer Deserialize(string path)
        {
            return K.Snippets.Files.LoadXML<ChipDataContainer>(path, true);
        }
    }

    public class ChipData
    {
        [XmlAttribute("str")]
        public string LinkStr = null;
        [XmlAttribute("rel")]
        public string RelatedCog = null;
        [XmlArray("datas"), XmlArrayItem("data")]
        public ChipDataContent[] Contents = null;
        public Color Color = Color.Black;
        [XmlAttribute("clr")]
        public string _colorConv
        {
            get { return ColorTranslator.ToHtml(this.Color); }
            set
            {
                if (value == null)
                    this.Color = Color.Black;
                else if (value.StartsWith("#"))
                    this.Color = ColorTranslator.FromHtml(value);
                else
                    this.Color = Color.FromName(value);
            }
        }

        public class ChipDataContent
        {
            [XmlAttribute("relval")]
            public string RelValue = null;
            [XmlElement("name")]
            public string Name = null;
            [XmlElement("desc")]
            public string Description = null;
            [XmlIgnore()]
            public Rectangle DrawClipRect = new Rectangle();
            [XmlElement("loc")]
            public string _drawClipPos
            {
                get { return DrawClipRect.X + "," + DrawClipRect.Y; }
                set
                {
                    if (value == null)
                    {
                        DrawClipRect.X = 0;
                        DrawClipRect.Y = 0;
                        return;
                    }
                    int v1, v2;
                    value.GetTwoCSVInt(out v1, out v2);
                    DrawClipRect.X = v1;
                    DrawClipRect.Y = v2;
                }
            }
            [XmlElement("size")]
            public string _drawClipSize
            {
                get { return DrawClipRect.Width + "," + DrawClipRect.Height; }
                set
                {
                    if (value == null)
                    {
                        DrawClipRect.Width = 0;
                        DrawClipRect.Height = 0;
                        return;
                    }
                    int v1, v2;
                    value.GetTwoCSVInt(out v1, out v2);
                    DrawClipRect.Width = v1;
                    DrawClipRect.Height = v2;
                }
            }
            [XmlIgnore()]
            public Point CenterPoint = new Point();
            [XmlElement("center")]
            public string _centerPoint
            {
                get { return CenterPoint.X + "," + CenterPoint.Y; }
                set
                {
                    if (value == null)
                    {
                        CenterPoint.X = 0;
                        CenterPoint.Y = 0;
                        return;
                    }
                    int v1, v2;
                    value.GetTwoCSVInt(out v1, out v2);
                    CenterPoint.X = v1;
                    CenterPoint.Y = v2;
                }
            }
            [XmlElement("extra")]
            public ExtraDrawData ExtraDraw = new ExtraDrawData();
            public class ExtraDrawData
            {
                [XmlIgnore()]
                bool Enabled = false;
                [XmlAttribute("backdraw")]
                public bool BackgroundDraw = false;
                [XmlIgnore()]
                public Point DrawPos = new Point(-1, -1);
                [XmlText()]
                public string _drawPos
                {
                    get { return DrawPos.X + "," + DrawPos.Y; }
                    set
                    {
                        if (value == null)
                        {
                            DrawPos.X = -1;
                            DrawPos.Y = -1;
                            this.Enabled = false;
                            return;
                        }
                        int v1, v2;
                        value.GetTwoCSVInt(out v1, out v2);
                        this.Enabled = v1 > 0 && v2 > 0;
                        DrawPos.X = v1;
                        DrawPos.Y = v2;
                    }
                }
            }
        }
    }

    public static class StringExtender
    {
        public static void GetTwoCSVInt(this string csv, out int val1, out int val2)
        {
            var c = csv.Split(',');
            if (c.Length != 2)
                throw new Exception("Invalid num format in " + csv);
            if (!int.TryParse(c[0], out val1))
                throw new Exception("Numeric parse error(1) in " + csv);
            if (!int.TryParse(c[1], out val2))
                throw new Exception("Numeric parse error(2) in " + csv);
        }
    }
}
