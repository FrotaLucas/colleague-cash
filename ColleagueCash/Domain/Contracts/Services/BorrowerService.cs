using ColleagueCash.Domain.Contracts.Interfaces.IRepository;
using ColleagueCash.Domain.Contracts.Interfaces.IService;
using ColleagueCash.Domain.Entities;

namespace ColleagueCash.Domain.Contracts.Services
{
    public class BorrowerService : IBorrowerService
    {

        public readonly IBorrowerRepository _repositoryBorrower;

        public BorrowerService(IBorrowerRepository repositoryBorrower)
        {
            _repositoryBorrower = repositoryBorrower;
        }

        public int AddNewBorrower(string name, string familyName)
        {
            Borrower newBorrower = new Borrower
            {
                Name = name,
                FamilyName = familyName,
            };
            Borrower borrower = _repositoryBorrower.AddNewBorrower(newBorrower);

            return borrower.BorrowerId;
        }

        public void GetAllBorrowersOrderedByName()
        {
            List<Borrower> borrowers = _repositoryBorrower.GetAllBorrowers()
                  .OrderBy(b => b.Name)
                  .ToList();

            if (borrowers.Count > 0)
            {
                foreach (var borrower in borrowers)
                {
                    Console.WriteLine($"Colleague: {borrower.Name} {borrower.FamilyName}");
                }
                return;
            }

            Console.WriteLine("List of colleagues not created.");
            return;
        }

    }
}
