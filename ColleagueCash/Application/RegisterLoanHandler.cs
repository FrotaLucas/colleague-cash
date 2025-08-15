using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColleagueCash.Domain;

namespace ColleagueCash.Application
{
    public class RegisterLoanHandler
    {
        private IRepositoryLoan _repositoryLoan;

        public RegisterLoanHandler(IRepositoryLoan repositoryLoan)
        {
            _repositoryLoan = repositoryLoan;
        }


        public void Execute(string name, double amount)
        {

        }
    }
}
