using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deplyment_TestSolution
{
    public interface IPlugin
    {
        string PluginInfo { get; }

        IEnumerable<string> RunSearch(string filePath, string searchKey);

    }
}
