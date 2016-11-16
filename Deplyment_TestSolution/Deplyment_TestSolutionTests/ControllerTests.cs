using System.IO;
using Deplyment_TestSolution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Deplyment_TestSolutionTests
{
    [TestClass()]
    public class ControllerTests
    {
        private readonly Controller _testController = new Controller(false);
        public ControllerTests()
        {
            _testController.TestFileLinesList.Add(null);
            _testController.TestFileLinesList.Add("");
            _testController.TestFileLinesList.Add("                       ");
            _testController.TestFileLinesList.Add("TEST");
            _testController.TestFileLinesList.Add("Test");
            _testController.TestFileLinesList.Add("  t     E   sT     TE   S    t");
        }
        [TestMethod()]
        public void RunSearchInpluginsWhiteSpacesTest()
        {
            _testController.MyView.Key = "";

            if (!_testController.RunSearchInplugins())
            {
                Assert.Fail();
            }
            _testController.MyView.Key = " ";
            if (!_testController.RunSearchInplugins())
            {
                Assert.Fail();
            }
           
        }

        [TestMethod()]
        public void RunSearchInpluginsInvariantCultureTest()
        {
            _testController.MyView.Key = "test";
            if (!_testController.RunSearchInplugins())
            {
                Assert.Fail();
            }
            _testController.MyView.Key = "Test";
            if (!_testController.RunSearchInplugins())
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public void CheckModule1()
        {
            _testController.MyView.Key = "test";
            RenameModule("Module2.dll", "Module2.test");
            if (_testController.RunSearchInplugins())
            {
                Assert.Fail();
            }
            RenameModule("Module2.test", "Module2.dll");
        }

        [TestMethod]
        public void CheckModule2()
        {
            _testController.MyView.Key = "test";
            RenameModule("Module1.dll", "Module1.test");
            if (!_testController.RunSearchInplugins())
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
