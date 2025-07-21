using Microsoft.EntityFrameworkCore;
using ThomasConstruction.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext
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

    public DbSet<ProjectBillModel> ProjectBills { get; set; } = default!;

    public DbSet<ReceiptModel> Receipts { get; set; } = default!;
    
    public DbSet<SupplieModel> Supplies { get; set; } = default!;
    
     public DbSet<ChangeOrderModel> ChangeOrders { get; set; } = default!;
     
      public DbSet<WorkerModel> Workers { get; set; } = default!;

      public DbSet<WorkerSalaryModel> WorkerSalarys { get; set; } = default!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProjectModel>()
            .Property(p => p.profit)
            .HasComputedColumnSql("[bueget] - [cost]", stored: true); // Ajusta esto según tu fórmula real

        modelBuilder.Entity<SupplieModel>()
       .Property(p => p.price_tax)
       .HasComputedColumnSql("[price] + ( [price] * 0.0825)", stored: true); // Ajusta esto según tu fórmula real

         modelBuilder.Entity<WorkerSalaryModel>()
       .Property(p => p.salary)
       .HasComputedColumnSql("[price_hour] * [work_hours]", stored: true); // Ajusta esto según tu fórmula real

        modelBuilder.Entity<SupplieModel>()
      .Property(p => p.total_price)
      .HasComputedColumnSql("[amount] * ([price] +( [price] * 0.0825))", stored: true); // 
    }

}
