using ColleagueCash.Domain;

namespace ColleagueCash.Infrastructure
{
    public class RepositoryLoan : IRepositoryLoan
    {
        private readonly string fileName;

        public RepositoryLoan(string fileName)
        {
            this.fileName = fileName;
        }

        public void AddnewRegistration(string name, decimal amount)
        {
            //escrever na ultima linha do csv 
            //name e amount
            throw new NotImplementedException();
        }

        public List<Loan> ListLoans()
        {
            var loans = File.ReadAllLines(fileName)
                .Skip(1)
                .Select(line => line.Split(";"))
                .Select(line => new Loan
                {
                    Name = line[0],
                    Amount = Decimal.Parse(line[1])  
                })
                .ToList();  

            return loans;

        }
    }
}
