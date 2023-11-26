using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Web.Http.Cors;



[Route("api/patients")]
[ApiController]
public class PatientController : ControllerBase
{
    private PatientRepository _repository;
    public PatientController(PatientRepository repository)
    {
        _repository = repository;
    }
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        try
        {
            return Ok(await _repository.GetPatients());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> Get(int id)
    {
        try
        {
            return await _repository.GetPatient(id);
        }
        catch (Exception error)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                  error.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Patient>> Post([FromBody] Patient patient)
    {
          if (patient == null)
            return BadRequest();
           
        try
        {
            var createdPatient = await _repository.AddPatient(patient);
            return CreatedAtAction(nameof(Get),
                new { id = createdPatient.PatientId }, createdPatient);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new patient record");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Patient>> Put(int id, Patient patient)
    {
        if (id != patient.PatientId)
                return BadRequest("Patient ID mismatch");
        try
        {
            return await _repository.UpdatePatient(id, patient);
        }
        catch (Exception error)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                error.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Patient>> Delete(int id)
    {
        try
        {
            return await _repository.DeletePatient(id);
        }
        catch (Exception error)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                error.Message);
        }
    }

}