using System.Text.Encodings.Web;
using System.Text.Json;

namespace ConsoleApp1;

internal class Program
{
    public static void Main(string[] args)
    {
        List<string> formats = new List<string>() { "json" };
        Console.WriteLine("Adres pliku csv: \n");
        string pathCSV = Console.ReadLine();
        if (!File.Exists(pathCSV))
        {
            throw new FileNotFoundException("Plik nie istnieje");
        }
        Console.WriteLine("Adres sciezki docelowej: \n");
        string endPath = Console.ReadLine();
        if (!Directory.Exists(endPath))
        {
            throw new ArgumentException("podana sciezka jest nie poprawna");
        }
        Console.WriteLine("format: \n");
        string dataFormat = Console.ReadLine();
        if (!formats.Contains(dataFormat))
        {
            throw new ArgumentException("podany format nie jest obslugiwany");
        }
        Uczelnia uczelnia = new Uczelnia();
        using (MyLogger myLogger = new MyLogger())
        {
            foreach (string line in File.ReadLines(pathCSV))
            {
                string[] con = line.Split(",");
                try
                {
                    if (con.Length != 9)
                    {
                        myLogger.WriteLog("nie prawidlowe dane: " + line);
                        throw new Exception("Nie prawidlowe dane");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


                Studies studies = new Studies()
                {
                    name = con[2],
                    mode = con[3]
                };
                Student student = new Student()
                {
                    fname = con[0],
                    lname = con[1],
                    indexNumber = con[4],
                    birthdate = con[5],
                    email = con[6],
                    mothersName = con[7],
                    fathersName = con[8],
                    studies = studies
                };
                try
                {
                    uczelnia.addStudent(student);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + student);
                    myLogger.WriteLog(e.Message + student);
                }

            }

            Root root = new Root()
            {
                uczelnia = uczelnia
            };

            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string jsonResult = JsonSerializer.Serialize(root, jsonSerializerOptions);
            using (StreamWriter sw = new StreamWriter(File.Create(endPath + "\\result.json")))
            {
                sw.WriteLine(jsonResult);
            }
        }
    }
}