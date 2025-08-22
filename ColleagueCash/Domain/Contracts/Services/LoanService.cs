using ColleagueCash.Domain.Contracts.Interfaces.IRepository;
using ColleagueCash.Domain.Contracts.Interfaces.IService;
using ColleagueCash.Domain.Entities;
using CollegueCashV2.Application.Configuration;
using Microsoft.Extensions.Options;

namespace ColleagueCash.Domain.Contracts.Services
{
    public class LoanService : ILoanService
    {

        private IBorrowerRepository _repositoryBorrower;

        private readonly AppConfig _appConfig;

        //public LoanService(AppConfig appConfig) => _appConfig = appConfig;

        private ILoanRepository _repositoryLoan;

        public LoanService(ILoanRepository repositoryLoan, IOptions<AppConfig> appConfig, IBorrowerRepository repositoryBorrower)
        {
            _repositoryLoan = repositoryLoan;
            _appConfig = appConfig.Value;
            _repositoryBorrower = repositoryBorrower;
        }

        public void RegisterNewLoan(decimal amount, string description, string name, string familyName)
        {
            //old 
            //_repositoryLoan.AddNewLoan(amount, description, name, familyName);


            //code 
            var storedBorrower = _repositoryBorrower.ExistedBorrower(name, familyName);
            var date = DateTime.Now;
            string newRegistration;
            int idRegistration = _repositoryLoan.GetNextId();

            if (!File.Exists(_appConfig.DataFilesCSV.LoanPath))
                File.WriteAllText(_appConfig.DataFilesCSV.LoanPath, "id;description;amount;date;idBorrower" + Environment.NewLine);

            if (storedBorrower != null)
            {
                newRegistration = $"{idRegistration};{description};{amount};{date:yyyy-MM-dd};{storedBorrower.BorrowerId}";
                File.AppendAllText(_appConfig.DataFilesCSV.LoanPath, newRegistration + Environment.NewLine);
                return;
            }

            var borrower = new Borrower
            {
                Name = name,
                FamilyName = familyName,
            };

            //ToDO
            //try catch // desfazer o que vc fez no lista de arquivo 
            Borrower newBorrower = _repositoryBorrower.AddNewBorrower(borrower);

            newRegistration = $"{idRegistration};{description};{amount};{date:yyyy-MM-dd};{newBorrower.BorrowerId}";
            File.AppendAllText(_appConfig.DataFilesCSV.LoanPath, newRegistration + Environment.NewLine);

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
