using Microsoft.EntityFrameworkCore;

public class CabinetRepository
{
    HospitalContext _db;
    public CabinetRepository(HospitalContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Cabinet>> GetCabinets() 
    {
        return await _db.Cabinets.ToListAsync();
    }

    public async Task<Cabinet> GetCabinet(int id) 
    {
        var foundCabinet = await _db.Cabinets.FindAsync(id) ?? throw new Exception("Cabinet not found");
        return foundCabinet;
    }
}