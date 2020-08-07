using System.Net.Http;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Web;
using Newtonsoft.Json;
using Xunit;

namespace CleanArchitecture.FunctionalTests.Api
{
    public class ApiToDoItemsControllerV2GetById : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ApiToDoItemsControllerV2GetById(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsToDoItem()
        {
            var response = await _client.GetAsync($"/api/v2/todoitems/{SeedData.ToDoItem1.Id}");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();

            var toDoItem = JsonConvert.DeserializeObject<ToDoItem>(stringResponse);

            Assert.Equal(SeedData.ToDoItem1, toDoItem);
        }
    }
}