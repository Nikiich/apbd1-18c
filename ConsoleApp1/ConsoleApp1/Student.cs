using System.Text.Json;

namespace ConsoleApp1;
public class Student
{
    public string indexNumber { get; set; }
    public string fname { get; set; }
    public string lname { get; set; }
    public string birthdate { get; set; }
    public string email { get; set; }
    public string mothersName { get; set; }
    public string fathersName { get; set; }
    public Studies studies { get; set; }

    public bool isCorrect()
    {
        if (indexNumber.Equals("") || fname.Equals("") ||
            lname.Equals("") || birthdate.Equals("") ||
            email.Equals("") || mothersName.Equals("") || fathersName.Equals(""))
        {
            return false;
        }

        return true;
    }

    public override string ToString()
    {
        return fname + "; "
                     + lname + "; "
                     + indexNumber + "; "
                     + birthdate + "; "
                     + email + "; "
                     + mothersName + "; "
                     + fathersName + "; "
                     + studies.name + "; "
                     + studies.mode;
    }
}