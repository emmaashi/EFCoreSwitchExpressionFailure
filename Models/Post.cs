namespace ConsoleApp1.Models;
public class Post
{
    public Guid Id { get; set; }
    public string Title { get; set; }

    public PostTypeEnum PostTypeId { get; set; }
}
