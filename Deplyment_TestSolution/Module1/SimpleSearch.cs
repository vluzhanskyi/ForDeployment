using Deplyment_TestSolution;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Module1
{
    public class SimpleSearch : IPlugin
    {
        string IPlugin.PluginInfo
        {
            get
            {
                return "===============SimpleSearch=============== \n Descriptin: Contains method \n";
            }
        }

        private IEnumerable<string> GetTextLines(string filePath)
        {
            var testFileLinesList = new List<string>();
            if (!string.IsNullOrEmpty(filePath))
            {
                using (var reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        testFileLinesList.Add(line);
                    }
                    reader.Close();
                }
            }

            return testFileLinesList;
        }
        public IEnumerable<string> RunSearch(string filePath, string searchKey)
        {
            var testFileLinesList = GetTextLines(filePath);

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
