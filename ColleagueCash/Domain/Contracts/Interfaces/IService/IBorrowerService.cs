using ColleagueCash.Domain.Entities;

namespace ColleagueCash.Domain.Contracts.Interfaces.IService
{
    public interface IBorrowerService
    {
        public void GetAllBorrowersOrderedByName();

        public List<Borrower> GetAllBorrowersWithLoans();

        public int AddNewBorrower(string name, string familyName, int? cellphone);
    }
}
