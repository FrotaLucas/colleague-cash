namespace ColleagueCash.Domain
{
    public interface IRepositoryBorrower
    {
        public Borrower AddNewBorrower(Borrower borrower);

        public List<Borrower> GetAllBorrowers();

        public Borrower BorrowerHasActiveLoan(string Name, string FamilyName);   
    }
}
