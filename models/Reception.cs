

public class Reception
{
    public int ReceptionId { get; set; }
    public int PatientId { get; set; }
    public Patient Patient { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }

    public DateOnly DateOfReception { get; set; }
    public TimeOnly TimeOfReception { get; set; }
}