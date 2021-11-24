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
    }
}
