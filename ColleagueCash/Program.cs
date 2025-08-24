using System.Threading.Channels;
using ColleagueCash.Application.Configuration;
using ColleagueCash.Domain.Contracts.Interfaces.IRepository;
using ColleagueCash.Domain.Contracts.Interfaces.IService;
using ColleagueCash.Domain.Contracts.Services;
using ColleagueCash.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    public static void Main(String[] args)
    {
        string baseDir = AppContext.BaseDirectory;

        // sobe 3 níveis até a raiz do projeto
        string projectDirectory = Directory.GetParent(baseDir)!.Parent!.Parent!.Parent!.FullName;

        //new code
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((ctx, cfg) =>
            {
                cfg.SetBasePath(AppContext.BaseDirectory);
                cfg.AddJsonFile(Path.Combine("Application", "appsettings.json"), optional: true, reloadOnChange: true);
                cfg.AddJsonFile(
                    Path.Combine("Application", $"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json"),
                    optional: true, reloadOnChange: true);
                cfg.AddEnvironmentVariables();
            })
            .ConfigureServices((ctx, services) =>
            {
                Console.WriteLine(ctx.Configuration["DataFilesCSV:LoanPath"]);

                // ✨ Bind do appsettings inteiro
                //opt1
                //var appConfig = ctx.Configuration.Get<AppConfig>();
                //services.AddSingleton(appConfig);

                //opt2
                //services.Configure<AppConfig>(ctx.Configuration);

                //op3
                // Bind + ajustar caminhos absolutos
                services.Configure<AppConfig>(options =>
                {
                    ctx.Configuration.Bind(options);


                    options.DataFilesCSV.LoanPath = Path.Combine(projectDirectory, options.DataFilesCSV.LoanPath);
                    options.DataFilesCSV.BorrowerPath =
                        Path.Combine(projectDirectory, options.DataFilesCSV.BorrowerPath);
                });

                services.AddSingleton<ILoanService, LoanService>();
                services.AddSingleton<ILoanRepository, LoanRepository>();
                services.AddSingleton<IBorrowerRepository, BorrowerRepository>();
                services.AddSingleton<IBorrowerService, BorrowerService>();
                // … registre seus outros serviços depois e injete AppConfig no servico que precisar neles
            })
            .Build();

        //op1
        //AppConfig cfg = host.Services.GetRequiredService<AppConfig>();


        //op2
        var cfg = host.Services.GetRequiredService<Microsoft.Extensions.Options.IOptions<AppConfig>>().Value;
        Console.WriteLine($"[Main] Path Loan is: {cfg.DataFilesCSV.LoanPath}");


        var loanService = host.Services.GetRequiredService<ILoanService>();
        var borrowerService = host.Services.GetRequiredService<IBorrowerService>();

        //old code
        //string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        //string projectDirectory = Path.GetFullPath(Path.Combine(appDirectory, @"..\..\.."));

        //var config = new ConfigurationBuilder()
        //    .SetBasePath(projectDirectory)
        //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //    .Build();

        //var fileSettings = config.GetSection("FileSettings").Get<FileSettings>();

        //string loanFile = Path.Combine(projectDirectory, fileSettings.LoanFile);
        //string borrowerFile = Path.Combine(projectDirectory, fileSettings.BorrowerFile);
        //string lastBorrowerIdFile = Path.Combine(projectDirectory, fileSettings.LastBorrowerIdFile);
        //string lastLoanIdFile = Path.Combine(projectDirectory, fileSettings.LastLoanIdFile);


        //IRepositoryBorrower repositoryBorrower = new RepositoryBorrower(borrowerFile, lastBorrowerIdFile);
        //var borrowerService = new BorrowerService(repositoryBorrower);

        //IRepositoryLoan repositoryLoan = new RepositoryLoan(loanFile, lastLoanIdFile, repositoryBorrower);
        //var loanService = new LoanService(repositoryLoan);


        while (true)
        {
            Console.WriteLine("\n=== Loan Tracker ===");
            Console.WriteLine("1 - Register new Loan\n");
            Console.WriteLine("2 - Settling a colleague debt\n");
            Console.WriteLine("3 - List all debts ordered by amount\n"); //By amount
            Console.WriteLine("4 - List all debts ordered by date\n");
            Console.WriteLine("5 - List all debts of a colleague\n");
            Console.WriteLine("6 - List all your colleagues\n");
            Console.WriteLine("0 - Exit\n");
            Console.Write("Your option: ");

            var choice = Console.ReadLine();


            string name = string.Empty;
            string familyName = string.Empty;
            decimal amount = 0;
            string description = string.Empty;

            switch (choice)
            {
                case "1":
                    Console.Write("Name and Last name of the Colleague. (ex. Thomas Müller): ");

                    var inputName = Console.ReadLine().ToLower();
                    string[] fullName1;

                    if (!inputName.Contains(' '))
                    {
                        Console.WriteLine("Please enter your full name (First Name and Last name).");
                        fullName1 = Console.ReadLine().ToLower().Split(' ');
                        name = fullName1[0];
                        familyName = fullName1[1];
                    }

                    else
                    {
                        fullName1 = inputName.Split(' ');
                        name = fullName1[0];
                        familyName = fullName1[1];
                    }



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

                        loanService.RegisterNewLoan(amount, description, name, familyName);
                        Console.WriteLine("Loan registered!");
                    }

                    else
                        Console.WriteLine("Invalid Amount");

                    break;

                case "2":
                    Console.Write("Name and Family name of the Colleague. (ex. Thomas Müller): ");
                    var inputName2 = Console.ReadLine().ToLower();
                    string[] fullName2;

                    if (!inputName2.Contains(' '))
                    {
                        Console.WriteLine("Please enter your full name (First Name and Last name).");
                        fullName2 = Console.ReadLine().ToLower().Split(' ');
                        name = fullName2[0];
                        familyName = fullName2[1];
                    }

                    else
                    {
                        fullName2 = inputName2.Split(' ');
                        name = fullName2[0];
                        familyName = fullName2[1];
                    }

                    Console.Write("Amount to be paid: ");
                    if (decimal.TryParse(Console.ReadLine(), out amount))
                    {
                        if (loanService.ReduceLoan(name, familyName, amount))
                            Console.WriteLine("Payment registered!");
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
                    var inputName5 = Console.ReadLine().ToLower();
                    string[] fullName5;

                    if (!inputName5.Contains(' '))
                    {
                        Console.WriteLine("Please enter your full name (First Name and Last name).");
                        fullName5 = Console.ReadLine().ToLower().Split(' ');
                        name = fullName5[0];
                        familyName = fullName5[1];
                    }

                    else
                    {
                        fullName5 = inputName5.Split(' ');
                        name = fullName5[0];
                        familyName = fullName5[1];
                    }

                    loanService.DisplayAllLoansOfColleague(name, familyName);
                    break;

                case "6":
                    borrowerService.GetAllBorrowersOrderedByName();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Wrong option! Choose a number from menu.");
                    break;
            }
        }
    }
}