using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace USAElections.Services
{
    public class FileService
    {

        public void createOrFillErrorLogFile(string line)
        {
            string errorPath = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files\errorLog"}" + "\\" + "errors.txt";

            if (!System.IO.File.Exists(errorPath))
            {
                // Create a file to write to.
                using (StreamWriter sw = System.IO.File.CreateText(errorPath))
                {
                    sw.WriteLine(line);
                }
            }
            else
            {
                using (StreamWriter sw = System.IO.File.AppendText(errorPath))
                {
                    sw.WriteLine(line);
                }
            }
        }

        public string[] uploadAndReadCSVFile(IFormFile file, IHostingEnvironment hosting)
        {
            #region Upload CSV
            string fileName = $"{hosting.WebRootPath}\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            #endregion

            #region Read CSV
            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + file.FileName;
            return System.IO.File.ReadAllLines(path);
            #endregion
        }

        public List<Tuple<string, string, string, string>> readErrorFile(string errorPath)
        {
            List<Tuple<string, string, string, string>> results = new List<Tuple<string, string, string, string>>();
            if (System.IO.File.Exists(errorPath))
            {
                using (StreamReader sr = System.IO.File.OpenText(errorPath))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        var values = s.Split(',');
                        results.Add(new Tuple<string, string, string, string>(values[0], values[2], values[1], values[3]));
                    }
                }
            }
            return results;
        }
    }
}
