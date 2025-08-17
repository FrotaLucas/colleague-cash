using ColleagueCash.Domain;

namespace ColleagueCash.Infrastructure
{
    public class RepositoryBorrower : IRepositoryBorrower
    {

        private readonly string borrowerFile;

        public RepositoryBorrower(string fileName)
        {
            this.borrowerFile = fileName;
        }

        public void AddNewBorrower(Borrower borrower)
        {
            string newRegistration = $"{borrower.Name};{borrower.FamilyName};";

            File.AppendAllText(borrowerFile, newRegistration + Environment.NewLine);
        }

        public List<Borrower> GetAllBorrowers()
        {
            throw new NotImplementedException();
        }
    }
}
