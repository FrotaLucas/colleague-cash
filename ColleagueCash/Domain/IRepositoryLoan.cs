namespace ColleagueCash.Domain
{
    public interface IRepositoryLoan
    {
        public void AddNewLoan(Loan loan, Borrower borrower);

        public List<Loan> GetAllLoans();

        public List<Loan> GetAllLoansByPerson(string name, string familyName);

        public void ReduceLoan(string name, string familyName, decimal amount);

        public int GetNextId();
    }
}
