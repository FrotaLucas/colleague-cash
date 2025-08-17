namespace ColleagueCash.Domain
{
    public interface IRepositoryLoan
    {
        public void AddnewRegistration(Loan loan);

        public List<Loan> ListLoans();

    }
}
