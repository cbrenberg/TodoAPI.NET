using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Models;

namespace TodoAPI.Controllers
{
    //establishes this controller as an API route
    [Route("api/[controller]")]
    //this attribute makes sure this type and derived types will create api responses
    [ApiController]
    public class TodoController : ControllerBase
    {
        //encapsulates the todo database context as a private field
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            //sets private _context as a reference to method parameter 'context'
            _context = context;
            //creates a default TodoItem if database is empty
            if(_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        //GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems() 
        {
            //asynchronously puts together a list of all TodoItems from database
            return await _context.TodoItems.ToListAsync();
        }

        //GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            //asynchronously finds a single todo item by its id
            var todoItem = await _context.TodoItems.FindAsync(id);

            //if no such id exists, returns 404
            if (todoItem == null)
            {
                return NotFound();
            }
            //otherwise, returns the todo item
            return todoItem;
        }
    }
}
