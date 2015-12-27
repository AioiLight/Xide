using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Apocalypse.Data
{
    public class Project
    {
        public Project()
        {
            mapFiles = new List<Map>();
            scriptFiles = new List<string>();
            prop = null;
        }

        public static Project ProjectReader(string proj)
        {
            return K.Snippets.Files.LoadXML<Project>(proj, true);
        }

        bool IsModified { get; set; }
        [XmlAttribute("name")]
        public string ProjName { get; set; }

        private List<Map> mapFiles;
        [XmlArray("maps"), XmlArrayItem("map")]
        public Map[] Map
        {
            get
            {
                return mapFiles.ToArray();
            }
            set
            {
                mapFiles.Clear();
                if (value != null)
                    mapFiles.AddRange(value);
            }
        }
        public void AddMap(Map map)
        {
            mapFiles.Add(map);
        }
        public void RemoveMap(Map map)
        {
            mapFiles.Remove(map);
        }

        private List<string> scriptFiles { get; set; }
        [XmlArray("scripts"), XmlArrayItem("script")]
        public string[] ScriptFiles
        {
            get { return scriptFiles.ToArray(); }
            set
            {
                scriptFiles.Clear();
                if (value != null)
                    scriptFiles.AddRange(value);
            }
        }

        private IProperty prop;
        [XmlIgnore()]
        public IProperty Property
        {
            get { return prop; }
        }
        public string PropertySerializer
        {
            get { return null; }
            set { }
        }
    }

    public interface IProperty { }
}
