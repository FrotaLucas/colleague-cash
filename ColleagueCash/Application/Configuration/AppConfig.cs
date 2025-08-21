namespace CollegueCashV2.Application.Configuration;

public class AppConfig
{
    public DataFilesCSVConfig DataFilesCSV { get; set; } = new();
    public sealed class DataFilesCSVConfig
    {
        public string loanPath { get; init; }
        public string borrowerPath { get; init; }
        public string borrowerIdPath { get; init; }
        public string loanIdPath { get; init; }
    }
}