namespace QueryingLoadingAndRawSql.Entities;

public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;

    // virtual is required for Lazy Loading via proxies
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}

public class Post
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public int Views { get; set; }
    public int BlogId { get; set; }

    public virtual Blog Blog { get; set; } = null!;
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}

public class Comment
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public int PostId { get; set; }
    public virtual Post Post { get; set; } = null!;
}
