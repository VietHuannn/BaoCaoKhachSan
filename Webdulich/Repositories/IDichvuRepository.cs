using System.Collections.Generic;
using System.Threading.Tasks;
using Webdulich.Models;

public interface IDichvuRepository
{
    Task<IEnumerable<Dichvu>> GetAllAsync();
    Task<Dichvu> GetByIdAsync(int id);
    Task AddAsync(Dichvu dichvu);
    Task UpdateAsync(Dichvu dichvu);
    Task DeleteAsync(int id);
}
