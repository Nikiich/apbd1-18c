using System.ComponentModel.DataAnnotations;

namespace Animals.Models
{
    public class Animal
    {
        [Required] public int IdAnimal { get; set; }
        [MaxLength(200)] [Required] public string Name { get; set; }
        [MaxLength(200)] [Required] public string Description { get; set; }
        [Required] [MaxLength(200)] public string Category { get; set; }
        [MaxLength(200)] [Required] public string Area { get; set; }
    }
}