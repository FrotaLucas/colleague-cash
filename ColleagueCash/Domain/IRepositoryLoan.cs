namespace ColleagueCash.Domain
{
    public interface IRepositoryLoan
    {
        public void AddNewRegistration(Loan loan, Borrower borrower);

        public List<Loan> GetAllLoans();

    }
}
