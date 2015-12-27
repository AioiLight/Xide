using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Apocalypse
{
    public class RuntimeManager
    {
        static readonly RuntimeManager _instance = new RuntimeManager();
        public static RuntimeManager Instance
        {
            get { return _instance; }
        }

        Dictionary<string, string> runtimes;

        public RuntimeManager()
        {
            runtimes = new Dictionary<string, string>();
            var rtmdir = Path.Combine(Application.StartupPath, Definition.RuntimeDir);
            var dirs = Directory.GetDirectories(rtmdir);
            foreach (var item in dirs)
            {
                var rtmFile = Path.Combine(item, Path.GetFileName(item) + "." + Definition.RuntimeExt);
                if (File.Exists(rtmFile))
                    runtimes.Add(Path.GetFileName(item), rtmFile);
            }
        }

        public string[] GetRuntimes()
        {
            return runtimes.Keys.ToArray<string>();
        }

        public Data.Runtime GetRuntime(string id)
        {
            if (runtimes.ContainsKey(id))
                return Data.Runtime.Load(runtimes[id]);
            else
                throw new InvalidOperationException("runtime id " + id + " isn't existed.");
        }

    }
}
