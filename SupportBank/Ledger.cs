using System;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SupportBank
{
    class Ledger
    {
        public static List<Transaction> transactionList { get; set; }
        public Ledger()
        {
            using (var streamReader = new StreamReader(@"Transactions2014.csv"))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    transactionList = csvReader.GetRecords<Transaction>().ToList();
                }
            }
        }
        public static HashSet<string> PersonList()
        {
            HashSet<string> personList = new HashSet<string>();
            foreach (var transaction in transactionList)
            {
                personList.Add(transaction.To);
                personList.Add(transaction.From);
            }
            return personList;
        }
    }
}