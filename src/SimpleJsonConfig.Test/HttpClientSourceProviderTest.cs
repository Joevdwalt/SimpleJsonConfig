using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleJsonConfig.Providers;

namespace SimpleJsonConfig.Test
{
    [TestClass]
    public class HttpClientSourceProviderTest
    {
        public IJsonSourceProvider SourceProvider => new HttpClientSourceProvider("http://jsonplaceholder.typicode.com/users/1", HttpMethod.Get);

        [TestMethod]
        public async Task Http_Async_GetSetting_TypeOfString()
        {
            // arrange
            var configReader = new ConfigReader(this.SourceProvider);
            // act
            const string expectedValue = "Leanne Graham";
            var actualValue = await configReader.GetSettingAsync<string>("name");
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public async Task Http_Async_GetSetting_TypeOfAddress()
        {
            // arrange
            var configReader = new ConfigReader(this.SourceProvider);
            var expectedValue = new Address
            {
                street="Kulas Light",
                suite = "Apt. 556",
                city = "Gwenborough"
            };
            // act
            var actualValue = await configReader.GetSettingAsync<Address>("address");
            // assert
            Assert.AreEqual(expectedValue.street, actualValue.street);
            Assert.AreEqual(expectedValue.suite, actualValue.suite);
        }

        [TestMethod]
        public async Task Http_Async_GetSetting_KeyDoesNotExits()
        {
            // arrange
            var configReader = new ConfigReader(this.SourceProvider);
            // act
            var actualValue = await configReader.GetSettingAsync<string>("DoesNotExit");
            // assert
            Assert.AreEqual(actualValue, null);
        }


        [TestMethod]
        public void Http_Sync_GetSetting_TypeOfString()
        {
            // arrange
            var provider = new HttpClientSourceProvider("http://date.jsontest.com/", HttpMethod.Get);
            var configReader = new ConfigReader(this.SourceProvider);
            // act
            const string expectedValue = "Leanne Graham";
            var actualValue = configReader.GetSetting<string>("name");
            // assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void Http_Sync_GetSetting_TypeOfAddress()
        {
            // arrange
            var configReader = new ConfigReader(this.SourceProvider);
            var expectedValue = new Address
            {
                street = "Kulas Light",
                suite = "Apt. 556",
                city = "Gwenborough"
            };
            // act
            var actualValue = configReader.GetSetting<Address>("address");
            // assert
            Assert.AreEqual(expectedValue.street, actualValue.street);
            Assert.AreEqual(expectedValue.suite, actualValue.suite);
        }

        [TestMethod]
        public void Http_Sync_GetSetting_KeyDoesNotExits()
        {
            // arrange
            var configReader = new ConfigReader(this.SourceProvider);
            // act
            var actualValue = configReader.GetSetting<string>("DoesNotExit");
            // assert
            Assert.AreEqual(actualValue, null);
        }

        public class Geo
        {
            public string lat { get; set; }
            public string lng { get; set; }
        }

        public class Address
        {
            public string street { get; set; }
            public string suite { get; set; }
            public string city { get; set; }
            public string zipcode { get; set; }
            public Geo geo { get; set; }
        }

        public class Company
        {
            public string name { get; set; }
            public string catchPhrase { get; set; }
            public string bs { get; set; }
        }

        public class RootObject
        {
            public int id { get; set; }
            public string name { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public Address address { get; set; }
            public string phone { get; set; }
            public string website { get; set; }
            public Company company { get; set; }
        }
    }
}
