using System;
using System.Collections.Generic;

namespace Module2
{
    public class SearchWithoutRegister
    {
        public string PluginInfo = "============SimpleSearchWithoutRegister============ \n Descriptin: IndexOf searchKey" +
                                   " with StringComparison.InvariantCultureIgnoreCase \n";

        public IEnumerable<string> RunSearch(string searchKey, List<string> testFileLinesList)
        {
            foreach (string line in testFileLinesList)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    var tempLine = line.Replace(" ", "");
                    if (tempLine.IndexOf(searchKey, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    {
                        yield return line;
                    }
                }
            }
        }

      
    }
}
