using ColleagueCash.Application.Configuration;
using ColleagueCash.Domain.Contracts.Interfaces.IRepository;
using ColleagueCash.Domain.Contracts.Interfaces.IService;
using ColleagueCash.Domain.Entities;

namespace ColleagueCash.Domain.Contracts.Services
{
    public class BorrowerService : IBorrowerService
    {
        public readonly IBorrowerRepository _repositoryBorrower;

        public readonly ILoanRepository _loanRepository;

        public BorrowerService(IBorrowerRepository repositoryBorrower, ILoanRepository loanRepository)
        {
            _repositoryBorrower = repositoryBorrower;

            _loanRepository = loanRepository;
        }

        public int AddNewBorrower(string name, string familyName, int? cellphone)
        {
            Borrower newBorrower = new Borrower
            {
                Name = name,
                FamilyName = familyName,
                Cellphone = cellphone
            };
            Borrower borrower = _repositoryBorrower.AddNewBorrower(newBorrower);

            return borrower.BorrowerId;
        }

        public void GetAllBorrowersOrderedByName()
        {
            List<Borrower> borrowers = _repositoryBorrower.GetAllBorrowers()
                  .OrderBy(b => b.Name)
                  .ToList();

            if (borrowers.Count == 0)
            {
                Console.WriteLine("List of colleagues not created.");
                return;
            }


            foreach (var borrower in borrowers)
            {
                Console.WriteLine($"Colleague: {borrower.Name} {borrower.FamilyName}");
            }

        }

        public List<Borrower> GetAllBorrowersWithLoans()
        {
            var borrowers = new List<Borrower>();
            borrowers = _repositoryBorrower.GetAllBorrowers();

            foreach (Borrower b in borrowers)
            {
                b.Loans = _loanRepository.GetAllLoansByBorrowerId(b.BorrowerId);
            }

            return borrowers;
        }
    }
}
