using ColleagueCash.Domain.Contracts.Interfaces.IRepository;
using ColleagueCash.Domain.Contracts.Interfaces.IService;
using CollegueCashV2.Application.Configuration;
using Microsoft.Extensions.Options;

namespace ColleagueCash.Domain.Contracts.Services
{
    public class LoanService : ILoanService
    {

        private readonly AppConfig _appConfig;

        //public LoanService(AppConfig appConfig) => _appConfig = appConfig;

        private ILoanRepository _repositoryLoan;

        public LoanService(ILoanRepository repositoryLoan, IOptions<AppConfig> appConfig)
        {
            _repositoryLoan = repositoryLoan;
            _appConfig = appConfig.Value;
        }

        public void RegisterNewLoan(decimal amount, string description, string name, string familyName)
        {
            _repositoryLoan.AddNewLoan(amount, description, name, familyName);
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
