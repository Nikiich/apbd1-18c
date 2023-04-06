using System.ComponentModel.DataAnnotations;

namespace Czwiczenie3.Models
{
    public class Student
    {
        [Required] [MaxLength(100)] public string Name { get; set; }
        [Required] [MaxLength(100)] public string Surname { get; set; }

        [Required]
        [RegularExpression(@"[s][0-9]{1,9}$")]
        public string Index { get; }

        [Required] [DataType(DataType.Date)] public DateOnly DateOfBirth { get; set; }
        [Required] [MaxLength(100)] public string Study { get; set; }
        [Required] [MaxLength(100)] public string Mode { get; set; }
        [Required] [EmailAddress] public string Email { get; set; }
        [Required] [MaxLength(100)] public string FName { get; set; }
        [Required] [MaxLength(100)] public string MName { get; set; }


        public Student(string name, string surname, string index, DateOnly dateOfBirth, string study, string mode,
            string email, string fName, string mName)
        {
            Name = name;
            Surname = surname;
            Index = index;
            DateOfBirth = dateOfBirth;
            Study = study;
            Mode = mode;
            Email = email;
            FName = fName;
            MName = mName;
        }
    }
}