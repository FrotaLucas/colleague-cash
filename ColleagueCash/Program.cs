using ColleagueCash.Domain;
using ColleagueCash.Infrastructure;

class Program
{
    public static void Main(String[] args)
    {
        string baseDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
        string filePath = Path.Combine(baseDirectory, "WorkLoad\\loan-registration.csv");

        //2T
        //string baseDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
        //string fileInvestments = Path.Combine(baseDirectory, "Csv\\InvestmentsT.csv");

        Console.WriteLine("hier:" + filePath);

        string fileName = string.Empty;

        IRepositoryLoan repositoryLoan = new RepositoryLoan(fileName);
        

    }
}