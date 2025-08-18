using ColleagueCash.Domain;

namespace ColleagueCash.Application
{
    public class BorrowerService
    {

        public readonly IRepositoryBorrower _repositoryBorrower;

        public BorrowerService(IRepositoryBorrower repositoryBorrower)
        {
            _repositoryBorrower = repositoryBorrower;
        }


        public void AddNewBorrower(Borrower borrower)
        {
            _repositoryBorrower.AddNewBorrower(borrower);
        }

        public List<Borrower> GetAllBorrowersByFamilyName() {

            var borrowers = _repositoryBorrower.GetAllBorrowers()
                .OrderBy( borrower => borrower.FamilyName )
                .ToList();  

            return borrowers;
        }


        public List<Borrower> GetAllBorrowersByName()
        {
            var borrowers = _repositoryBorrower.GetAllBorrowers()
                .OrderBy( borrower => borrower.Name ).ToList();
            return borrowers;
        }

    }
}
