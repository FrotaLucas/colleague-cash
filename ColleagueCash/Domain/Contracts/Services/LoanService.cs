using ColleagueCash.Application.Configuration;
using ColleagueCash.Domain.Contracts.Interfaces.IRepository;
using ColleagueCash.Domain.Contracts.Interfaces.IService;
using ColleagueCash.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ColleagueCash.Domain.Contracts.Services
{
    public class LoanService : ILoanService
    {
        private ILoanRepository _repositoryLoan;
        private IBorrowerRepository _repositoryBorrower;
        private IBorrowerService _borrowerService;
        

        public LoanService(
            ILoanRepository repositoryLoan, 
            IBorrowerRepository repositoryBorrower,
            IBorrowerService borrowerService)
        {
            _repositoryLoan = repositoryLoan;
            _repositoryBorrower = repositoryBorrower;
            _borrowerService = borrowerService;
        }

        public void RegisterNewLoan(decimal amount, string description, string name, string familyName)
        {
            Borrower storedBorrower = _repositoryBorrower.GetBorrowerByEmail(name, familyName);
            
            int idRegistration = _repositoryLoan.GetNextId();

            if (storedBorrower is null)
            {
                storedBorrower = new Borrower();
                int borrowerId = _borrowerService.AddNewBorrower(name, familyName);
                storedBorrower.BorrowerId = borrowerId;
            }
            
            string newRegistration = $"{idRegistration};{description};{amount};{DateTime.Now:yyyy-MM-dd};{storedBorrower.BorrowerId}";
            _repositoryLoan.AddNewLoan(newRegistration);
        }

        public bool ReduceLoan(string name, string familyName, decimal amount)
        {
            var borrower = _repositoryBorrower.GetBorrowerByEmail(name, familyName);

            if (borrower != null && borrower.BorrowerId != null)
            {
                var listOfLoan = _repositoryLoan.GetAllLoansByBorrowerId(borrower.BorrowerId)
                    .OrderBy(loan => loan.LoanDate)
                    .ToList();

                int i = 0;
                while (amount > 0 && i < listOfLoan.Count)
                {
                    if (amount >= listOfLoan[i].Amount)
                    {
                        amount -= listOfLoan[i].Amount;
                        listOfLoan[i].Amount = 0;
                    }

                    else
                    {
                        listOfLoan[i].Amount -= amount;
                        amount = 0;
                    }
                    i++;
                }

                if (amount > 0)
                    Console.WriteLine($"Warining: Payment amount exceeds the total loan");

                _repositoryLoan.ReduceLoan(listOfLoan);
                return true;
            }

            else
            {
                Console.WriteLine("Colleage not registered yet.");
                return false;
            }
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
                .OrderBy(loan => loan.LoanDate);


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
