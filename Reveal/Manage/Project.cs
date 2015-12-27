using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reveal.Manage
{
    public class Project
    {
        public Project()
        {
            mapData = new Map();
        }

        public Project(string proj):this()
        {

        }

        bool IsModified { get; set; }
        public string ProjName { get; set; }
        private Map mapData;
        public Map Map { get { return mapData; } }
    }
}
