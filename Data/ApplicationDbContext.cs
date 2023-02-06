using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<ApplicationEntity> Applications => Set<ApplicationEntity>();
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> o) : base(o)
    {
        
    }

    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) 
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        optionsBuilder.UseSqlite($"Data Source={Path.Join(path, "applications.db")}");
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        SeedData.Seed(builder);
    }
}