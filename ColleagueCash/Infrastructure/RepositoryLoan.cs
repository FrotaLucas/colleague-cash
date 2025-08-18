using ColleagueCash.Domain;

namespace ColleagueCash.Infrastructure
{
    public class RepositoryLoan : IRepositoryLoan
    {
        private readonly string fileName;

        public IRepositoryBorrower _repositoryBorrower;

        public RepositoryLoan(string fileName, IRepositoryBorrower repositoryBorrower)
        {
            this.fileName = fileName;
            _repositoryBorrower = repositoryBorrower;
        }

        public void AddNewLoan(Loan loan, Borrower borrower)
        {
            var storedBorrower = _repositoryBorrower.BorrowerHasActiveLoan(borrower.Name, borrower.FamilyName);
            var date = DateTime.Now;
            string newRegistration;

            if (storedBorrower != null)

            {
                newRegistration = $"{loan.Description};{loan.Amount};{date:yyyy-MM-dd};{storedBorrower.BorrowerId}";
                File.AppendAllText(fileName, newRegistration + Environment.NewLine);

            }

            Borrower newBorrower = _repositoryBorrower.AddNewBorrower(borrower);

            newRegistration = $"{loan.Description};{loan.Amount};{date:yyyy-MM-dd};{newBorrower.BorrowerId}";
            File.AppendAllText(fileName, newRegistration + Environment.NewLine);

        }

        public List<Loan> GetAllLoans()
        {
            var loans = File.ReadAllLines(fileName)
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

        public void ReduceLoan(string name, decimal amount)
        {

        }

    }
}
