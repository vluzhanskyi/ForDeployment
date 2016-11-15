using System;

namespace Deplyment_TestSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            View view = new View();

            Controller c = new Controller(view.FilePath);
            c.RunSearchInplugins(view.Key);
            Console.ReadLine();
        }
    }
}
