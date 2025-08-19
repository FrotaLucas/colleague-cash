using System.Net.Sockets;
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
        string borrowerSize = Path.Combine(borrowerIdPath, "WorkLoad\\last-borrowerId.txt");

        //last id borrower
        string loanIdPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
        string loanSize = Path.Combine(loanIdPath, "WorkLoad\\last-loanId.txt");


        IRepositoryBorrower repositoryBorrower = new RepositoryBorrower(borrowerFile, borrowerSize);
        var borrowerService = new BorrowerService(repositoryBorrower);

        IRepositoryLoan repositoryLoan = new RepositoryLoan(loanFile, loanSize, repositoryBorrower);
        var loanService = new LoanService(repositoryLoan);

        while (true)
        {
            Console.WriteLine("\n=== Loan Tracker ===");
            Console.WriteLine("1 - Register new Loan");
            Console.WriteLine("2 - Settling a colleague debt");
            Console.WriteLine("3 - List all debts ordered by amount"); //By amount
            Console.WriteLine("4 - List all debts ordered by date");
            Console.WriteLine("5 - List all debts of a colleague");
            Console.WriteLine("6 - List all your colleague");
            Console.WriteLine("0 - Exit");
            Console.Write("Your option: ");

            var choice = Console.ReadLine();


            string name = string.Empty;
            string familyName = string.Empty;
            decimal amount = 0;
            string description = string.Empty;

            switch (choice)
            {
                case "1":
                    Console.Write("Name and Family name of the Colleague. (ex. Thomas Müller): ");
                    var fullName1 = Console.ReadLine().ToLower().Split(' ');

                    name = fullName1[0];
                    familyName = fullName1[1];

                    //string? cellphone = null;
                    //Console.WriteLine("Cellphone Number: Y/N:");
                    //{
                    //    Console.Write("Enter cellphone number: ");
                    //    cellphone = Console.ReadLine();
                    //}

                    Console.Write("Amount: ");
                    if (decimal.TryParse(Console.ReadLine(), out amount))
                    {
                        Console.Write("Short description for the loan: ");
                        description = Console.ReadLine();
                     
                        loanService.RegisterNewLoan(amount, description, name, familyName );
                        Console.WriteLine("✅ Loan registered!");
                    }

                    else
                    {
                        Console.WriteLine("Invalid Amount");
                    }
                    break;

                case "2":
                    Console.Write("Name and Family name of the Colleague. (ex. Thomas Müller): ");
                    var fullName = Console.ReadLine().ToLower().Split(' ');

                    name = fullName[0];
                    familyName = fullName[1];

                    Console.Write("Amount to be paid: ");
                    if (decimal.TryParse(Console.ReadLine(), out amount))
                    {
                        loanService.ReduceLoan(name, familyName, amount);
                        Console.WriteLine("✅ Payment registered!");
                    }

                    else
                    {
                        Console.WriteLine("Invalid Amount");
                    }
                    break;

                case "3":
                    loanService.DisplayAllLoansByAmount();
                    break;

                case "4":
                    loanService.DisplayAllLoansByDate();
                    break;

                case "5":
                    Console.Write("Name and Family name of the Colleague. (ex. Thomas Müller): ");
                    var fullName5 = Console.ReadLine().ToLower().Split(' ');

                    name = fullName5[0];
                    familyName = fullName5[1];

                    loanService.DisplayAllLoansOfColleague(name, familyName); 
                    break;

                case "6":
                    borrowerService.GetAllBorrowersOrderedByName();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("wrong option. Choose a number from menu");
                    break;
            }
        }

    }
}