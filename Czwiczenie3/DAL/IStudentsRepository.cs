using Czwiczenie3.Models;

namespace Czwiczenie3.DAL
{
    public interface IStudentsRepository
    {
        IEnumerable<Student> GetStudents();
        Student GetStudentsById(string index);
        Student ModifyStudent(StudentModyf studentModyf, string index);
        Student AddStudent(Student student);
        bool DeleteStudent(string index);
    }
}
