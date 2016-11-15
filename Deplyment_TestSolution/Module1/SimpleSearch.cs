using System.Collections.Generic;

namespace Module1
{
    public class SimpleSearch
    {
        public string PluginInfo = "===============SimpleSearch=============== \n Descriptin: Contains method \n";

        public IEnumerable<string> RunSearch(string searchKey, List<string> testFileLinesList)
        {
            foreach (string line in testFileLinesList)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    if (line.Contains(searchKey))
                    {
                        yield return line;
                    }
                }
            }
        }
    }
}
