using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace shift_logger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController(ShiftContext context) : ControllerBase
    {
        private readonly ShiftContext _context = context;

        // GET: api/Shifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewShiftDTO>>> GetShifts()
        {
            return await _context.Shifts.Select(
                shift => ItemToDTO(shift)).ToListAsync();
        }

        // GET: api/Shift/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewShiftDTO>> GetShift(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);

            if (shift == null)
            {
                return NotFound();
            }

            return ItemToDTO(shift);
        }

        // PUT: api/Shift/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        // public async Task<IActionResult> PutShift(int id, Shift shift)
        // {
        //     if (id != shift.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(shift).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ShiftExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // POST: api/Shift
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ViewShiftDTO>> PostShift(AddShiftDTO shiftDTO)
        {
            Shift dbShift = new() { StartTime = shiftDTO.StartTime, EndTime = shiftDTO.EndTime };
            dbShift = _context.Shifts.Add(dbShift).Entity;
            await _context.SaveChangesAsync();

            ViewShiftDTO outputShift = ItemToDTO(dbShift);
            return CreatedAtAction(nameof(GetShift), new { dbShift.Id }, outputShift);
        }

        // DELETE: api/Shift/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShiftExists(int id)
        {
            return _context.Shifts.Any(e => e.Id == id);
        }

        private static ViewShiftDTO ItemToDTO(Shift shift)
        {

            var result = new ViewShiftDTO() { Id = shift.Id, StartTime = shift.StartTime, EndTime = shift.EndTime };
            return result;
        }
    }
}
