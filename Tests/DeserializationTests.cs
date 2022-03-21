using NUnit.Framework;
using RestSharp;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pract17.DataEntities;

namespace Pract17.Tests
{
    [TestFixture]
    public class DeserializationTests
    {
        [Test]
        [Order(3)]
        [TestCase("StLab StLab")]
        public async Task DeserializationTest(string userName)
        {
            RestClient client = new RestClient("https://gorest.co.in");
            RestRequest request = new RestRequest($"public/v2/users?name={userName}", Method.Get);
            request.AddHeader("Authorization", "Bearer 2e03951568c9304f5892565524e91ba81d30ce2416447628708f6a67432c6cc7");

            RestResponse response = await client.ExecuteAsync(request);
            UserResponse[] userResponse = JsonConvert.DeserializeObject<UserResponse[]>(response.Content);

            Assert.That(userResponse[0].Name, Is.EqualTo(userName));
        }
    }
}
