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
            int idRegistration = GetNextId();   

            if (storedBorrower != null)

            {
                newRegistration = $"{idRegistration};{loan.Description};{loan.Amount};{date:yyyy-MM-dd};{storedBorrower.BorrowerId}";
                File.AppendAllText(loanFile, newRegistration + Environment.NewLine);
                return;
            }

            Borrower newBorrower = _repositoryBorrower.AddNewBorrower(borrower);

            newRegistration = $"{idRegistration};{loan.Description};{loan.Amount};{date:yyyy-MM-dd};{newBorrower.BorrowerId}";
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

        public int GetNextId()
        {
            int id = 0;

            if (File.Exists(loanIdFile)) {

                id = int.Parse(File.ReadAllText(loanIdFile));

            }

            int newId = id + 1;
            File.WriteAllText(loanIdFile, newId.ToString());
            return newId;
        }

        public void ReduceLoan(string name, string familyName, decimal amount)
        {
            var borrower = _repositoryBorrower.ExistedBorrower(name, familyName);

            if (borrower != null && borrower.BorrowerId != null) {

                var listOfLoan = File.ReadAllLines(loanFile)
                    .Skip(1)
                    .Select(line => line.Split(";"))
                    .Where(line => int.Parse(line[4]) == borrower.BorrowerId)
                    .OrderBy(line => DateTime.Parse(line[3].ToString() )  )
                    .Select(line => new Loan
                    {
                        LoanId = int.Parse(line[0]),
                        Description = line[1],
                        Amount = int.Parse(line[2]),
                        LoanDate = DateTime.Parse(line[3].ToString() ),
                        BorrowerId = int.Parse(line[4]),
                    })
                    .ToList();
                
                int n = listOfLoan.Count;
                int i = 0;  
                while (listOfLoan.Sum(loan => loan.Amount ) < amount ) {

                    if(listOfLoan[i].Amount < amount)
                    {
                        listOfLoan[i].Amount = 0;
                        amount = amount - listOfLoan[i].Amount;
                        i++;
                        continue;
                    }

                    listOfLoan[i].Amount = listOfLoan[i].Amount - amount;
                    break;
                   
                }


            }
        }

    }
}
