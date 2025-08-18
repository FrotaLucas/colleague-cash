namespace ColleagueCash.Domain
{
    public interface IRepositoryLoan
    {
        public void AddNewLoan(Loan loan, Borrower borrower);

        public List<Loan> GetAllLoans();

    }
}
