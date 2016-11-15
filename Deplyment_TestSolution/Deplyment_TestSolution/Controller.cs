using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

namespace Deplyment_TestSolution
{
   public class Controller
    {
       public List<string> TestFileLinesList = new List<string>();
       readonly string _pluginsPath = Directory.GetCurrentDirectory() + @"\Plugins";

       public Controller(string filePath)
       {
           GetText(filePath);
       }

       private async void GetText(string filePath)
       {
           try
           {
               using (var reader = new StreamReader(filePath))
               {
                   string line;
                   while ((line = await reader.ReadLineAsync()) != null)
                   {
                       TestFileLinesList.Add(line);
                   }
               }
           }
           catch (Exception e)
           {
               View.ShowError(e.ToString());
           }
       }

       private List<Type> Loadplugins()
       {
           string[] pluginFiles = Directory.GetFiles(_pluginsPath, "*.dll");
           var pluginsAssemblies = pluginFiles.Select(Assembly.LoadFile).ToList();
           var types = new List<Type>();
           foreach (var assembly in pluginsAssemblies)
           {
              types.AddRange(assembly.GetExportedTypes());               
           }
           return types;
       }

       public bool RunSearchInplugins(string searchKey)
       {
           List<Type> types = Loadplugins();
           bool result = false;
           foreach (var type in types.Where(e => e.Name.Contains("Search")))
           {
               dynamic c = Activator.CreateInstance(type);
               IEnumerable<string> pluginSearchResult = c.RunSearch(searchKey, TestFileLinesList);
               if (pluginSearchResult.LongCount() > 0)
               {
                   View.ShowResults(c.PluginInfo, type.Assembly.GetName().Name, type.Assembly.GetName().Version.ToString(), pluginSearchResult);
                   result = true;
               }
               
           }
           return result;
       }

       
    }
}
