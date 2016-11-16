using System.Collections.Generic;

namespace Deplyment_TestSolution.Views
{
    public interface IView
    {
        string FilePath { get; set; }
        string Key { get; set; }

        void ShowResults(string methodDescription, string assemblyname, string pluginVersion, IEnumerable<string> result);

        void ShowError(string error);
    }
}