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
            int borrowerId  = GetNextId();  
            string newRegistration = $"{borrowerId};{borrower.Name};{borrower.FamilyName};{borrower.Cellphone}";

            File.AppendAllText(borrowerFile, newRegistration + Environment.NewLine);
        }

        public List<Borrower> GetAllBorrowers()
        {
            var borrowers = File.ReadAllLines(borrowerFile)
                .Skip(1)
                .Select( line => line.Split(";"))
                .Select( line => new Borrower
                {
                    Name = line[1],
                    FamilyName = line[2],
                    Cellphone = String.IsNullOrEmpty(line[3]) ? 0 : int.Parse(line[3]),
                } )
                .ToList();

            return borrowers;
        }
    }
}
