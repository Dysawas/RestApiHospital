using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class PatientRepository
{
    private HospitalContext _db;
    public PatientRepository(HospitalContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Patient>> GetPatients()
    {
        return await _db.Patients.ToListAsync();
    }

    public async Task<Patient> GetPatient(int id)
    {
        var foundPatient = await _db.Patients.FindAsync(id) ?? throw new Exception("Patient not found");
        return foundPatient;
    }

    public async Task<Patient> AddPatient(Patient patient)
    {
        Patient patientFromClient = new()
        {
            PatientId = patient.PatientId,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Patronymic = patient.Patronymic,
            Age = patient.Age,
            Gender = patient.Gender,
            Address = patient.Address,
            BirthDate = patient.BirthDate,
        };
        var foundPatient = await _db.Patients.AddAsync(patientFromClient);
        await _db.SaveChangesAsync();
        return foundPatient.Entity;
    }

    public async Task<Patient?> UpdatePatient(int id, Patient patient)
    {
        var foundPatient = await _db.Patients
                .FirstOrDefaultAsync(p => p.PatientId == id);

        if (foundPatient != null)
        {
            foundPatient.FirstName = patient.FirstName;
            foundPatient.LastName = patient.LastName;
            foundPatient.Patronymic = patient.Patronymic;
            foundPatient.BirthDate = patient.BirthDate;
            foundPatient.Gender = patient.Gender;
            foundPatient.Age = patient.Age;
            foundPatient.Employees = patient.Employees;
            foundPatient.Address = patient.Address;

            await _db.SaveChangesAsync();

            return foundPatient;
        }

        throw new Exception("Error updating data");

    }

    public async Task<Patient> DeletePatient(int id)
    {
        var foundPatient = await _db.Patients.FindAsync(id);
        if (foundPatient != null)
        {
            _db.Patients.Remove(foundPatient);
            await _db.SaveChangesAsync();
            return foundPatient;
        }
        throw new Exception("Error deleting data");
    }
}