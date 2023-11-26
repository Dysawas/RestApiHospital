

public class Patient : IPerson
{
    public int PatientId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Patronymic { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
    public DateOnly BirthDate { get; set; }
    public List<Employee> Employees { get; set; } = new List<Employee>();
}