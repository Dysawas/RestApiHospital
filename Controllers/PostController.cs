using Microsoft.AspNetCore.Mvc;

[Route("api/posts")]
[ApiController]
public class PostController : ControllerBase
{
    PostRepository _repository;

    public PostController(PostRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult> Get() 
    {
        try
        {
            return Ok(await _repository.GetPosts());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> Get(int id) 
    {
        try
        {
            return await _repository.GetPost(id);
        }
        catch (Exception error)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                  error.Message);
        }
    }
}