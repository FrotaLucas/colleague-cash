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

        //last id borrower
        string borrowerIdPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
        string borrowerSize = Path.Combine(borrowerIdPath, "WorkLoad\\last-id.txt");


        IRepositoryLoan repositoryLoan = new RepositoryLoan(loanFile);

        var loanService = new LoanService(repositoryLoan);

        //loanService.Execute();

        Loan loan = new Loan
        {
            Description = "Sushi",
            Amount = 100,   
        };


        //BorrowerService


        Borrower borrower = new Borrower
        {
            Name = "Nathan",
            FamilyName = "Otta",

        };

        IRepositoryBorrower repositoryBorrower = new RepositoryBorrower(borrowerFile, borrowerSize);

        BorrowerService borrowerService = new BorrowerService(repositoryBorrower);

        //borrowerService.AddNewBorrower(borrower);

        var borrowers = borrowerService.GetAllBorrowersByName();
        foreach(var item in borrowers)
        {
            Console.WriteLine(item.Name);
            Console.WriteLine(item.FamilyName);
            Console.WriteLine(item.Cellphone);
        }

    }
}