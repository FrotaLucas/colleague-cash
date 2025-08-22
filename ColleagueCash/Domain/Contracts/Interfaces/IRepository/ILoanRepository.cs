using ColleagueCash.Domain.Entities;

namespace ColleagueCash.Domain.Contracts.Interfaces.IRepository
{
    public interface ILoanRepository
    {
        public void AddNewLoan(string newRegistration);

        public List<Loan> GetAllLoans();

        public List<Loan> GetAllLoansByBorrower(string name, string familyName);

        public void ReduceLoan(string name, string familyName, decimal amount);

        public int GetNextId();
    }
}
