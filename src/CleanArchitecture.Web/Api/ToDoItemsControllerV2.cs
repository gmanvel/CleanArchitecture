using System;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Web.ApiModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Web.Api
{
    [Route("api/v2/todoitems")]
    [ApiController]
    public class ToDoItemsControllerV2: Controller
    {
        private readonly AppDbContext _dbContext;

        public ToDoItemsControllerV2(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/ToDoItems
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var items = (await _dbContext.ToDoItems.ToListAsync())
                .Select(ToDoItemDTO.FromToDoItem);
            
            return Ok(items);
        }

        // GET: api/ToDoItems
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = ToDoItemDTO.FromToDoItem(await _dbContext.FindAsync<ToDoItem>(id));
            return Ok(item);
        }

        // POST: api/ToDoItems
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ToDoItemDTO item)
        {
            var todoItem = new ToDoItem
            {
                Title = item.Title,
                Description = item.Description
            };
            
            _dbContext.Add(todoItem);

            await _dbContext.SaveChangesAsync();

            return Ok(ToDoItemDTO.FromToDoItem(todoItem));
        }

        [HttpPatch("{id:int}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            var toDoItem = await _dbContext.FindAsync<ToDoItem>(id);

            toDoItem.MarkComplete();

            await _dbContext.SaveChangesAsync();

            return Ok(ToDoItemDTO.FromToDoItem(toDoItem));
        }
    }
}