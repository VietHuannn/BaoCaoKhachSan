using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Webdulich.Models;

public class SQLDichvuRepository : IDichvuRepository
{
    private readonly HotelDbContext _context;

    public SQLDichvuRepository(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Dichvu>> GetAllAsync()
    {
        return await _context.Dichvus.ToListAsync();
    }

    public async Task<Dichvu> GetByIdAsync(int id)
    {
        return await _context.Dichvus.FindAsync(id);
    }

    public async Task AddAsync(Dichvu dichvu)
    {
        _context.Dichvus.Add(dichvu);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Dichvu dichvu)
    {
        _context.Entry(dichvu).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var dichvu = await _context.Dichvus.FindAsync(id);
        _context.Dichvus.Remove(dichvu);
        await _context.SaveChangesAsync();
    }
}
