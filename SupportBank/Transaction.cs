using System;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SupportBank
{
    public class Transaction
    {
        [Name("Date")]
        public string Date { get; set; }
        [Name("From")]
        public string From { get; set; }
        [Name("To")]
        public string To { get; set; }
        [Name("Narrative")]
        public string Narrative { get; set; }
        [Name("Amount")]
        public double Amount { get; set; }
    }
}