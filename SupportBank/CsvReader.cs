using System;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;

namespace SupportBank
{
    class FileReader
    {
        public static void ReadFile()
        {
            using (var streamReader = new StreamReader(@"Transactions2014.csv"))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    var records = csvReader.GetRecords<dynamic>().ToList();
                }

                // List<string> date = new List<string>();
                // List<string> from = new List<string>();
                // List<string> to = new List<string>();
                // List<string> narrative = new List<string>();
                // List<string> amount = new List<string>();
                // while (!reader.EndOfStream)
                // {
                //     var line = reader.ReadLine();
                //     var values = line.Split(',');

                //     date.Add(values[0]);
                //     from.Add(values[1]);
                //     to.Add(values[2]);
                //     narrative.Add(values[3]);
                //     amount.Add(values[4]);
                // }

                // Console.WriteLine(date);
            }

        }
    }
}