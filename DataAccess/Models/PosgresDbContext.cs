using Microsoft.EntityFrameworkCore;

namespace SRE.Program.WebAPI.DataAccess.Models;

public partial class PosgresDbContext : DbContext
{
    public PosgresDbContext(DbContextOptions<PosgresDbContext> options) : base(options) { }

    public DbSet<Location> Locations { get; set; }

    public DbSet<Recipient> Recipients { get; set; }

    public DbSet<Logistic> Logistics { get; set; }

    public DbSet<LogisticTracking> LogisticTrackings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("location");
            entity.HasKey(c => c.location_id);

            entity.Property(e => e.location_id)
                .HasColumnName("location_id")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.location_address)
                .HasColumnType("character varying")
                .HasColumnName("location_address");

            entity.Property(e => e.location_city)
                .HasColumnType("character varying")
                .HasColumnName("location_city");

            entity.Property(e => e.location_title)
                .HasColumnType("character varying")
                .HasColumnName("location_title");
        });

        modelBuilder.Entity<Logistic>(entity =>
        {
            entity.ToTable("logistic");
            entity.HasKey(c => c.logistic_id);

            entity.Property(e => e.logistic_id)
                .HasColumnName("logistic_id")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.estimated_delivery).HasColumnName("estimated_delivery");

            entity.Property(e => e.recipient_id).HasColumnName("recipient_id");
        });

        modelBuilder.Entity<LogisticTracking>(entity =>
        {
            entity.ToTable("logistic_tracking");
            entity.HasKey(c => c.logistic_tracking_id);

            entity.Property(e => e.logistic_tracking_id)
                .HasColumnName("logistic_tracking_id")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.arrive_datetime).HasColumnName("arrive_datetime");

            entity.Property(e => e.location_id).HasColumnName("location_id");

            entity.Property(e => e.logistic_id).HasColumnName("logistic_id");

            entity.Property(e => e.recipient_id).HasColumnName("recipient_id");

            entity.Property(e => e.tracking_status)
                .HasColumnType("character varying")
                .HasColumnName("tracking_status");
        });

        modelBuilder.Entity<Recipient>(entity =>
        {
            entity.ToTable("recipient");
            entity.HasKey(c => c.recipient_id);

            entity.Property(e => e.recipient_id)
                .HasColumnName("recipient_id")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.recipient_address)
                .HasColumnType("character varying")
                .HasColumnName("recipient_address");

            entity.Property(e => e.recipient_name)
                .HasColumnType("character varying")
                .HasColumnName("recipient_name");

            entity.Property(e => e.recipient_phone)
                .HasColumnType("character varying")
                .HasColumnName("recipient_phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
