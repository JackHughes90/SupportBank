using System;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("++++Welcome to SupportBank, making people pay up since 2022!++++");
            Console.WriteLine("================================================================");
            Console.WriteLine();
            Ledger ledger = new Ledger();
            while (true)
            {
                Console.WriteLine("Would you like to LIST ALL ACCOUNTS (1) or SELECT AN ACCOUNT (2)");
                string option = Console.ReadLine();
                if (option == "1")
                {
                    Console.WriteLine("================================================================");
                    Console.WriteLine();
                    Console.WriteLine("LIST OF ALL ACCOUNTS");
                    Console.WriteLine();
                    ledger.PrintAllBalances();
                }
                if (option == "2")
                {
                    Console.WriteLine("================================================================");
                    Console.WriteLine();
                    Console.WriteLine("Please enter an account name");
                    string name = Console.ReadLine();
                    ledger.IndividualAccount(name);
                }
                if (option != "1" && option != "2")
                {
                    Console.WriteLine("Select either 1 or 2");
                }
            }
        }
    }
}

// Console.WriteLine(Ledger.personList[0].Name);
// Console.WriteLine(ledger.CalculateDebt("Sarah T"));