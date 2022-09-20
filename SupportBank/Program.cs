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
    class Program
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public static void Run(Ledger ledger)
        {
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
        static void Main(string[] args)
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Users\jachug\Documents\12.SupportBank\SupportBank\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;

            Console.WriteLine("++++Welcome to SupportBank, making people pay up since 2022!++++");
            Console.WriteLine("================================================================");
            Console.WriteLine();
            Ledger ledger = FileReader.CreateLedger(@"DodgyTransactions2015.csv");

            if (ledger.ErrorList.Count() == 0)
            {
                Run(ledger);
            }
            else
            {
                foreach (String error in ledger.ErrorList)
                {
                    Console.WriteLine(error);
                }
                Console.WriteLine("Would you like to continue? (Y) / (N)");
                string option = Console.ReadLine();
                if (option.ToUpper() == "Y")
                {
                    Run(ledger);
                }
            }
        }
    }
}
