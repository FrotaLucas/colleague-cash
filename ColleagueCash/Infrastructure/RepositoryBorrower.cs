using ColleagueCash.Domain;

namespace ColleagueCash.Infrastructure
{
    public class RepositoryBorrower : IRepositoryBorrower
    {

        private readonly string borrowerFile;

        private readonly string lastBorrowerId;

        public RepositoryBorrower(string fileName, string lastBorrowerId)
        {
            this.borrowerFile = fileName;
            this.lastBorrowerId = lastBorrowerId;
        }


        public int GetNextId()
        {
            int id = 0;

            if (File.Exists(lastBorrowerId)) {
                id = int.Parse(File.ReadAllText(lastBorrowerId));
            }

            int newId = id + 1;
            File.WriteAllText(lastBorrowerId, newId.ToString());
            return newId;
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
