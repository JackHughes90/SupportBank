using System;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank
{
    public static class FileReader
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public static Ledger CreateLedger(string filePath)
        {
            List<Transaction> transactionList = new List<Transaction>();
            List<string> errorList = new List<string>();
            using (var streamReader = new StreamReader(filePath))
            {
                using (var csvReader = new CsvReader(streamReader, new CultureInfo("en-GB")))
                {
                    csvReader.Read();
                    csvReader.ReadHeader();
                    while (csvReader.Read())
                    {
                        try
                        {
                            Transaction transaction = csvReader.GetRecord<Transaction>();
                            transactionList.Add(transaction);
                        }
                        catch (CsvHelper.TypeConversion.TypeConverterException ex)
                        {
                            Logger.Error(ex);
                            errorList.Add("There is a problem in your file! See Row " + ex.Context.Parser.Row + " in the " + csvReader.HeaderRecord[csvReader.CurrentIndex] + " column");
                        }
                        catch (CsvHelper.ReaderException ex)
                        {
                            Logger.Error(ex);
                            errorList.Add("There is a problem in your file! See Row " + ex.Context.Parser.Row + " in the " + csvReader.HeaderRecord[csvReader.CurrentIndex] + " column");
                        }
                    }
                }
            }
            return new Ledger(transactionList, errorList);
        }
    }

}
