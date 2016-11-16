using System;
using System.Threading;
using Deplyment_TestSolution.Views;

namespace Deplyment_TestSolution
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                var c = new Controller();
                c.RunSearchInplugins();

                if (View.AskForContinue() == true)
                {
                    continue;
                }
                break;
            }
        }
    }
}
