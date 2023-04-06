namespace Czwiczenie3.Models
{
    public class MyDate
    {
        public int Month { get; set; }
        public int Day { get; set; }
        public int Year { get; set; }

        public MyDate(int month, int day, int year) 
        { 
            Month = month;
            Day = day;
            Year = year;
        }
        public MyDate(string date) 
        {
            string[] s = date.Split("/");
            if (s.Length == 3)
            {
                Month = int.Parse(s[0]);
                Day = int.Parse(s[1]);
                Year = int.Parse(s[2]);
            }

        }
        public string toString()
        {
            return $"{Month}/{Day}/{Year}";
        }
    }
}
