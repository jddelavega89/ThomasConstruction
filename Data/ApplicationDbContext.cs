using Microsoft.EntityFrameworkCore;
using ThomasConstruction.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<BillModel> Bills { get; set; } = default!;

    public DbSet<CustomerModel> Customers { get; set; } = default!;

    public DbSet<StateModel> States { get; set; } = default!;

    public DbSet<ProjectModel> Projects { get; set; } = default!;
    
     public DbSet<PaymentModel> Payments { get; set; } = default!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectModel>()
            .Property(p => p.profit)
            .HasComputedColumnSql("[bueget] - [cost]", stored: true); // Ajusta esto según tu fórmula real
    }

}
