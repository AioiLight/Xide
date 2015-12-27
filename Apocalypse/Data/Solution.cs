using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;
namespace Apocalypse.Data
{
    [XmlRoot("solution")]
    public class Solution
    {
        List<Project> containProjects;

        [XmlElement("name")]
        public string SolutionName { get; set; }

        [XmlArray("projects"),XmlArrayItem("project")]
        public Project[] Projects
        {
            get { return this.containProjects.ToArray(); }
            set { containProjects.AddRange(value); }
        }

        public void AddProject(Project pj)
        {
            containProjects.Add(pj);
        }

        public void DeleteProject(Project pj)
        {
            containProjects.Remove(pj);
        }

        public Project LookupProject(int idx)
        {
            return containProjects[idx];
        }

        public void ReplaceProject(int orig, int dest)
        {
            var proj = containProjects[orig];
            containProjects.RemoveAt(orig);
            containProjects.Insert(dest, proj);
        }

        /// <summary>
        /// Solution parent directory
        /// </summary>
        /// <remarks>
        /// for internal management
        /// </remarks>
        [XmlIgnore()]
        public string SolutionDirectory { get; set; }

        List<string> containResouces;
        [XmlElement("resources")]
        public string[] Resources
        {
            get { return this.containResouces.ToArray(); }
            set { containResouces.AddRange(value); }
        }

        [XmlIgnore()]
        public Runtime UsingRuntimePackage { get; set; }
        [XmlElement("runtime")]
        public string _runtime
        {
            get { return UsingRuntimePackage.RuntimeFileName; }
            set
            {
                try
                {
                    UsingRuntimePackage = RuntimeManager.Instance.GetRuntime(value);
                }
                catch (Exception e)
                {
                    MessageBox.Show("ランタイムデータの読み込みに失敗しました。" + Environment.NewLine + e.Message,
                        "ランタイム ロードエラー",
                        MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    throw;
                }
            }
        }

        public Solution()
        {
            containProjects = new List<Project>();
            containResouces = new List<string>();
        }

        public static Solution SolutionReader(string path)
        {
            try
            {
                var sln = K.Snippets.Files.LoadXML<Solution>(path, true);
                sln.SolutionDirectory = System.IO.Path.GetDirectoryName(path);
                return sln;
            }
            catch (Exception e)
            {
                string msg = e.Message;
                if (e.GetBaseException() != null)
                    msg = e.GetBaseException().Message;
                MessageBox.Show("ソリューションの読み込みに失敗しました。" + Environment.NewLine + msg,
                    "ソリューション ロードエラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return null;
        }
    }
}
