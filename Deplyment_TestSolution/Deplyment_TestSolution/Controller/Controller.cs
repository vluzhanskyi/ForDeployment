using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Deplyment_TestSolution.Views;

namespace Deplyment_TestSolution.Controller
{
   public class Controller
    {
       private readonly string _pluginsPath = Directory.GetCurrentDirectory() + @"\Plugins";
       public View MyView;
       public Controller(bool useUi = true)
       {
           MyView = new View(useUi);    
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
               MyView.ShowError("Could not fould Plugins folder");
           }
           catch (FileNotFoundException)
           {
               MyView.ShowError("There no *.dll files in the Plugins folder");
           }
           catch (Exception e)
           {
               MyView.ShowError(e.Message);
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
                if (!string.IsNullOrEmpty(MyView.FilePath))
                {
                    try
                    {
                        IEnumerable<string> pluginSearchResult = c.RunSearch(MyView.FilePath, MyView.Key);
                        if (pluginSearchResult.LongCount() > 0)
                        {
                            MyView.ShowResults(c.PluginInfo, type.Assembly.GetName().Name,
                                                                     type.Assembly.GetName().Version.ToString(),
                                                                     pluginSearchResult);
                            result = true;
                        }
                    } catch(Exception e)
                    {
                        MyView.ShowError("Fail to load plugin: \n" + e.Message);
                    }
                    
                } else
                {
                    MyView.ShowError("Invalid data. Please try again");
                }
               
           }
           return result;
       }
    }
}
