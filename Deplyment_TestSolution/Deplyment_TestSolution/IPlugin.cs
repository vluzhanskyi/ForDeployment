using System.Collections.Generic;

namespace Deplyment_TestSolution
{
    public interface IPlugin
    {
        string PluginInfo { get;}

        IEnumerable<string> RunSearch(string filePath, string searchKey);

    }
}
