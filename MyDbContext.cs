using Microsoft.EntityFrameworkCore;
using ConsoleApp1.Models;

namespace ConsoleApp1;

using Microsoft.EntityFrameworkCore;
using ConsoleApp1.Models;

public class MyDbContext : DbContext
{
    public DbSet<Post> Posts => Set<Post>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ExpressionSwitchRepro;Trusted_Connection=True");
}

