using Animals.DTO;
using Animals.Models;

namespace Animals.Services
{
    public interface IDBAnimalsService
    {
        Task<IList<Animal>> GetAnimalsListAsync(string orderBy);
        Task<Animal> AddAnimals(AnimalDTO animal);
        Task<Animal> UpdateAnimals(AnimalDTO animal, int idAnimal);
        Task<bool> DeleteAnimal(int idAnimal);
    }
}
