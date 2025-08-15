using ColleagueCash.Application;
using ColleagueCash.Domain;
using ColleagueCash.Infrastructure;

class Program
{
    public static void Main(String[] args)
    {
        string baseDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
        string fileName = Path.Combine(baseDirectory, "WorkLoad\\loan-registration.csv");

        //2T
        //string baseDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
        //string fileInvestments = Path.Combine(baseDirectory, "Csv\\InvestmentsT.csv");

        Console.WriteLine("hier:" + fileName);

        IRepositoryLoan repositoryLoan = new RepositoryLoan(fileName);

        var listLoanHandler = new ListLoanHandler(repositoryLoan);
        var registerLoanHandler = new RegisterLoanHandler(repositoryLoan);

        listLoanHandler.Execute();
    }
}