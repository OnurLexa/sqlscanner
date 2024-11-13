using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace sqlscanner
{
    class program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Site Girin - Input Site: ");
            string siteUrl = Console.ReadLine();

            Console.WriteLine("Seçenekler - Options: ");
            Console.WriteLine("1. SQL Injection");
            Console.WriteLine("2. Veritabanından Veri Çek - Pull Data from the Database: ");
            Console.WriteLine("Seçim Yapın - Choose: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                await RunSqlmap(siteUrl, "-u");
            }
            else if (choice == "2")
            {
                await RunSqlmap(siteUrl, "-u --dbs");
            }
            else
            {
                Console.WriteLine("Geçersiz Seçim - Invalid Choice");
            }
        }
    }

    static async Task RunSqlmap(string url, params string[] args)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "sqlmap",
            Arguments = string.Join(" ", args) + " \"" + url + "\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = new Process { StartInfo = startInfo })
        {
            process.Start();

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            process.WaitForExit();

            Console.WriteLine("Çıktı - Output: \n" + output);

            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine("Hata - Error: \n" + error);
            }
        }
    }
}
