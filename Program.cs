using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ConsoleApp1.Models;
using ConsoleApp1;

using var context = new MyDbContext();

await context.Database.EnsureDeletedAsync();
await context.Database.EnsureCreatedAsync();

context.Add(new Post
{
    Id = Guid.NewGuid(),
    Title = "My Entertainment",
    PostTypeId = PostTypeEnum.Entertainment
});

await context.SaveChangesAsync();

Console.WriteLine("Attempting Expression.Switch...");

var param = Expression.Parameter(typeof(Post), "p");
var switchValue = Expression.Convert(Expression.Property(param, nameof(Post.PostTypeId)), typeof(PostTypeEnum?));

var switchExpr = Expression.Switch(
    typeof(string),
    switchValue,
    Expression.Constant("Unknown"),
    null,
    new[]
    {
        Expression.SwitchCase(Expression.Constant("Entertainment"), Expression.Constant(PostTypeEnum.Entertainment, typeof(PostTypeEnum?))),
        Expression.SwitchCase(Expression.Constant("Informative"),   Expression.Constant(PostTypeEnum.Informative, typeof(PostTypeEnum?))),
    }
);

var lambda = Expression.Lambda<Func<Post, string>>(switchExpr, param);

try
{
    var result = await context.Posts.Select(lambda).ToListAsync();
    foreach (var r in result)
    {
        Console.WriteLine($"Result: {r}");
    }
}
catch (Exception ex)
{
    Console.WriteLine("Error executing query:");
    Console.WriteLine(ex);
}