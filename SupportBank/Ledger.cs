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
        public static List<Person> personList { get; set; }
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
        public static List<Person> PersonList()
        {
            HashSet<string> tempList = new HashSet<string>();
            foreach (var transaction in transactionList)
            {
                tempList.Add(transaction.To);
                tempList.Add(transaction.From);
            }
            List<Person> personList = new List<Person>();
            foreach (var person in tempList)
            {
                Person peerson = new Person(person);
                personList.Add(new Person(person));
            }
            return personList;
        }
    }
}