using Czwiczenie3.Models;


namespace Czwiczenie3.DAL
{
    public class StudentsRepository : IStudentsRepository
    {
        private string _dbUrl = "database link";
        private List<Student> _students;

        public StudentsRepository()
        {
            _students = new List<Student>();
            foreach (string line in File.ReadLines(_dbUrl))
            {
                string[] con = line.Split(",");
                string[] date = con[3].Split('/');
                Student st = new Student(con[0], con[1], con[2],
                    new DateOnly(int.Parse(date[2]), int.Parse(date[0]), int.Parse(date[1])), con[4], con[5], con[6],
                    con[7], con[8]);

                _students.Add(st);
            }
        }

        public Student AddStudent(Student student)
        {
            if (_students.Exists(a => a.Index.Equals(student.Index)))
            {
                throw new Exception("Alreday exists");
            }

            _students.Add(student);
            SaveDB();
            return student;
        }

        public bool DeleteStudent(string index)
        {
            if (!_students.Exists(a => a.Index.Equals(index)))
            {
                return false;
            }

            Student st = _students.First(a => a.Index.Equals(index));
            _students.Remove(st);
            SaveDB();
            return true;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }

        public Student GetStudentsById(string index)
        {
            if (!_students.Exists(a => a.Index.Equals(index)))
            {
                throw new Exception($"Student with index {index} not found");
            }

            Student st = _students.First(a => a.Index.Equals(index));
            return st;
        }

        public Student ModifyStudent(StudentModyf studentModyf, string index)
        {
            if (!_students.Exists(a => a.Index.Equals(index)))
            {
                throw new Exception($"Student with index {index} not found");
            }

            Student st = _students.First(a => a.Index.Equals(index));
            st.Name = studentModyf.Name;
            st.Surname = studentModyf.Surname;
            st.DateOfBirth = studentModyf.DateOfBirth;
            st.Study = studentModyf.Study;
            st.Mode = studentModyf.Mode;
            st.Email = studentModyf.Email;
            st.FName = studentModyf.FName;
            st.MName = studentModyf.MName;
            SaveDB();
            return st;
        }

        private void SaveDB()
        {
            using (StreamWriter sw = new StreamWriter(_dbUrl))
            {
                foreach (Student st in _students)
                {
                    sw.WriteLine(
                        $"{st.Name},{st.Surname},{st.Index},{st.DateOfBirth.Month}/{st.DateOfBirth.Day}/{st.DateOfBirth.Year},{st.Study},{st.Mode},{st.Email},{st.FName},{st.MName}");
                }
            }
        }
    }
}