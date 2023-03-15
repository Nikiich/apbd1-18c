using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Crawler
{
    internal class Program
    {static async Task Main(string[] args)
        {
            string url;
            // Console.WriteLine("Hi");
            if (args.Length == 0)
            {
                throw new ArgumentException("Value can not be null");
            }

            url = args[0];
            if (!Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                throw new ArgumentException("Nie poprawny url");
            }

            await getcont(url);
        }

        

        static async Task getcont(string url)
        {
            using var client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("blad w czasie pobrania strony");
            }
            string content1 = await client.GetStringAsync(url);
            Regex extractEmailsRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            Array arr = extractEmailsRegex.Matches(content1)
                .Select(m => m.Value)
                .Distinct().ToArray();
            if (arr.Length == 0)
            {
                throw new Exception("Nie znaleziono adresow email");
            }

            foreach (var o in arr)
            {
                Console.WriteLine(o);
            }
        }
    }
}