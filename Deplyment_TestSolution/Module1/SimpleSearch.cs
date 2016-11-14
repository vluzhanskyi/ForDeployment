using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module1
{
    public class SimpleSearch
    {
        public string PluginInfo = "============SimpleSearch============ \n Descriptin: Contains method \n";

        public string RunSearch(string searchKey, List<string> testFileLinesList)
        {
            string resunlt = null;

            foreach (string line in testFileLinesList)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    if (line.Contains(searchKey))
                    {
                        resunlt = line;
                    }
                }
            }

            return resunlt;
        }
    }
}
