using System.ComponentModel.DataAnnotations;

namespace Czwiczenie3.Models
{
    public class StudentModyf
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public string Study { get; set; }
        [Required]
        public string Mode { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FName { get; set; }
        [Required]
        public string MName { get; set; }
    }
}
