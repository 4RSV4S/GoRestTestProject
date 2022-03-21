using Newtonsoft.Json;
using NUnit.Framework;
using Pract17.DataEntities;
using RestSharp;
using System.Net;
using System.Threading.Tasks;

namespace Pract17.Tests
{
    [TestFixture]
    public class StatusCodeTests
    {
        [Test]
        [Order(5)]
        [TestCase(1, HttpStatusCode.NotFound)]
        public async Task GETStatusCodeTest(int userId, HttpStatusCode expectedHttpStatusCode)
        {
            RestClient client = new RestClient("https://gorest.co.in");
            RestRequest request = new RestRequest($"public/v2/users/{userId}", Method.Get);

            RestResponse response =  await client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(expectedHttpStatusCode));
        }
        
        [Test]
        [Order(6)]
        [TestCase("StLab StLab")]
        public async Task PATCHStatusCodeTest(string userName)
        {
            RestClient client = new RestClient("https://gorest.co.in");
            RestRequest getRequest = new RestRequest($"public/v2/users?name={userName}", Method.Get);
            getRequest.AddHeader("Authorization", "Bearer 2e03951568c9304f5892565524e91ba81d30ce2416447628708f6a67432c6cc7");
            RestResponse getResponse = await client.ExecuteAsync(getRequest);

            UserResponse[] userResponse = JsonConvert.DeserializeObject<UserResponse[]>(getResponse.Content);

            RestRequest request = new RestRequest($"public/v2/users/{userResponse[0].Id}", Method.Patch);
            request.AddHeader("Authorization", "Bearer 2e03951568c9304f5892565524e91ba81d30ce2416447628708f6a67432c6cc7");
            request.AddParameter("gender", "male");
            request.AddParameter("status", "active");

            RestResponse response = await client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        [Order(7)]
        [TestCase("StLab StLab")]
        public async Task PUTStatusCodeTest(string userName)
        {
            RestClient client = new RestClient("https://gorest.co.in");
            RestRequest getRequest = new RestRequest($"public/v2/users?name={userName}", Method.Get);
            getRequest.AddHeader("Authorization", "Bearer 2e03951568c9304f5892565524e91ba81d30ce2416447628708f6a67432c6cc7");
            RestResponse getResponse = await client.ExecuteAsync(getRequest);

            UserResponse[] userResponse = JsonConvert.DeserializeObject<UserResponse[]>(getResponse.Content);

            RestRequest request = new RestRequest($"public/v2/users/{userResponse[0].Id}", Method.Put);
            request.AddHeader("Authorization", "Bearer 2e03951568c9304f5892565524e91ba81d30ce2416447628708f6a67432c6cc7");
            request.AddParameter("name", "Xxxx Xxxx");
            request.AddParameter("email", "Xxxx@xxx.xxx");
            request.AddParameter("gender", "female");
            request.AddParameter("status", "inactive");

            RestResponse response = await client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        [Order(8)]
        [TestCase("Xxxx Xxxx")]
        public async Task DELATEStatusCodeTest(string userName)
        {
            RestClient client = new RestClient("https://gorest.co.in");
            RestRequest getRequest = new RestRequest($"public/v2/users?name={userName}", Method.Get);
            getRequest.AddHeader("Authorization", "Bearer 2e03951568c9304f5892565524e91ba81d30ce2416447628708f6a67432c6cc7");
            RestResponse getResponse = await client.ExecuteAsync(getRequest);

            UserResponse[] userResponse = JsonConvert.DeserializeObject<UserResponse[]>(getResponse.Content);

            RestRequest delRequest = new RestRequest($"public/v2/users/{userResponse[0].Id}", Method.Delete);
            delRequest.AddHeader("Authorization", "Bearer 2e03951568c9304f5892565524e91ba81d30ce2416447628708f6a67432c6cc7");

            RestResponse delResponse = await client.ExecuteAsync(delRequest);

            Assert.That(delResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }
    }
}