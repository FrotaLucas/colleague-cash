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
        public void Add(string name, double amount)
        {
            throw new NotImplementedException();
        }

        public List<Loan> ListLoans()
        {
            throw new NotImplementedException();
        }
    }
}
