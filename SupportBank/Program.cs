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
            Console.WriteLine("Welcome to SupportBank, making people pay up since 2022!");
            Console.WriteLine("========================================================");
            Ledger ledger = new Ledger();
            Console.WriteLine(Ledger.personList[0].Name);
            Console.WriteLine(ledger.CalculateDebt("Sarah T"));
        }
    }
}