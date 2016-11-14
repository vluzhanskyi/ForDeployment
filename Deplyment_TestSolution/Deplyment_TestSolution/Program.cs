using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deplyment_TestSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller c = new Controller("test.txt");
            c.RunSearchInplugins("Test");
            Console.ReadLine();
        }
    }
}
