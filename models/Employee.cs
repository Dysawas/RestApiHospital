

public class Employee : IPerson
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Patronymic { get; set; }
    public int Age { get; set; }
    public DateOnly BirthDate { get; set; }
    public int Salary { get; set; }

    public int PostId { get; set; }
    public Post? Post { get; set; }

    public int CabinetId { get; set; }
    public Cabinet? Cabinet { get; set; }
    public List<Patient> Patients { get; set; } = new List<Patient>();

}