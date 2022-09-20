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
    public class Ledger
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public List<Transaction> TransactionList { get; }
        public List<Person> PersonList { get; }
        public List<string> ErrorList { get; }
        public Ledger(List<Transaction> transactionList, List<string> errorList)
        {
            TransactionList = transactionList;
            ErrorList = errorList;
            PersonList = GetPeople();
        }

        private List<Person> GetPeople()
        {
            HashSet<string> tempList = new HashSet<string>();
            foreach (var transaction in TransactionList)
            {
                tempList.Add(transaction.To);
                tempList.Add(transaction.From);
            }
            List<Person> personList = new List<Person>();
            foreach (var person in tempList)
            {
                personList.Add(new Person(person));
            }
            return personList;
        }

        public decimal CalculateDebt(string name)
        {
            decimal owes = 0;
            decimal owed = 0;
            foreach (var transaction in TransactionList)
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
            foreach (var person in PersonList)
            {
                if (person.Name != name)
                {
                    decimal owes = 0;
                    decimal owed = 0;
                    foreach (var transaction in TransactionList)
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
            foreach (var person in PersonList)
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