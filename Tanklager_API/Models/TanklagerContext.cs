using Microsoft.EntityFrameworkCore;
namespace Tanklager_API.Models;
using TanklagerLibraryv2;
public class TanklagerContext : DbContext
{
    public TanklagerContext(DbContextOptions<TanklagerContext> options)
        : base(options)
    {
    }

    public DbSet<OilTank> OilTanks { get; set; } = null!;
}