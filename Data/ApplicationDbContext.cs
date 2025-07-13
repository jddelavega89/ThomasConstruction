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
    
     //public DbSet<CustomerStateModel> CustomerStates { get; set; } = default!;

}
