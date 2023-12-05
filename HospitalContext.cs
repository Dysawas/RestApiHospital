using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class HospitalContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Reception> Receptions { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<TypeOfPost> TypeOfPosts { get; set; }
    public DbSet<Cabinet> Cabinets { get; set; }
    
    public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
    {
    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        base.OnModelCreating(modelBuilder);
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<DateOnly>()
        .HaveConversion<DateOnlyConverter>()
        .HaveColumnType("date");

        configurationBuilder.Properties<TimeOnly>()
        .HaveConversion<TimeOnlyConverter>()
        .HaveColumnType("time");

    }

}