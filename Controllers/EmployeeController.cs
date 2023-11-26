using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[Route("api/employees")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private EmployeeRepository _repository;
    public EmployeeController(EmployeeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        try
        {
            return Ok(await _repository.GetEmployees());
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> Get(int id)
    {
        try
        {
            return await _repository.GetEmployee(id);
        }
        catch (Exception error)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                  error.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> Post([FromBody] Employee employee) 
    {
        if (employee == null)
          return BadRequest();

        try
        {
            var createdEmployee = await _repository.AddEmployee(employee);
            return CreatedAtAction(nameof(Get), new {id = employee.EmployeeId}, createdEmployee);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new employee record");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Employee>> Put(int id, Employee employee)
    {
        if (id != employee.EmployeeId)
                return BadRequest("Patient ID mismatch");
        try
        {
            return await _repository.UpdateEmployee(id, employee);
        }
        catch (Exception error)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                error.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Employee>> Delete(int id) 
    {
        try
        {
            return await _repository.DeleteEmployee(id);
        }
        catch (Exception error)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                error.Message);
        }
    }
}