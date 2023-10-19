using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using To_Do_List.Models;

namespace To_Do_List.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoesController : ControllerBase
    {
        private readonly Context _context;

        public ToDoesController(Context context)
        {
            _context = context;
        }

        // GET: api/ToDoes
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetToDo()
        {
            if (_context.ToDo == null)
            {
                return NotFound();
            }
            try
            {
                return await _context.ToDo.ToListAsync();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/ToDoes/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ToDo>> GetToDo(int id)
        {
            if (_context.ToDo == null)
            {
                return NotFound();
            }
            var toDo = await _context.ToDo.FindAsync(id);

            if (toDo == null)
            {
                return NotFound();
            }

            return toDo;
        }

        // PUT: api/ToDoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutToDo(int id, [FromQuery] string status)
        {

            var user = _context.ToDo.FirstOrDefault(x => x.Id == id);

            if (user != null)
            {

                user.Status = status;

                _context.Entry(user).State = EntityState.Modified;

            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // POST: api/ToDoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ToDo>> PostToDo(ToDo toDo)
        {
            if (_context.ToDo == null)
            {
                return Problem("Entity set 'Context.ToDo'  is null.");
            }
            _context.ToDo.Add(toDo);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetToDo", new { id = toDo.Id }, toDo);
        }

        // DELETE: api/ToDoes/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteToDo(int id)
        {
            if (_context.ToDo == null)
            {
                return NotFound();
            }
            var toDo = await _context.ToDo.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }
            try
            {
                _context.ToDo.Remove(toDo);
                await _context.SaveChangesAsync();
                return Ok(toDo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private bool ToDoExists(int id)
        {
            return (_context.ToDo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
