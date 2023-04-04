using Animals.Models;

namespace Animals.Services
{
    public interface IDBAnimalsService
    {
        Task<IList<Animal>> GetAnimalsListAsync(string orderBy);
        Task<Animal> AddAnimals(Animal animal);
    }
}
