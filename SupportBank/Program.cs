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