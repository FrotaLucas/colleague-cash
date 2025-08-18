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

        public void AddNewLoan(Loan loan, Borrower borrower)
        {
            var storedBorrower = _repositoryBorrower.ExistedBorrower(borrower.Name, borrower.FamilyName);
            var date = DateTime.Now;
            string newRegistration;

            if (storedBorrower != null)

            {
                newRegistration = $"{loan.Description};{loan.Amount};{date:yyyy-MM-dd};{storedBorrower.BorrowerId}";
                File.AppendAllText(loanFile, newRegistration + Environment.NewLine);
                return;
            }

            Borrower newBorrower = _repositoryBorrower.AddNewBorrower(borrower);

            newRegistration = $"{loan.Description};{loan.Amount};{date:yyyy-MM-dd};{newBorrower.BorrowerId}";
            File.AppendAllText(loanFile, newRegistration + Environment.NewLine);

        }

        public List<Loan> GetAllLoans()
        {
            var loans = File.ReadAllLines(loanFile)
                .Skip(1)
                .Select(line => line.Split(";"))
                .Select(line => new Loan
                {
                    Description = line[0],
                    Amount = Decimal.Parse(line[1])
                })
                .ToList();

            return loans;


        }

        public void GetNextId()
        {
            int id = 0;



            throw new NotImplementedException();
        }

        public void ReduceLoan(string name, string familyName, decimal amount)
        {
            var borrower = _repositoryBorrower.ExistedBorrower(name, familyName);

            if (borrower != null && borrower.BorrowerId != null) {

                //var loan = File.ReadAllLines(fileName)
                //    .Skip(1)
                //    .Select(line => line.Split(";")
                //    .Where( line => line[])
                     

                

            }
        }

    }
}
