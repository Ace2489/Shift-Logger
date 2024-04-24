using Microsoft.EntityFrameworkCore;

namespace shift_logger;

public class ShiftContext(DbContextOptions<ShiftContext> options) : DbContext(options)
{
    public DbSet<Shift> Shifts { get; set; }
}
