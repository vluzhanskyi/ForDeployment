using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2
{
    public class SearchWithoutRegister
    {
        public string RunSearch(string searchKey, List<string> testFileLinesList)
        {
            string resunlt = null;

            foreach (string line in testFileLinesList)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    if (line.IndexOf(searchKey, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    {
                        resunlt = line;
                    }
                }
            }

            return resunlt;
        }

      
    }
}
