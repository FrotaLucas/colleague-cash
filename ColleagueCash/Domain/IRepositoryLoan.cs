using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColleagueCash.Domain
{
    public interface IRepositoryLoan
    {
        public void AddnewRegistration(string name, decimal amount);

        public List<Loan> ListLoans();

    }
}
