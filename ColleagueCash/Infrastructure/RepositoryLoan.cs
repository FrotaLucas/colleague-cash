using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void Add(string name, double amount)
        {
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
                    Amount = Double.Parse(line[1])  
                })
                .ToList();  

            //Console.WriteLine("first line: " + loans[0]);

            throw new NotImplementedException();
        }
    }
}
