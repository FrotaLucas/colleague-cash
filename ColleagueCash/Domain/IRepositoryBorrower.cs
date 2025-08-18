namespace ColleagueCash.Domain
{
    public interface IRepositoryBorrower
    {
        public void AddNewBorrower(Borrower borrower);

        public List<Borrower> GetAllBorrowers();

        public bool BorrowerHasActiveLoan(string Name, string FamilyName);   
    }
}
