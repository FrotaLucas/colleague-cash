using ColleagueCash.Domain.Entities;

namespace ColleagueCash.Domain.Contracts.Interfaces.IRepository
{
    public interface IBorrowerRepository
    {
        public Borrower AddNewBorrower(Borrower borrower);

        public List<Borrower> GetAllBorrowers();

        public Borrower GetBorrowerByEmail(string name, string familyName);   

        public int GetNextId();
    }
}
