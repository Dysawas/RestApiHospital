using Microsoft.EntityFrameworkCore;

public class EmployeeRepository
{
    private HospitalContext _db;
    public EmployeeRepository(HospitalContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Employee>> GetEmployees()
    {
        return await _db.Employees
        .Include(e => e.Cabinet)
        .Include(e => e.Post)
        .ThenInclude(p => p.TypeOfPost)
        .ToListAsync();
    }

    public async Task<Employee> GetEmployee(int id)
    {
        var employee = await _db.Employees
        .Include(e => e.Cabinet)
        .Include(e => e.Post)
        .ThenInclude(p => p.TypeOfPost)
        .FirstOrDefaultAsync(e => e.EmployeeId == id) 
        ?? throw new Exception("Employee not found");
        return employee;
    }

    public async Task<Employee> AddEmployee(Employee employee)
    {
        Employee employeeFromClient = new()
        {
            EmployeeId = employee.EmployeeId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Patronymic = employee.Patronymic,
            Age = employee.Age,
            BirthDate = employee.BirthDate,
            Salary = employee.Salary,
            PostId = employee.PostId,
            CabinetId = employee.CabinetId,
        };

        var foundEmployee = await _db.Employees.AddAsync(employeeFromClient);

        await _db.SaveChangesAsync();

        foundEmployee.Reference(e => e.Post).Query().Include(p => p.TypeOfPost).Load();
        foundEmployee.Reference(e => e.Cabinet).Load();
        return foundEmployee.Entity;
    }

    public async Task<Employee> UpdateEmployee(int id, Employee employee)
    {
        var foundEmployee = await _db.Employees
        .Include(e => e.Cabinet)
        .Include(e => e.Post)
        .ThenInclude(p => p.TypeOfPost)
        .FirstOrDefaultAsync(e => e.EmployeeId == id);;
        
        if (foundEmployee != null)
        {
            foundEmployee.FirstName = employee.FirstName;
            foundEmployee.LastName = employee.LastName;
            foundEmployee.Patronymic = foundEmployee.Patronymic;
            foundEmployee.Age = foundEmployee.Age;
            foundEmployee.BirthDate = foundEmployee.BirthDate;
            foundEmployee.Salary = foundEmployee.Salary;
            foundEmployee.PostId = employee.PostId;
            foundEmployee.CabinetId = employee.CabinetId;

            await _db.SaveChangesAsync();
            return foundEmployee;
        }
        throw new Exception("Error updating data");
    }

    public async Task<Employee> DeleteEmployee(int id)
    {
        var foundEmployee = await _db.Employees
        .Include(e => e.Cabinet)
        .Include(e => e.Post)
        .ThenInclude(p => p.TypeOfPost)
        .FirstOrDefaultAsync(e => e.EmployeeId == id);

        if (foundEmployee != null)
        {
            _db.Employees.Remove(foundEmployee);
            await _db.SaveChangesAsync();
            return foundEmployee;
        }
        
        throw new Exception("Error deleting data");
    }
}