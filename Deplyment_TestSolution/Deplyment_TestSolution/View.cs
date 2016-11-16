using System;
using System.Collections.Generic;
using System.Linq;

namespace Deplyment_TestSolution
{
    public class View
    {
        public string FilePath;
        public string Key;

        public View(bool useUI = true)
        {
            if (useUI)
            {
                Console.Clear();
                Console.WriteLine("{0," + Console.WindowWidth / 2 + "}", "Welcome to data searcher\n\n");
                Console.WriteLine("Please enter full path to the test file:");
                FilePath = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("TestFile:{0}\n", FilePath);
                Console.WriteLine("Please enter the key to Search in the test file:");
                Key = Console.ReadLine();
                Console.WriteLine("Search Key:{0}\n", Key);
            }
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

        public static bool? AskForContinue()
        {
            Console.WriteLine("Press 'SPACE' to try again or 'ESCAPE' to exit");
            var keyInfo = Console.ReadKey(true);
            while (true)
            {
                if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    return true;
                }
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    return false;
                }
                keyInfo = Console.ReadKey(true);
            }
        }

    }
}
