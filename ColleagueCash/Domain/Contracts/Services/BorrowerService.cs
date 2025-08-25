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

        public void GetAllBorrowersWithLoans()
        {
            var borrowers = _repositoryBorrower.GetAllBorrowers();

            foreach (Borrower b in borrowers)
            {
                Console.WriteLine($"Name: {b.Name} {b.FamilyName}\n");
                b.Loans = _loanRepository.GetAllLoansByBorrowerId(b.BorrowerId);

                b.Loans.Where(loan => loan.Amount > 0)
                    .ToList()
                    .ForEach(loan => Console.WriteLine(
                    $"Outstanding amount: {loan.Amount} - " +
                    $"Description: {loan.Description} - " +
                    $"Date of registration: {loan.LoanDate}"
                ));
            }
        }
    }
}
