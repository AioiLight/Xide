using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace Apocalypse.Data
{
    [XmlRoot("runtime")]
    public class Runtime
    {
        public static Runtime Load(string path)
        {
            var r = K.Snippets.Files.LoadXML<Runtime>(path, true);
            r.RuntimeFileName = Path.GetFileNameWithoutExtension(path);
            return r;
        }

        /// <summary>
        /// Runtime-definition file name (load by filename)
        /// </summary>
        [XmlIgnore()]
        public string RuntimeFileName { get; set; }

        /// <summary>
        /// Runtime's name (REQUIRED)
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Runtime's author
        /// </summary>
        [XmlElement("author")]
        public string Author { get; set; }

        /// <summary>
        /// Runtime distribution url
        /// </summary>
        [XmlElement("dist")]
        public string DistributedURL { get; set; }

        /// <summary>
        /// Runtime's version (REQUIRED)
        /// </summary>
        [XmlElement("ver")]
        public double Version { get; set; }

        /// <summary>
        /// Required lower xide version
        /// </summary>
        [XmlElement("req_ver")]
        public double ReqXideVer { get; set; }

        /// <summary>
        /// Required files (relative path)
        /// </summary>
        [XmlArray("req_files"), XmlArrayItem("file")]
        public string[] Required { get; set; }

        [XmlArray("res_files"), XmlArrayItem("file")]
        public string[] Resources { get; set; }

        /// <summary>
        /// Property definition file (REQUIRED)
        /// </summary>
        [XmlElement("propdef")]
        public string PropertyFile { get; set; }
        
        /// <summary>
        /// Template file (REQUIRED)
        /// </summary>
        [XmlElement("tmplfile")]
        public string Template { get; set; }

        /// <summary>
        /// Default stage num (If no set this prop, it will set Zero.)
        /// </summary>
        [XmlElement("defstage")]
        public int DefaultStage{get;set;}

        /// <summary>
        /// Stages definition
        /// </summary>
        [XmlArray("stages"), XmlArrayItem("stage")]
        public StageData[] Stages { get; set; }

    }
    /// <summary>
    /// Stage data class
    /// </summary>
    public class StageData
    {
        /// <summary>
        /// Stage's name
        /// </summary>
        /// <remarks>
        /// {0}:stage's num {1}:global num
        /// </remarks>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Create limitation of this stage 
        /// </summary>
        /// <remarks>
        /// if set zero, it arrows to create this stage limitless.
        /// </remarks>
        [XmlAttribute("limit")]
        public int CreateLimit { get; set; }

        /// <summary>
        /// Reflect property of this stage created
        /// </summary>
        [XmlElement("ref_prop")]
        public string ReflectProperty { get; set; }

        /// <summary>
        /// Reflect value for prop
        /// </summary>
        /// <remarks>
        /// if you want to reflect stage's numeric to integer property, you should set {0}.
        /// and you should obey the property's type's format.
        /// </remarks>
        [XmlElement("ref_prop_value")]
        public string ReflectPropertyValue { get; set; }

        /// <summary>
        /// Override layer
        /// </summary>
        [XmlArray("layers"), XmlArrayItem("layer")]
        public LayerData[] Layers { get; set; }

        public override string ToString()
        {
            if (this.CreateLimit > 0)
                return this.Name + "(最大 " + CreateLimit + " ステージまで)";
            else
                return this.Name;
        }
    }

    /// <summary>
    /// Layer data class
    /// </summary>
    public class LayerData
    {
        /// <summary>
        /// Layer's name
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Stage's size (chip width/height)
        /// </summary>
        [XmlIgnore()]
        public Size StageSize { get; set; }
        [XmlElement("chipsize")] //for serialization support
        public string _stageSize
        {
            get { return StageSize.Width + "," + StageSize.Height; }
            set
            {
                if (value == null)
                {
                    StageSize = new Size(0, 0);
                    return;
                }
                int v1, v2;
                value.GetTwoCSVInt(out v1, out v2);
                StageSize = new Size(v1, v2);
            }
        }

        /// <summary>
        /// Using chip definition file
        /// </summary>
        [XmlElement("chipdefine")]
        public string ChipDataDefine { get; set; }

        /// <summary>
        /// Default use chip picture
        /// </summary>
        [XmlElement("def_chippict")]
        public string DefaultChipPicture { get; set; }

        /// <summary>
        /// Referencing property for using chip picture
        /// </summary>
        [XmlElement("chippict_ref_prop")]
        public string DefaultChipPictureReflectProp { get; set; }

        /// <summary>
        /// Output split num
        /// </summary>
        [XmlElement("outputsplit")]
        public int OutputSplitNum { get; set; }

        /// <summary>
        /// Output param format
        /// </summary>
        /// <remarks>
        /// {0}:value {1}:row {2}:split {3}:stagenum {4}:stagenum_formatted {5}:stagenum_global
        /// </remarks>
        [XmlElement("outputformat")]
        public string OutputFormat { get; set; }
    }

}
