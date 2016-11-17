using Deplyment_TestSolution;
using System;
using System.Collections.Generic;
using System.IO;

namespace Module2
{
    public class SearchWithoutRegister : IPlugin
    {
        string IPlugin.PluginInfo
        {
            get
            {
                return "============SimpleSearchWithoutRegister============ \n Descriptin: IndexOf searchKey" +
                                   " with StringComparison.InvariantCultureIgnoreCase \n";
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
