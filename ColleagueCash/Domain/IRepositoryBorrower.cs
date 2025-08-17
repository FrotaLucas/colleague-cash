namespace ColleagueCash.Domain
{
    public interface IRepositoryBorrower
    {
        public void AddNewBorrower(Borrower borrower);

        public List<Borrower> GetAllBorrowers();
    }
}
