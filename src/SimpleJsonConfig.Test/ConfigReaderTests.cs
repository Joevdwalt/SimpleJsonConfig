using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleJsonConfig.Test
{
    [TestClass]
    public class ConfigReaderTests
    {
        [TestMethod]
        public void GetSetting_NoEnviroment_TypeOfString()
        {
            var configReader = new ConfigReader();
            const string expectedValue = "TestValue";
            var actualValue = configReader.GetSetting<string>("TestKey");

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public async Task GetSetting_NoEnviroment_TypeOfStringAsync()
        {
            var configReader = new ConfigReader();
            const string expectedValue = "TestValue";
            var actualValue = await configReader.GetSettingAsync<string>("TestKey");

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void GetSetting_NoEnviroment_TypeOfPerson()
        {
            var configReader = new ConfigReader();
            var expectedValue = new Person
            {
                Name = "foo",
                Surname = "bar"

            };
            var actualValue = configReader.GetSetting<Person>("TestObjectKey");

            Assert.AreEqual(expectedValue.Name, actualValue.Name);
            Assert.AreEqual(expectedValue.Surname, actualValue.Surname);
        }

        [TestMethod]
        public void GetSetting_Production_TypeOfString()
        {
            Environment.SetEnvironmentVariable("ConfEnv", "Production");

            var configReader = new ConfigReader();
            const string expectedValue = "TestValue";
            var actualValue = configReader.GetSetting<string>("TestKey");

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void GetSetting_Production_TypeOfPerson()
        {
            Environment.SetEnvironmentVariable("ConfEnv", "Production");

            var configReader = new ConfigReader();
            var expectedValue = new Person
            {
                Name = "foo",
                Surname = "bar"

            };
            var actualValue = configReader.GetSetting<Person>("TestObjectKey");

            Assert.AreEqual(expectedValue.Name, actualValue.Name);
            Assert.AreEqual(expectedValue.Surname, actualValue.Surname);
        }

        [TestMethod]
        public void GetSetting_KeyDoesNotExits()
        {
            var configReader = new ConfigReader();

            var actualValue = configReader.GetSetting<string>("DoesNotExit");

            Assert.AreEqual(actualValue, null);
        }

        [TestMethod]
        public void GetSetting_RootFolderSpecified()
        {
            Environment.SetEnvironmentVariable("RootFolder", "Config");
            var configReader = new ConfigReader();
            const string expectedValue = "TestValueInCustomRootConfig";
            var actualValue = configReader.GetSetting<string>("TestKey");

            Assert.AreEqual(expectedValue, actualValue);
        }

        public class Person
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }
    }
}
