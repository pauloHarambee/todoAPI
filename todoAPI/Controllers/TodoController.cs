using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todoitems.DbContext;
using Todoitems.Models;

namespace todoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _todoContext;
        public TodoController(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetAll()
        {
            return Ok(_todoContext.TodoItems.ToList());
        }
        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(int id)
        {
            return _todoContext.TodoItems.Find(id) == null 
                ? NotFound() : 
                Ok(_todoContext.TodoItems.Find(id));
        }
        [HttpPut]
        public async Task<IActionResult> Put(int id, TodoItem todo)
        {
            if(id != todo.Id)
                return BadRequest();

            TodoItem todoItem = await _todoContext.TodoItems.FindAsync(id);
            if(todoItem == null)
                return NotFound();

            todoItem.Name = todo.Name;
            todoItem.IsComplete = todo.IsComplete;

            try
            {
                _todoContext.Update(todoItem);
                await _todoContext.SaveChangesAsync();
            }catch(DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(todoItem);
        }
        [HttpPost]
        public async Task<ActionResult<TodoItem>> Post(TodoItem todoItem)
        {
            _todoContext.Add(todoItem);
            await _todoContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Post), todoItem);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            TodoItem todoItem = await _todoContext.TodoItems.FindAsync(id);

            if( todoItem == null)
                return NotFound(nameof(TodoItem));

            _todoContext.TodoItems.Remove(todoItem);
            await _todoContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
