using MusicManager.API.Models.Domain;

namespace MusicManager.API.Repositories
{
    public interface IBandRepository
    {
        Task<List<Band>> GetAllAsync();
        Task<Band?> GetByIdAsync(int id);
        Task<Band> CreateAsync(Band band);
        Task<Band?> UpdateAsync(int id, Band band);
        Task<Band?> DeleteAsync(int id);
    }
}
