using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Web;
using Newtonsoft.Json;
using Xunit;

namespace CleanArchitecture.FunctionalTests.Api
{
    public class ApiToDoItemsControllerV2List : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ApiToDoItemsControllerV2List(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsThreeItems()
        {
            var response = await _client.GetAsync("/api/v2/todoitems");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<ToDoItem>>(stringResponse).ToList();

            Assert.Equal(3, result.Count);

            Assert.Contains(result, i => i.Title == SeedData.ToDoItem1.Title);
            Assert.Contains(result, i => i.Title == SeedData.ToDoItem2.Title);
            Assert.Contains(result, i => i.Title == SeedData.ToDoItem3.Title);
        }
    }
}