using ColleagueCash.Application;
using ColleagueCash.Domain;
using ColleagueCash.Infrastructure;

class Program
{
    public static void Main(String[] args)
    {
        //COMO ESCONDER loanFile & borrowerFile DA APLICACAO? COLOCAR TUDO DENTRO DE UM app.config ??


        string loanPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
        string loanFile = Path.Combine(loanPath, "WorkLoad\\loan-registration.csv");


        string borrowerPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
        string borrowerFile = Path.Combine(borrowerPath, "WorkLoad\\borrower-registration.csv");


        IRepositoryLoan repositoryLoan = new RepositoryLoan(loanFile);

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