using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Deplyment_TestSolution;

namespace Deplyment_TestSolutionTests
{
    [TestClass()]
    public class ControllerTests
    {
        private readonly Controller _testController = new Controller(false);
        private readonly string _testFilePath = string.Format("{0}\\test.txt", Directory.GetCurrentDirectory());
        public ControllerTests()
        {
            var content = new List<string> {
                null,
                "",
                "                       ",
                "TEST",
                "Test",
                "  t     E   sT     TE   S    t"
            };
            CreateFile(_testFilePath, content);
            _testController.MyView.FilePath = _testFilePath;
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
                RenameModule("Module2.test", "Module2.dll");
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
                RenameModule("Module1.test", "Module1.dll");
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

        private void CreateFile(string path, List<string> content)
        {
            using(StreamWriter writer = File.CreateText(path))
            {
                foreach(var line in content)
                {
                    writer.WriteLine(line);
                }
                writer.Close();
            }
        }
    }
}
