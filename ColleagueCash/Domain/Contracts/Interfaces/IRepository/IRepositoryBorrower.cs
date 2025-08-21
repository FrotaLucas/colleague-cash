using ColleagueCash.Domain.Entities;

namespace ColleagueCash.Domain.Contracts.Interfaces.IRepository
{
    public interface IRepositoryBorrower
    {
        public Borrower AddNewBorrower(Borrower borrower);

        public List<Borrower> GetAllBorrowers();

        public Borrower ExistedBorrower(string Name, string FamilyName);   

        public int GetNextId();
    }
}
