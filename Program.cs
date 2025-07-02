using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ConsoleApp1;
using ConsoleApp1.Models;

using var context = new MyDbContext();
await context.Database.EnsureDeletedAsync();
await context.Database.EnsureCreatedAsync();

context.Posts.Add(new Post { Type = 1 });
await context.SaveChangesAsync();

var param = Expression.Parameter(typeof(Post), "p");
var switchExpr = Expression.Switch(
    typeof(string),
    Expression.Property(param, nameof(Post.Type)),
    Expression.Constant("Unknown"),
    null,
    new[]
    {
        Expression.SwitchCase(Expression.Constant("One"), Expression.Constant(1)),
        Expression.SwitchCase(Expression.Constant("Two"), Expression.Constant(2))
    }
);
var lambda = Expression.Lambda<Func<Post, string>>(switchExpr, param);

var query = context.Posts.Select(lambda);

var results = await query.ToListAsync();
