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
    class Ledger
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public List<Transaction> transactionList { get; set; }
        public List<Person> personList { get; set; }
        public Ledger()
        {
            transactionList = new List<Transaction>();
            using (var streamReader = new StreamReader(@"DodgyTransactions2015.csv"))
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
                            Console.WriteLine("There is a problem in your file! See Row " + ex.Context.Parser.Row + " in the " + csvReader.HeaderRecord[csvReader.CurrentIndex] + " column");
                            Console.WriteLine("Would you like to continue? (Y) / (N)");
                            string option = Console.ReadLine();
                            if (option.ToUpper() == "N")
                            {
                                throw;
                            }
                        }
                        catch (CsvHelper.ReaderException ex)
                        {
                            Logger.Error(ex);
                            Console.WriteLine("There is a problem in your file! See Row " + ex.Context.Parser.Row + " in the " + csvReader.HeaderRecord[csvReader.CurrentIndex] + " column");
                            Console.WriteLine("Would you like to continue? (Y) / (N)");
                            string option = Console.ReadLine();
                            if (option.ToUpper() == "N")
                            {
                                throw;
                            }
                        }
                    }
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

        public decimal CalculateDebt(string name)
        {
            decimal owes = 0;
            decimal owed = 0;
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
            decimal debt = owed - owes;
            return debt;
        }
        public void IndividualAccount(string name)
        {
            Console.WriteLine();
            Console.WriteLine("The ledger for " + name + ":");
            Console.WriteLine();
            foreach (var person in personList)
            {
                if (person.Name != name)
                {
                    decimal owes = 0;
                    decimal owed = 0;
                    foreach (var transaction in transactionList)
                    {
                        if (transaction.From == name && transaction.To == person.Name)
                        {
                            owes += transaction.Amount;
                        }
                        if (transaction.To == name && transaction.From == person.Name)
                        {
                            owed += transaction.Amount;
                        }
                    }
                    decimal debt = owed - owes;
                    if (debt < 0)
                    {
                        Console.WriteLine(name + " is owed £" + (0 - debt) + " by " + person.Name);
                    }
                    if (debt > 0)
                    {
                        Console.WriteLine(name + " owes " + person.Name + " £" + debt);
                    }
                    if (debt == 0)
                    {
                        Console.WriteLine(name + " and " + person.Name + " are all square!");
                    }
                }
            }
        }
        public void PrintAllBalances()
        {
            foreach (var person in personList)
            {
                if (CalculateDebt(person.Name) < 0)
                {
                    Console.WriteLine(person.Name + " is owed £" + (0 - CalculateDebt(person.Name)));
                }
                else
                {
                    Console.WriteLine(person.Name + " owes £" + CalculateDebt(person.Name));
                }
            }
        }
    }
}