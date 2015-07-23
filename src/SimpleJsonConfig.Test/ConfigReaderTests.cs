using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleJsonConfig.Providers;

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
