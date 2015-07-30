using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleJsonConfig.Test
{
    [TestClass]
    public class ConfigReaderTests
    {
        [TestMethod]
        public void GetSetting_NoEnviroment_TypeOfString()
        {
            if (Environment.GetEnvironmentVariable("ConfEnv") != null)
            {
                Environment.SetEnvironmentVariable("ConfEnv", null);
            }

            if (Environment.GetEnvironmentVariable("RootFolder") != null)
            {
                Environment.SetEnvironmentVariable("RootFolder", null);
            }
            

            var configReader = new ConfigReader();
            const string expectedValue = "TestValue";
            var actualValue = configReader.GetSetting<string>("TestKey");

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void GetSetting_NoEnviroment_TypeOfPerson()
        {
            //Explicitly Remove eniroment 
            if (Environment.GetEnvironmentVariable("ConfEnv") != null)
            {
                Environment.SetEnvironmentVariable("ConfEnv", null);
            }

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
            if (Environment.GetEnvironmentVariable("ConfEnv") != null)
            {
                Environment.SetEnvironmentVariable("ConfEnv", null);
            }

            if (Environment.GetEnvironmentVariable("RootFolder") != null)
            {
                Environment.SetEnvironmentVariable("RootFolder", null);
            }
            

            Environment.SetEnvironmentVariable("ConfEnv", "Production");

            var configReader = new ConfigReader();
            const string expectedValue = "TestValue";
            var actualValue = configReader.GetSetting<string>("TestKey");

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void GetSetting_Production_TypeOfPerson()
        {
            if (Environment.GetEnvironmentVariable("ConfEnv") != null)
            {
                Environment.SetEnvironmentVariable("ConfEnv", null);
            }

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

            if (Environment.GetEnvironmentVariable("ConfEnv") != null)
            {
                Environment.SetEnvironmentVariable("ConfEnv", null);
            }

            if (Environment.GetEnvironmentVariable("RootFolder") != null)
            {
                Environment.SetEnvironmentVariable("RootFolder", null);
            }
            

            Environment.SetEnvironmentVariable("RootFolder", "config");
            var configReader = new ConfigReader();
            string expectedValue = "TestValueInCustomRootConfig";
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
