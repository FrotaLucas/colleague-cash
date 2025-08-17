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
            string newRegistration = $"{borrower.Name};{borrower.FamilyName};";

            File.AppendAllText(fileName, newRegistration + Environment.NewLine);
        }

        public List<Borrower> GetAllBorrowers()
        {
            throw new NotImplementedException();
        }
    }
}
