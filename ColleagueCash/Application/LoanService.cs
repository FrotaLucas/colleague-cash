using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColleagueCash.Domain;

namespace ColleagueCash.Application
{
    public class LoanService
    {
        private IRepositoryLoan _repositoryLoan;

        public LoanService(IRepositoryLoan repositoryLoan)
        {
            _repositoryLoan = repositoryLoan;
        }


        public void Execute()
        {
            var list = _repositoryLoan.GetAllLoans()
                .OrderBy( loan => loan.Description)
                .ToList();  

            Console.WriteLine("nome com a:" + list[0].Description);
        }
    }
}
