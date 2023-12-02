using Microsoft.EntityFrameworkCore;

namespace SRE.Program.WebAPI.DataAccess.Models;

public class PosgresDbContext : DbContext
{
	public PosgresDbContext(DbContextOptions<PosgresDbContext> options) : base(options) { }

	public DbSet<Location> Locations { get; set; }

	public DbSet<Recipient> Recipients { get; set; }

    public DbSet<Logistic> Logistics { get; set; }

    public DbSet<LogisticTracking> LogisticTrackings { get; set; }
}
