

public class Post
{
    public int PostId { get; set; }
    public string PostTitle { get; set; }
    public int TypeOfPostId { get; set; }
    public TypeOfPost TypeOfPost { get; set; }
}