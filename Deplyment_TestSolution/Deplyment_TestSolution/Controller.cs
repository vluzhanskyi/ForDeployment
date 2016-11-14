using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Deplyment_TestSolution
{
   public class Controller
    {
       public List<string> TestFileLinesList = new List<string>();

       public Controller(string filePath)
       {
           GetText(filePath);
       }

       private async void GetText(string filePath)
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

       private List<Type> Loadplugins()
       {
           string path = Directory.GetCurrentDirectory() + @"\Plugins";
           string[] pluginFiles = Directory.GetFiles(path, "*.dll");
           var pluginsAssemblies = pluginFiles.Select(Assembly.LoadFile).ToList();
           var types = new List<Type>();
           foreach (var assembly in pluginsAssemblies)
           {
              types.AddRange(assembly.GetExportedTypes());               
           }
           return types;
       }

       public void RunSearchInplugins(string searchKey)
       {
           List<Type> types = Loadplugins();

           foreach (var type in types.Where(e => e.Name.Contains("Search")))
           {
               dynamic c = Activator.CreateInstance(type);
               var result = c.RunSearch(searchKey, TestFileLinesList);
           }
       }
    }
}
