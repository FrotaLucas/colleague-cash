using ColleagueCash.Domain;

namespace ColleagueCash.Application
{
    public class LoanService
    {
        private IRepositoryLoan _repositoryLoan;

        public LoanService(IRepositoryLoan repositoryLoan)
        {
            _repositoryLoan = repositoryLoan;
        }

        public void RegisterNewLoan(Loan loan, Borrower borrower)
        {
            _repositoryLoan.AddNewLoan(loan, borrower);
        }

        public void ReduceLoan(string name, string familyName, decimal amount)
        {
            _repositoryLoan.ReduceLoan(name, familyName, amount);
        }


        public List<Loan> DisplayAllLoansByDate()
        {
            var list = _repositoryLoan.GetAllLoans()
                .OrderByDescending(loan => loan.LoanDate)
                .ToList();
            return list;
        }

        public List<Loan> DisplayAllLoansByDescription()
        {
            var list = _repositoryLoan.GetAllLoans()
                .OrderBy( loan => loan.Description)
                .ToList();  
            return list;
        }

        public List<Loan> DisplayAllLoansByAmount()
        {
            var list = _repositoryLoan.GetAllLoans()
                .OrderByDescending(loan => loan.Amount)
                .ToList();
            return list;
        }

        public List<Loan> DisplayAllLoanByName(string name, string familyName)
        {
            var list = _repositoryLoan.GetAllLoansByPerson(name, familyName);
            return list;
        }

      
    }
}
