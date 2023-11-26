using Microsoft.AspNetCore.Mvc;

[Route("api/cabinets")]
[ApiController]
public class CabinetController : ControllerBase
{
    CabinetRepository _repository;

    public CabinetController(CabinetRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult> Get() 
    {
        try
        {
            return Ok(await _repository.GetCabinets());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cabinet>> Get(int id) 
    {
        try
        {
            return await _repository.GetCabinet(id);
        }
        catch (Exception error)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                  error.Message);
        }
    }
}