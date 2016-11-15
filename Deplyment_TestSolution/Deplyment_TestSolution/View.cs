using System;
using System.Collections.Generic;
using System.Linq;

namespace Deplyment_TestSolution
{
    public class View
    {
        public string FilePath;
        public string Key;

        public View()
        {
            Console.WriteLine("Welcome to data searcher\n\n");
            Console.WriteLine("please enter full path to the test file:");
            FilePath = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("TestFile:{0}\n", FilePath);
            Console.WriteLine("please enter the key to Search in the test file:");
            Key = Console.ReadLine();
            Console.WriteLine("Search Key:{0}\n", Key);
        }

        public static void ShowResults(string methodDescription, string assemblyname, string pluginVersion, IEnumerable<string> result)
        {
            Console.WriteLine(methodDescription);
            Console.WriteLine(assemblyname);
            Console.WriteLine(pluginVersion);
            var lines = result as IList<string> ?? result.ToList();
            if (lines.Count > 0)
            {
                foreach (var line in lines)
                {
                    Console.WriteLine("FoundLine: {0}", line);
                }
            }
            else
            {
                Console.WriteLine("Did not found any lines");
            }
            Console.WriteLine();
        }

        public static void ShowError(string error)
        {
            Console.WriteLine(error);
        }

    }
}
