using System;
using System.IO;
using System.Linq;
using Deplyment_TestSolution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Deplyment_TestSolutionTests
{
    [TestClass()]
    public class ControllerTests
    {
        readonly Controller _cNullTest = new Controller(null);

        public ControllerTests()
        {
            _cNullTest.TestFileLinesList.Add(null);
            _cNullTest.TestFileLinesList.Add("");
            _cNullTest.TestFileLinesList.Add("                       ");
            _cNullTest.TestFileLinesList.Add("TEST");
            _cNullTest.TestFileLinesList.Add("Test");
            _cNullTest.TestFileLinesList.Add("  t     E   sT     TE   S    t");
        }
        [TestMethod()]
        public void RunSearchInpluginsWhiteSpacesTest()
        {
            
            if (!_cNullTest.RunSearchInplugins(""))
            {
                Assert.Fail();
            }
            if (!_cNullTest.RunSearchInplugins(" "))
            {
                Assert.Fail();
            }
           
        }

        [TestMethod()]
        public void RunSearchInpluginsInvariantCultureTest()
        {
            if (!_cNullTest.RunSearchInplugins("test"))
            {
                Assert.Fail();
            }
            if (!_cNullTest.RunSearchInplugins("Test"))
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public void CheckModule1()
        {
            RenameModule("Module2.dll", "Module2.test");
            if (_cNullTest.RunSearchInplugins("test"))
            {
                Assert.Fail();
            }
            RenameModule("Module2.test", "Module2.dll");
        }

        [TestMethod]
        public void CheckModule2()
        {
            RenameModule("Module1.dll", "Module1.test");
            if (!_cNullTest.RunSearchInplugins("test"))
            {
                Assert.Fail();
            }
            RenameModule("Module1.test", "Module1.dll");
        }

        private void RenameModule(string moduleName, string expectedName)
        {
            var pluginsFolder = Directory.GetCurrentDirectory() + @"\Plugins";
            var plugins = Directory.GetFiles(pluginsFolder);
            foreach (var plugin in plugins)
            {
                if (!plugin.Contains(moduleName)) continue;
                var file = Path.GetFullPath(plugin);
                {
                    var newFileName = file.Replace(moduleName, expectedName);
                    File.Move(file, newFileName);
                }
            }
        }
    }
}
