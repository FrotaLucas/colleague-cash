namespace ColleagueCash.Domain
{
    public interface IBorrowerRepository
    {
        public void AddNewBorrower(Borrower borrower);

        public List<Borrower> GetAllBorrowers();
    }
}
