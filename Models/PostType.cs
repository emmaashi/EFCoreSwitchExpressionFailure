namespace ConsoleApp1.Models;
public class PostType
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;

    public static readonly PostType Entertainment = new() { Id = new("cd36312b-d844-4dbd-99bb-1a44860b5b80"), Description = nameof(Entertainment) };
    public static readonly PostType Informative = new() { Id = new("dd611aba-7ee8-46ba-be61-c34242df1c3e"), Description = nameof(Informative) };
}