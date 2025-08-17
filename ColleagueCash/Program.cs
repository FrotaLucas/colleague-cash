using ColleagueCash.Application;
using ColleagueCash.Domain;
using ColleagueCash.Infrastructure;

class Program
{
    public static void Main(String[] args)
    {
        //COMO ESCONDER ESSA PARTE DA APLICACAO? COLOCAR TUDO DENTRO DE UM app.config ??
        string baseDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
        string fileName = Path.Combine(baseDirectory, "WorkLoad\\loan-registration.csv");


        string borrowerPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
        string borrowerFile = Path.Combine(baseDirectory, "WorkLoad\\loan-registration.csv");


        IRepositoryLoan repositoryLoan = new RepositoryLoan(fileName);

        var listLoanHandler = new ListLoanHandler(repositoryLoan);
        var registerLoanHandler = new RegisterLoanHandler(repositoryLoan);

        //listLoanHandler.Execute();

        Loan loan = new Loan
        {
            Description = "Sushi",
            Amount = 100,   
        };

        registerLoanHandler.Execute(loan);
    }
}