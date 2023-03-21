namespace ConsoleApp1;

public class Uczelnia
{
    public string createdAt { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
    public string author { get; set; } = "Nikita Heraimovych";
    public List<Student> studenci { get; } = new();
    public List<ActiveStudy> activeStudies { get; } = new();

    public void addStudent(Student student)
    {
        if (!student.isCorrect())
        {
            throw new Exception("err, brakujace pola::: ");

        }

        if (studenci.Exists(s => s.fname.Equals(student.fname) &&
                                          s.lname.Equals(student.lname) &&
                                          s.indexNumber.Equals(student.indexNumber)))
        {
            throw new Exception("err, nie poprawny duplikat::: ");
        }

        studenci.Add(student);
        if (student.studies.name.Split(" ")[0].ToLower().Equals("informatyka"))
        {
            if (activeStudies.Exists(a => a.name.Equals("Informatyka")))
            {
                activeStudies.Find(a=>a.name.Equals("Informatyka"))!.numberOfStudents++;
                return;
            }
            activeStudies.Add(new ActiveStudy()
            {
                name = "Informatyka",
                numberOfStudents = 1
            });
            return;
        }

        if (student.studies.name.Split(" ")[0].ToLower().Equals("sztuka"))
        {
            if (activeStudies.Exists(a => a.name.Equals("Sztuka Nowych Media")))
            {
                activeStudies.Find(a=>a.name.Equals("Sztuka Nowych Media"))!.numberOfStudents++;
                return;
            }
            activeStudies.Add(new ActiveStudy()
            {
                name = "Sztuka Nowych Media",
                numberOfStudents = 1
            });
            
        }
    }     
    
}

public class ActiveStudy
{
    public string name { get; set; }
    public int numberOfStudents { get; set; } = 0;

}