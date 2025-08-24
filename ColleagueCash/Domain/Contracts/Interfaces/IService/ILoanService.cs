using ColleagueCash.Infrastructure;

namespace ColleagueCash.Domain.Contracts.Interfaces.IService
{
    public interface ILoanService
    {
        public void RegisterNewLoan(decimal amount, string description, string name, string familyName, int? cellphone);

        public bool ReduceLoan(string name, string familyName, decimal amount);

        public void DisplayAllLoansByAmount();

        public void DisplayAllLoansByDate();

        public void DisplayAllLoansOfColleague(string name, string familyName);
       
    }
}
