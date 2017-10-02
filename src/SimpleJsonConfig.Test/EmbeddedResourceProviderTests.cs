using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleJsonConfig.Providers;

namespace SimpleJsonConfig.Test
{
    [TestClass]
    public class EmbeddedResourceProviderTests
    {
        private static IJsonSourceProvider BuildProvider()
        {
            var provider = new EmbeddedResourceJsonProvider(typeof(ConfigReaderTests.Person),
                "SimpleJsonConfig.Test.embedded_resource",
                "default.json");
            return provider;
        }

        [TestMethod]
        public void Embedded_GetSetting_NoEnviroment_TypeOfString()
        {
            var configReader = new ConfigReader(BuildProvider());

            const string expectedValue = "TestValue";
            var actualValue = configReader.GetSetting<string>("TestKey");

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void Embedded_GetSetting_NoEnviroment_TypeOfPerson()
        {
            var configReader = new ConfigReader(BuildProvider());
            var expectedValue = new ConfigReaderTests.Person {Name = "foo", Surname = "bar"};
            var actualValue = configReader.GetSetting<ConfigReaderTests.Person>("TestObjectKey");

            Assert.AreEqual(expectedValue.Name, actualValue.Name);
            Assert.AreEqual(expectedValue.Surname, actualValue.Surname);
        }

        [TestMethod]
        public void Embedded_GetSetting_KeyDoesNotExits()
        {
            var configReader = new ConfigReader(BuildProvider());
            string expectedValue = null;
            var actualValue = configReader.GetSetting<string>("DoesNotExit");

            Assert.AreEqual(actualValue, expectedValue);
        }

        public class Person
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }
    }
}