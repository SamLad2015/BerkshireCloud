using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using testApp.Data;

namespace testApp
{
    class Program
    {
        public static IConfigurationRoot Configuration;
        [STAThread]
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
            
            using (var input = new StreamReader(Configuration.GetSection("filePath").Value))
            {
                var wordList = input.ReadToEnd().Split(null);
                var output = wordList.GroupBy(x => x)
                    .Select(x => new Word {charchter = x.Key, repeat = x.Count()})
                    .OrderByDescending(x => x.repeat);
                foreach (var item in output)
                {
                    Console.WriteLine($"For word '{item.charchter}' count is :{item.repeat}");
                }
            }
        }
    }
}