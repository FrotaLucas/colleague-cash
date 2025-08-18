using ColleagueCash.Domain;

namespace ColleagueCash.Infrastructure
{
    public class RepositoryLoan : IRepositoryLoan
    {
        private readonly string fileName;

        public RepositoryBorrower _repositoryBorrower;

        public RepositoryLoan(string fileName, RepositoryBorrower repositoryBorrower)
        {
            this.fileName = fileName;
            _repositoryBorrower = repositoryBorrower;
        }

        public void AddNewRegistration(Loan loan, Borrower borrower)
        {
            var storedBorrowed = _repositoryBorrower.BorrowerHasActiveLoan(borrower.Name, borrower.FamilyName);
            var date = DateTime.Now;

            if (storedBorrowed != null)
            {
                string newRegistration = $"{loan.Description};{loan.Amount};{date:yyyy-MM-dd};{borrower.BorrowerId}";
                File.AppendAllText(fileName, newRegistration + Environment.NewLine);

            }



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
