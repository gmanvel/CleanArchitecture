using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Web;
using CleanArchitecture.Web.ApiModels;
using Newtonsoft.Json;
using Xunit;

namespace CleanArchitecture.FunctionalTests.Api
{
    public class ApiToDoItemsControllerV2Post : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ApiToDoItemsControllerV2Post(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreatesAToDoItem()
        {
            var newToDoItem = new ToDoItem
            {
                Title = "Pass Tests",
                Description = "ToDo item created from the test"
            };

            var newToDoItemDto = ToDoItemDTO.FromToDoItem(newToDoItem);

            var payload = new StringContent(JsonConvert.SerializeObject(newToDoItemDto), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v2/todoitems/", payload);

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();

            var createdToDoItemDTO = JsonConvert.DeserializeObject<ToDoItemDTO>(stringResponse);

            Assert.True(createdToDoItemDTO.Id != 0);

            Assert.Equal(newToDoItem.Title, createdToDoItemDTO.Title);

            Assert.Equal(newToDoItem.Description, createdToDoItemDTO.Description);

            Assert.Equal(newToDoItem.IsDone, createdToDoItemDTO.IsDone);
        }
    }
}