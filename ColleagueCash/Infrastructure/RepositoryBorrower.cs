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

            if (File.Exists(lastBorrowerId))
            {
                id = int.Parse(File.ReadAllText(lastBorrowerId));
            }

            int newId = id + 1;
            File.WriteAllText(lastBorrowerId, newId.ToString());
            return newId;
        }

        public Borrower AddNewBorrower(Borrower borrower)
        {
            int borrowerId = GetNextId();
            var newBorrower = new Borrower() { 
                BorrowerId = borrowerId,
                Name = borrower.Name,
                FamilyName = borrower.FamilyName,
                Cellphone = borrower.Cellphone,
            };
            
            string newRegistration = $"{borrowerId};{borrower.Name};{borrower.FamilyName};{borrower.Cellphone}";

            File.AppendAllText(borrowerFile, newRegistration + Environment.NewLine);

            return newBorrower;
        }

        public List<Borrower> GetAllBorrowers()
        {
            var borrowers = File.ReadAllLines(borrowerFile)
                .Skip(1)
                .Select(line => line.Split(";"))
                .Select(line => new Borrower
                {
                    Name = line[1],
                    FamilyName = line[2],
                    Cellphone = String.IsNullOrEmpty(line[3]) ? 0 : int.Parse(line[3]),
                })
                .ToList();

            return borrowers;
        }

        public Borrower BorrowerHasActiveLoan(string name, string familyName)
        {
            var borrower = File.ReadAllLines(borrowerFile)
                .Skip(1)
                .Select(line => line.Split(";"))
                .Where(line => line[1].Contains(name) && line[2].Contains(familyName))
                .Select(line => new Borrower
                {
                    BorrowerId = int.Parse(line[0]),
                    Name = line[1],
                    FamilyName = line[2],
                    Cellphone = String.IsNullOrEmpty(line[3]) ? 0 : int.Parse(line[3]),

                })
                .FirstOrDefault();

            return borrower;

        }
    }
}
