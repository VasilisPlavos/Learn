using MagicVilla.VillaAPI.Models;

namespace MagicVilla.VillaAPI.Repository.IRepository;

public interface IVillaRepository : IRepository<Villa>
{
    Task<Villa> UpdateAsync(Villa entity);
}