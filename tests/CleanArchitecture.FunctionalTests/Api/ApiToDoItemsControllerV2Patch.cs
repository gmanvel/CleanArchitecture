using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Web.ApiModels;
using Newtonsoft.Json;
using Xunit;

namespace CleanArchitecture.FunctionalTests.Api
{
    public class ApiToDoItemsControllerV2Patch : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly HttpClient _client;

        public ApiToDoItemsControllerV2Patch(CustomWebApplicationFactory<TestStartup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task MarksToDoItemAsComplete()
        {
            var newToDoItem = new ToDoItem
            {
                Title = "Mark Complete",
                Description = "ToDo item created from test to mark completed"
            };

            var newToDoItemDto = ToDoItemDTO.FromToDoItem(newToDoItem);

            var createRequestPayload = new StringContent(JsonConvert.SerializeObject(newToDoItemDto), Encoding.UTF8, "application/json");

            var createToDoItemResponse = await _client.PostAsync("/api/v2/todoitems/", createRequestPayload);

            createToDoItemResponse.EnsureSuccessStatusCode();

            var createToDoItemStringResponse = await createToDoItemResponse.Content.ReadAsStringAsync();

            var createdToDoItemDTO = JsonConvert.DeserializeObject<ToDoItemDTO>(createToDoItemStringResponse);

            var markCompleteResponse = 
                await _client.PatchAsync($"/api/v2/todoitems/{createdToDoItemDTO.Id}/complete", new StringContent(string.Empty));

            markCompleteResponse.EnsureSuccessStatusCode();

            var markCompleteStringResponse = await markCompleteResponse.Content.ReadAsStringAsync();

            var markCompleteToDoItemDTO = 
                JsonConvert.DeserializeObject<ToDoItemDTO>(markCompleteStringResponse);

            Assert.True(markCompleteToDoItemDTO.IsDone);
        }
    }
}