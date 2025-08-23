namespace ColleagueCash.Application.Configuration;

public class AppConfig
{
    public DataFilesCSVConfig DataFilesCSV { get; set; } = new();
    public sealed class DataFilesCSVConfig
    {
        public string LoanPath { get; set; }
        public string BorrowerPath { get; set; }
    }
}