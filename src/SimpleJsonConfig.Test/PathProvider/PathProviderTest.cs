using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleJsonConfig.Test.PathProvider
{
    [TestClass]
    public class PathProviderTest
    {
        private SimpleJsonConfig.PathProvider.PathProvider PathProvider { get; set; }

        public PathProviderTest()
        {
            this.PathProvider = new SimpleJsonConfig.PathProvider.PathProvider
            {
                CurrentPath = "foo"
            };
            //override base for testing
        }

        [TestMethod]
        public void GetConfigPath_DefaultFolder()
        {
            const string expectedPath = "foo\\default";
            const string environment = "default";

            var actualPath = this.PathProvider.GetConfigPath(environment);

            Assert.AreEqual(expectedPath, actualPath);
        }

        [TestMethod]
        public void GetConfigPath_RootDefaultFolder()
        {
            this.PathProvider = new SimpleJsonConfig.PathProvider.PathProvider
            {
                RootPath = "root", CurrentPath = "foo"
            };
            const string expectedPath = "foo\\root\\default";
            const string environment = "default";
            var actualPath = this.PathProvider.GetConfigPath(environment);

            Assert.AreEqual(expectedPath, actualPath);
        }
    }
}
