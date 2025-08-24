using ColleagueCash.Domain.Contracts.Interfaces.IRepository;
using ColleagueCash.Domain.Entities;
using ColleagueCash.Application.Configuration;
using Microsoft.Extensions.Options;

namespace ColleagueCash.Infrastructure
{
    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly AppConfig _appConfig;

        public BorrowerRepository(IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig.Value;
        }

        public int GetNextId()
        {
            int id = 0;

            if (File.Exists(_appConfig.DataFilesCSV.BorrowerPath))
            {
                if (File.ReadLines(_appConfig.DataFilesCSV.BorrowerPath).Count() == 1)
                    return id;

                else
                {
                    string lastLine = File.ReadLines(_appConfig.DataFilesCSV.BorrowerPath).Last();
                    id = int.Parse(lastLine.Split(';').First());
                }
            }

            int newId = id + 1;
            return newId;
        }

        public Borrower AddNewBorrower(Borrower borrower)
        {
            int borrowerId = GetNextId();

            if (!File.Exists(_appConfig.DataFilesCSV.BorrowerPath))
                File.WriteAllText(_appConfig.DataFilesCSV.BorrowerPath, "id;name;familyName;cellphone" + Environment.NewLine);

            var newBorrower = new Borrower()
            {
                BorrowerId = borrowerId,
                Name = borrower.Name,
                FamilyName = borrower.FamilyName,
                Cellphone = borrower.Cellphone,
            };

            string newRegistration = $"{borrowerId};{borrower.Name};{borrower.FamilyName};{borrower.Cellphone}";

            File.AppendAllText(_appConfig.DataFilesCSV.BorrowerPath, newRegistration + Environment.NewLine);

            return newBorrower;
        }

        public List<Borrower> GetAllBorrowers()
        {
            if (!File.Exists(_appConfig.DataFilesCSV.BorrowerPath))
            {
                Console.WriteLine("List of colleagues not created.");
                return new List<Borrower>();
            }

            var borrowers = new List<Borrower>();

            borrowers = File.ReadAllLines(_appConfig.DataFilesCSV.BorrowerPath)
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

        public Borrower GetBorrowerByEmail(string name, string familyName)
        {
            if (!File.Exists(_appConfig.DataFilesCSV.BorrowerPath))
                File.WriteAllText(_appConfig.DataFilesCSV.BorrowerPath, "id;name;familyName;cellphone" + Environment.NewLine);

            var lines = File.ReadAllLines(_appConfig.DataFilesCSV.BorrowerPath).Skip(1);


            if (!lines.Any())
                return null;

            var borrower = new Borrower();
            borrower = lines
                .Select(line => line.Split(";"))
                .Where(line => line[1] == name && line[2] == familyName)
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
