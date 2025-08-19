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

        public List<Loan> DisplayAllLoansByDate()
        {
            var list = _repositoryLoan.GetAllLoans()
                .OrderByDescending(loan => loan.LoanDate)
                .ToList();
            return list;
        }

        public List<Loan> DisplayAllLoanByName(string name, string familyName)
        {
            var list = _repositoryLoan.GetAllLoansByPerson(name, familyName);
            return list;
        }

        //DESNECESSARIO
        //public List<Loan> DisplayAllLoansByDescription()
        //{
        //    var list = _repositoryLoan.GetAllLoans()
        //        .OrderBy( loan => loan.Description)
        //        .ToList();  
        //    return list;
        //}

    }
}
