using ColleagueCash.Domain;

namespace ColleagueCash.Infrastructure
{
    public class RepositoryBorrower : IRepositoryBorrower
    {

        private readonly string borrowerFile;

        private readonly string borrowerIdFile;

        public RepositoryBorrower(string fileName, string lastBorrowerId)
        {
            this.borrowerFile = fileName;
            this.borrowerIdFile = lastBorrowerId;
        }

        public int GetNextId()
        {
            int id = 0;

            if (File.Exists(borrowerIdFile))
            {
                id = int.Parse(File.ReadAllText(borrowerIdFile));
            }

            int newId = id + 1;
            File.WriteAllText(borrowerIdFile, newId.ToString());
            return newId;
        }

        public Borrower AddNewBorrower(Borrower borrower)
        {
            int borrowerId = GetNextId();

            if (!File.Exists(borrowerFile))
            {
                File.WriteAllText(borrowerFile, "id;name;familyName;cellphone" + Environment.NewLine);
            }

            var newBorrower = new Borrower()
            {
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

        public Borrower ExistedBorrower(string name, string familyName)
        {
            if(!File.Exists(borrowerFile))
                File.WriteAllText(borrowerFile, "id;name;familyName;cellphone" + Environment.NewLine);

            var lines = File.ReadAllLines(borrowerFile).Skip(1);


            if (!lines.Any())
                return null;
            
            var borrower = lines
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
