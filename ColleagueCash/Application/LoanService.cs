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


        public void DisplayAllLoans()
        {
            var list = _repositoryLoan.GetAllLoans()
                .OrderBy( loan => loan.Description)
                .ToList();  

            Console.WriteLine("nome com a:" + list[0].Description);
        }

        public void RegisterNewLoan(Loan loan, Borrower borrower)
        {
            _repositoryLoan.AddNewLoan(loan, borrower); 
        }

        public void ReduceLoan(string name, string familyName, decimal amount)
        {
            _repositoryLoan.ReduceLoan(name, familyName, amount);
        }
    }
}
