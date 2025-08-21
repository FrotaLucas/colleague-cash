namespace CollegueCashV2.Application.Configuration;

public class AppConfig
{
    public DataFilesCSVConfig DataFilesCSV { get; set; } = new();
    public sealed class DataFilesCSVConfig
    {
        public string LoanPath { get; init; }
        public string BorrowerPath { get; init; }
        public string LastBorrowerIdFile { get; init; }
        public string LastLoanIdFile { get; init; }
    }

}