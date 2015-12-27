using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apocalypse
{
    public class SolutionHolder
    {
        static SolutionHolder _instance = new SolutionHolder();
        public static SolutionHolder Instance
        {
            get { return _instance; }
        }

        public Data.Solution OpeningSolution { get; set; }
        public event Action SolutionUpdated;
        public void SolutionUpdateNotify()
        {
            if (SolutionUpdated != null)
                SolutionUpdated.Invoke();
        }
    }
}
