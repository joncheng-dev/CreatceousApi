using Microsoft.EntityFrameworkCore;

namespace CretaceousApi.Models
{
  public class CretaceousApiContext : DbContext
  {
    public DbSet<Animal> Animals { get; set; }

    public CretaceousApiContext(DbContextOptions<CretaceousApiContext> options) : base(options)
    {
    }
    
    // 'protected' b/c accessible to the class itself; 'override' to override default method
    protected override void OnModelCreating(ModelBuilder builder) // redefining how OnModelCreating() method works
    { 
      builder.Entity<Animal>() // call on builder's Entity<Type>(), which returns an EntityTypeBuilder object; allow us to configure the type specified
        .HasData( // call EntityTypeBuilder's HasData() method, passing in any number of entries we'd like to seed database with.
          new Animal { AnimalId = 1, Name = "Matilda", Species = "Woolly Mammoth", Age = 7 },
          new Animal { AnimalId = 2, Name = "Rexie", Species = "Dinosaur", Age = 10 },
          new Animal { AnimalId = 3, Name = "Matilda", Species = "Dinosaur", Age = 2 },
          new Animal { AnimalId = 4, Name = "Pip", Species = "Shark", Age = 4 },
          new Animal { AnimalId = 5, Name = "Bartholomew", Species = "Dinosaur", Age = 22 }
        );
    }    
  }
}