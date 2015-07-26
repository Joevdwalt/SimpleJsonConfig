using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleJsonConfig.Test
{
    [TestClass]
    public class PathProviderTest
    {

        PathProvider PathProvider { get; set; }

        public PathProviderTest()
        {
            this.PathProvider = new PathProvider();
            //override base for testing
            this.PathProvider.CurrentPath = "foo";
        }

        [TestMethod]
        public void GetConfigPath_DefaultFolder()
        {
            var expectedPath = "foo\\default";
            var environment = "default";

            var actualPath = this.PathProvider.GetConfigPath(environment);

            Assert.AreEqual(expectedPath, actualPath);
        }

        [TestMethod]
        public void GetConfigPath_RootDefaultFolder()
        {
            this.PathProvider = new SimpleJsonConfig.PathProvider();
            this.PathProvider.RootPath = "root";
            this.PathProvider.CurrentPath = "foo";
            var expectedPath = "foo\\root\\default";
            var environment = "default";
            var actualPath = this.PathProvider.GetConfigPath(environment);

            Assert.AreEqual(expectedPath, actualPath);
        }
    }
}
