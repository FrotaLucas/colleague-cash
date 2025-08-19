using ColleagueCash.Domain;

namespace ColleagueCash.Infrastructure
{
    public class RepositoryLoan : IRepositoryLoan
    {
        private readonly string loanFile;

        private readonly string loanIdFile;

        public IRepositoryBorrower _repositoryBorrower;

        public RepositoryLoan(string loanFile, string loandIdFile, IRepositoryBorrower repositoryBorrower)
        {
            this.loanFile = loanFile;
            this.loanIdFile = loandIdFile;
            _repositoryBorrower = repositoryBorrower;
        }

        public void AddNewLoan(decimal amount, string description, string name, string familyName)
        {
            var storedBorrower = _repositoryBorrower.ExistedBorrower(name, familyName);
            var date = DateTime.Now;
            string newRegistration;
            int idRegistration = GetNextId();

            if (!File.Exists(loanFile))
                File.WriteAllText(loanFile, "id;description;amount;date;idBorrower" + Environment.NewLine);

            if (storedBorrower != null)
            {
                newRegistration = $"{idRegistration};{description};{amount};{date:yyyy-MM-dd};{storedBorrower.BorrowerId}";
                File.AppendAllText(loanFile, newRegistration + Environment.NewLine);
                return;
            }

            var borrower = new Borrower
            {
                Name = name,
                FamilyName = familyName,
            };

            Borrower newBorrower = _repositoryBorrower.AddNewBorrower(borrower);

            newRegistration = $"{idRegistration};{description};{amount};{date:yyyy-MM-dd};{newBorrower.BorrowerId}";
            File.AppendAllText(loanFile, newRegistration + Environment.NewLine);

        }

        public List<Loan> GetAllLoans()
        {
            if (!File.Exists(loanFile) || !File.ReadAllLines(loanFile).Skip(1).Any())
            {
                Console.WriteLine("There are no loans registered;");
                return new List<Loan>();    
            }

            var loans = File.ReadAllLines(loanFile)
                .Skip(1)
                .Select(line => line.Split(";"))
                .Select(line => new Loan
                {
                    Description = line[1],
                    Amount = Decimal.Parse(line[2]),
                    LoanDate = DateTime.Parse(line[3])
                })
                .ToList();

            return loans;
        }

        public List<Loan> GetAllLoansByBorrower(string name, string familyName)
        {
            var borrower = _repositoryBorrower.ExistedBorrower(name, familyName);

            var loans = new List<Loan>();

            if (borrower != null)
            {
                loans = File.ReadAllLines(loanFile)
                    .Skip(1)
                    .Select(line => line.Split(";"))
                    .Where(parts => int.Parse(parts[4]) == borrower.BorrowerId)
                    .Select(line => new Loan
                    {
                        Description = line[1],
                        Amount = Decimal.Parse(line[2]),
                        LoanDate = DateTime.Parse(line[3])
                    })
                    .ToList();
            }
            
            if(loans.Count == 0)
                Console.WriteLine("Colleague not registered yet.");

            return loans;
        }

        public int GetNextId()
        {
            int id = 0;

            if (File.Exists(loanIdFile))
                id = int.Parse(File.ReadAllText(loanIdFile));

            int newId = id + 1;
            File.WriteAllText(loanIdFile, newId.ToString());
            return newId;
        }

        public void ReduceLoan(string name, string familyName, decimal amount)
        {
            var borrower = _repositoryBorrower.ExistedBorrower(name, familyName);

            if (borrower != null && borrower.BorrowerId != null)
            {

                var allLines = File.ReadAllLines(loanFile).ToList();

                var listOfLoan = allLines
                    .Skip(1)
                    .Select(line => line.Split(";"))
                    .Where(line => int.Parse(line[4]) == borrower.BorrowerId)
                    .OrderBy(line => DateTime.Parse(line[3].ToString()))
                    .Select(line => new Loan
                    {
                        LoanId = int.Parse(line[0]),
                        Description = line[1],
                        Amount = int.Parse(line[2]),
                        LoanDate = DateTime.Parse(line[3].ToString()),
                        BorrowerId = int.Parse(line[4]),
                    })
                    .ToList();

                int i = 0;
                while (amount > 0 && i < listOfLoan.Count)
                {
                    if (amount >= listOfLoan[i].Amount)
                    {
                        amount -= listOfLoan[i].Amount;
                        listOfLoan[i].Amount = 0;
                    }

                    else
                    {
                        listOfLoan[i].Amount -= amount;
                        amount = 0;
                    }

                    i++;
                }

                if (amount > 0)
                    Console.WriteLine($"Warining: Payment amount exceeds the total loan");

                List<string> updatedFile = new List<string> { allLines.First() };
                foreach (var line in allLines.Skip(1))
                {
                    var parts = line.Split(";");


                    var updatedLoan = listOfLoan.SingleOrDefault(l => l.LoanId == int.Parse(parts[0]));

                    if (updatedLoan != null)
                        parts[2] = updatedLoan.Amount.ToString();


                    updatedFile.Add(string.Join(";", parts));

                }


                File.WriteAllLines(loanFile, updatedFile);

            }

            Console.WriteLine("Colleage not registered yet.");
        
        }


    }
}
