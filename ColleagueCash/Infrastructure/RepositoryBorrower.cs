using ColleagueCash.Domain;

namespace ColleagueCash.Infrastructure
{
    public class RepositoryBorrower : IBorrowerRepository
    {
        private readonly string fileName;

        public RepositoryBorrower(string fileName)
        {
            this.fileName = fileName;
        }


        public void AddNewBorrower(Borrower borrower)
        {
            
        }

        public List<Borrower> GetAllBorrowers()
        {
            throw new NotImplementedException();
        }
    }
}
