using ColleagueCash.Domain.Contracts.Interfaces.IRepository;
using ColleagueCash.Domain.Entities;
using ColleagueCash.Application.Configuration;
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

            if (!File.Exists(_appConfig.DataFilesCSV.LoanPath))
                File.WriteAllText(_appConfig.DataFilesCSV.LoanPath, "id;description;amount;date;idBorrower" + Environment.NewLine);

            File.AppendAllText(_appConfig.DataFilesCSV.LoanPath, newRegistration + Environment.NewLine);
        }

        public List<Loan> GetAllLoans()
        {
            if (!File.Exists(_appConfig.DataFilesCSV.LoanPath) || !File.ReadAllLines(_appConfig.DataFilesCSV.LoanPath).Skip(1).Any())
            {
                Console.WriteLine("There are no loans registered;");
                return new List<Loan>();
            }

            List<Loan> loans = File.ReadAllLines(_appConfig.DataFilesCSV.LoanPath)
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
            Borrower borrower = _repositoryBorrower.GetBorrowerByFullname(name, familyName);

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

            return loans;
        }

        public void ReduceLoan(List<Loan> loans)
        {
            var allLines = File.ReadAllLines(_appConfig.DataFilesCSV.LoanPath);

            List<string> updatedFile = new List<string> { "id;description;amount;date;idBorrower" };

            foreach (var line in allLines.Skip(1))
            {
                var parts = line.Split(";");


                var updatedLoan = loans.SingleOrDefault(l => l.LoanId == int.Parse(parts[0]));

                if (updatedLoan != null)
                    parts[2] = updatedLoan.Amount.ToString();


                updatedFile.Add(string.Join(";", parts));
            }

            File.WriteAllLines(_appConfig.DataFilesCSV.LoanPath, updatedFile);
        }


        public List<Loan> GetAllLoansByBorrowerId(int id)
        {

            if (!File.Exists(_appConfig.DataFilesCSV.LoanPath) || !File.ReadAllLines(_appConfig.DataFilesCSV.LoanPath).Skip(1).Any())
            {
                Console.WriteLine("There are no loans registered;");
                return new List<Loan>();
            }

            var allLines = File.ReadAllLines(_appConfig.DataFilesCSV.LoanPath).ToList();

            List<Loan> listOfLoan = allLines
                .Skip(1)
                .Select(line => line.Split(";"))
                .Where(line => int.Parse(line[4]) == id)
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

            return listOfLoan;
        }

        public int GetNextId()
        {
            int id = 0;

            if (File.Exists(_appConfig.DataFilesCSV.LoanPath))
            {
                string lastLine = File.ReadAllLines(_appConfig.DataFilesCSV.LoanPath).Last();

                id = int.Parse(lastLine.Split(';').First());
            }

            int newId = id + 1;
            return newId;
        }
    }
}
