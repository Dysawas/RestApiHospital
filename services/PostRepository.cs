using Microsoft.EntityFrameworkCore;

public class PostRepository
{
    HospitalContext _db;
    public PostRepository(HospitalContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Post>> GetPosts() 
    {
        return await _db.Posts.Include(p => p.TypeOfPost).ToListAsync();
    }

    public async Task<Post> GetPost(int id) 
    {
        var foundPost = await _db.Posts.FindAsync(id) ?? throw new Exception("Post not found");
        return foundPost;
    }
}