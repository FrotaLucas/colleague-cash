using ColleagueCash.Domain.Entities;

namespace ColleagueCash.Domain
{
    public interface IRepositoryLoan
    {
        public void AddNewLoan(decimal amount, string description, string name, string familyName);

        public List<Loan> GetAllLoans();

        public List<Loan> GetAllLoansByBorrower(string name, string familyName);

        public void ReduceLoan(string name, string familyName, decimal amount);

        public int GetNextId();
    }
}
