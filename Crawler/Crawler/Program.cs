namespace Crawler
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //if (args.Length == 0)
            //{
            //    throw new ArgumentException();
            //}
            String urla = "https://pl.wikipedia.org/wiki/Wikipedia:Strona_g%C5%82%C3%B3wna";

            HttpClient httpClient = new HttpClient();
            var content = httpClient.GetStringAsync(urla);
            
            Console.WriteLine(content.ToString);
        }
    }
}