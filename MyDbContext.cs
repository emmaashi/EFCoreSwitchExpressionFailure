using Microsoft.EntityFrameworkCore;
using ConsoleApp1.Models;

namespace ConsoleApp1;

public class MyDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<PostType> PostTypes { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
        .UseSqlServer($@"Server=(localdb)\mssqllocaldb;Database={GetType().Assembly.GetName().Name};Trusted_Connection=True").LogTo(Console.WriteLine);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PostType>().HasData(new[] { PostType.Informative, PostType.Entertainment });
        modelBuilder.Entity<Post>().Property(z => z.PostTypeId).HasConversion(
            pte => pte == PostTypeEnum.Entertainment ? PostType.Entertainment.Id : PostType.Informative.Id,
            pt => pt == PostType.Entertainment.Id ? PostTypeEnum.Entertainment : PostTypeEnum.Informative);
    }
}
