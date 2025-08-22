using ColleagueCash.Domain.Contracts.Interfaces.IRepository;
using ColleagueCash.Domain.Contracts.Interfaces.IService;
using ColleagueCash.Domain.Entities;
using CollegueCashV2.Application.Configuration;
using Microsoft.Extensions.Options;

namespace ColleagueCash.Domain.Contracts.Services
{
    public class LoanService : ILoanService
    {
        private readonly AppConfig _appConfig;
        private ILoanRepository _repositoryLoan;
        private IBorrowerRepository _repositoryBorrower;
        private IBorrowerService _borrowerService;
        

        public LoanService(
            IOptions<AppConfig> appConfig, 
            ILoanRepository repositoryLoan, 
            IBorrowerRepository repositoryBorrower,
            IBorrowerService borrowerService)
        {
            _appConfig = appConfig.Value;
            _repositoryLoan = repositoryLoan;
            _repositoryBorrower = repositoryBorrower;
            _borrowerService = borrowerService;
        }

        public void RegisterNewLoan(decimal amount, string description, string name, string familyName)
        {
            Borrower storedBorrower = _repositoryBorrower.GetBorrowerByEmail(name, familyName);
            
            int idRegistration = _repositoryLoan.GetNextId();

            if (!File.Exists(_appConfig.DataFilesCSV.LoanPath))
                File.WriteAllText(_appConfig.DataFilesCSV.LoanPath, "id;description;amount;date;idBorrower" + Environment.NewLine);
            
            if (storedBorrower is null)
            {
                storedBorrower = new Borrower();
                int borrowerId = _borrowerService.AddNewBorrower(name, familyName);
                storedBorrower.BorrowerId = borrowerId;
            }
            
            string newRegistration = $"{idRegistration};{description};{amount};{DateTime.Now:yyyy-MM-dd};{storedBorrower.BorrowerId}";
            _repositoryLoan.AddNewLoan(newRegistration);
        }

        public void ReduceLoan(string name, string familyName, decimal amount)
        {
            _repositoryLoan.ReduceLoan(name, familyName, amount);
        }

        public void DisplayAllLoansByAmount()
        {
            var loans = _repositoryLoan.GetAllLoans()
                .Where(loan => loan.Amount > 0)
                .OrderByDescending(loan => loan.Amount);

            foreach (var loan in loans)                      
            {
                Console.WriteLine(
                    $"Outstanding amount: {loan.Amount} - " +
                    $"Description: {loan.Description} - " +
                    $"Date of registration: {loan.LoanDate}"
                );
            }
        }

        public void DisplayAllLoansByDate()
        {
            var loans = _repositoryLoan.GetAllLoans()
                .Where(loan => loan.Amount > 0)
                .OrderByDescending(loan => loan.LoanDate);

            foreach (var loan in loans)
            {
                Console.WriteLine(
                    $"Outstanding amount: {loan.Amount} - " +
                    $"Description: {loan.Description} - " +
                    $"Date of registration: {loan.LoanDate}"
                );
            }
        }

        public void DisplayAllLoansOfColleague(string name, string familyName)
        {
            var loans = _repositoryLoan.GetAllLoansByBorrower(name, familyName)
                .Where(loan => loan.Amount > 0)
                .OrderByDescending(loan => loan.LoanDate);


            foreach (var loan in loans)
            {
                Console.WriteLine(
                    $"Outstanding amount: {loan.Amount} - " +
                    $"Description: {loan.Description} - " +
                    $"Date of registration: {loan.LoanDate}"
                );
            }

        }

    }
}
