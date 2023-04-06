using System.ComponentModel.DataAnnotations;
using Animals.DTO;

namespace Animals.Models
{
    public class Animal
    {
        public int IdAnimal { get; set; }
        [MaxLength(200)] [Required] public string Name { get; set; }
        [MaxLength(200)] [Required] public string Description { get; set; }
        [Required] [MaxLength(200)] public string Category { get; set; }
        [MaxLength(200)] [Required] public string Area { get; set; }

        public Animal()
        {
        }

        public Animal(AnimalDTO animalDto, int idAnimal)
        {
            IdAnimal = idAnimal;
            Name = animalDto.Name;
            Description = animalDto.Description;
            Category = animalDto.Category;
            Area = animalDto.Area;
        }
    }
}