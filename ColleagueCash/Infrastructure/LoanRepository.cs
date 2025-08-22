using ColleagueCash.Domain.Contracts.Interfaces.IRepository;
using ColleagueCash.Domain.Entities;
using CollegueCashV2.Application.Configuration;
using Microsoft.Extensions.Options;

namespace ColleagueCash.Infrastructure
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AppConfig _appConfig;

        public IBorrowerRepository _repositoryBorrower;

        public LoanRepository(IBorrowerRepository repositoryBorrower, IOptions<AppConfig> appConfig)
        {
            _repositoryBorrower = repositoryBorrower;
            _appConfig = appConfig.Value;
        }

        public void AddNewLoan(string newRegistration)
        {
            try
            {
                File.AppendAllText(_appConfig.DataFilesCSV.LoanPath, newRegistration + Environment.NewLine);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Loan> GetAllLoans()
        {
            if (!File.Exists(_appConfig.DataFilesCSV.LoanPath) || !File.ReadAllLines(_appConfig.DataFilesCSV.LoanPath).Skip(1).Any())
            {
                Console.WriteLine("There are no loans registered;");
                return new List<Loan>();
            }

            var loans = File.ReadAllLines(_appConfig.DataFilesCSV.LoanPath)
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
            var borrower = _repositoryBorrower.GetBorrowerByEmail(name, familyName);

            var loans = new List<Loan>();

            if (borrower != null)
            {
                loans = File.ReadAllLines(_appConfig.DataFilesCSV.LoanPath)
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

            if (loans.Count == 0)
                Console.WriteLine("Colleague not registered yet.");

            return loans;
        }

        public int GetNextId()
        {
            int id = 0;

            if (File.Exists(_appConfig.DataFilesCSV.LastLoanIdFile))
                id = int.Parse(File.ReadAllText(_appConfig.DataFilesCSV.LastLoanIdFile));

            int newId = id + 1;
            File.WriteAllText(_appConfig.DataFilesCSV.LastLoanIdFile, newId.ToString());
            return newId;
        }

        public void ReduceLoan(string name, string familyName, decimal amount)
        {
            var borrower = _repositoryBorrower.GetBorrowerByEmail(name, familyName);

            if (borrower != null && borrower.BorrowerId != null)
            {
                var allLines = File.ReadAllLines(_appConfig.DataFilesCSV.LoanPath).ToList();

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

                File.WriteAllLines(_appConfig.DataFilesCSV.LoanPath, updatedFile);
            }
            Console.WriteLine("Colleage not registered yet.");
        }
    }
}
