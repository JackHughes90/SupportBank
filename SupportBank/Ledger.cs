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

            HashSet<string> tempList = new HashSet<string>();
            foreach (var transaction in transactionList)
            {
                tempList.Add(transaction.To);
                tempList.Add(transaction.From);
            }
            List<Person> PersonList = new List<Person>();
            foreach (var person in tempList)
            {
                Person peerson = new Person(person);
                PersonList.Add(new Person(person));
            }
            personList = PersonList;
        }

        public double CalculateDebt(string name)
        {
            double owes = 0;
            double owed = 0;
            foreach (var transaction in transactionList)
            {
                if (transaction.From == name)
                {
                    owes += transaction.Amount;
                }
                if (transaction.To == name)
                {
                    owed += transaction.Amount;
                }
            }
            double debt = owed - owes;
            return debt;
        }

        public void PrintAllBalances()
        {
            foreach (var person in personList)
            {
                Console.WriteLine(person.Name + " owes " + CalculateDebt(person.Name));
            }
        }
    }
}