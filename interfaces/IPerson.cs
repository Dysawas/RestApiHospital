

interface IPerson
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Patronymic { get; set; }
    public int Age { get; set; }
    public DateOnly BirthDate { get; set; }
    
}