using ColleagueCash.Domain;
using ColleagueCash.Domain.Contracts.Interfaces.IService;

namespace ColleagueCash.Domain.Contracts.Services
{
    public class BorrowerService : IBorrowerService
    {

        public readonly IRepositoryBorrower _repositoryBorrower;

        public BorrowerService(IRepositoryBorrower repositoryBorrower)
        {
            _repositoryBorrower = repositoryBorrower;
        }


        //public void AddNewBorrower(Borrower borrower)
        //{
        //    _repositoryBorrower.AddNewBorrower(borrower);
        //}

        //public List<Borrower> GetAllBorrowersByFamilyName() {

        //    var borrowers = _repositoryBorrower.GetAllBorrowers()
        //        .OrderBy( borrower => borrower.FamilyName )
        //        .ToList();  

        //    return borrowers;
        //}


        public void GetAllBorrowersOrderedByName()
        {
            var borrowers = _repositoryBorrower.GetAllBorrowers()
                .OrderBy(borrower => borrower.Name);


            foreach (var borrower in borrowers)
            {
                Console.WriteLine($"Colleague: {borrower.Name} {borrower.FamilyName}");
            }
        }

    }
}
