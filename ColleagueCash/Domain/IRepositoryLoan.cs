namespace ColleagueCash.Domain
{
    public interface IRepositoryLoan
    {
        public void AddNewLoan(string name, string familyName, decimal amount, string description);

        public List<Loan> GetAllLoans();

        public List<Loan> GetAllLoansByPerson(string name, string familyName);

        public void ReduceLoan(string name, string familyName, decimal amount);

        public int GetNextId();
    }
}
