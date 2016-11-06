using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DBImporterClient
{
    class CSVReader
    {
        public static string ReadCSV(string path)
        {
            List<string> lines = new List<string>();
            var reader = new StreamReader(File.OpenRead(path));
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                lines.Add(line);
            }
            return string.Join("\n", lines);
        }
    }
}
