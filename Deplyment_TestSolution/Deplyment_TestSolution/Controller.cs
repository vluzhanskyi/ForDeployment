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
       private readonly string _pluginsPath = Directory.GetCurrentDirectory() + @"\Plugins";
       public View View;
       public Controller(bool useUI = true)
       {
           View = new View(useUI);
            GetText(View.FilePath);      
       }

       private async void GetText(string filePath)
       {
           try
           {
               if (!string.IsNullOrEmpty(filePath))
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
               else
               {
                   View.ShowError("FilePath is empty");
               }
           }
           catch (FileNotFoundException)
           {
               View.ShowError(string.Format("Could not found '{0}' file", View.FilePath));
           }
           catch (Exception e)
           {
               View.ShowError(e.ToString());
           }
       }

       private List<Type> Loadplugins()
       {
           var pluginsAssemblies = new List<Assembly>();
           try
           {
               string[] plagins = Directory.GetFiles(_pluginsPath, "*.dll");
               pluginsAssemblies = plagins.Select(Assembly.LoadFile).ToList();
           }
           catch (DirectoryNotFoundException)
           {
               View.ShowError("Could not fould Plugins folder");
           }
           catch (FileNotFoundException)
           {
               View.ShowError("There no *.dll files in the Plugins folder");
           }
           catch (Exception e)
           {
               View.ShowError(e.Message);
           }
           var types = new List<Type>();

           foreach (var assembly in pluginsAssemblies)
           {
              types.AddRange(assembly.GetExportedTypes());               
           }
           return types;
       }

       public bool RunSearchInplugins()
       {
           List<Type> types = Loadplugins();
           bool result = false;

           foreach (var type in types.Where(e => e.Name.Contains("Search")))
           {
               dynamic c = Activator.CreateInstance(type);
               IEnumerable<string> pluginSearchResult = c.RunSearch(View.Key, TestFileLinesList);
               if (pluginSearchResult.LongCount() > 0)
               {
                   Deplyment_TestSolution.View.ShowResults(c.PluginInfo, type.Assembly.GetName().Name,
                                                            type.Assembly.GetName().Version.ToString(), 
                                                            pluginSearchResult);
                   result = true;
               }
               
           }
           return result;
       }
    }
}
