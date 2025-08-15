using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColleagueCash.Domain;

namespace ColleagueCash.Application
{
    public class ListLoanHandler
    {
        private IRepositoryLoan _repositoryLoan;

        public ListLoanHandler(IRepositoryLoan repositoryLoan)
        {
            _repositoryLoan = repositoryLoan;
        }


        public void Execute()
        {
            var list = _repositoryLoan.ListLoans()
                .OrderBy( loan => loan.Name)
                .ToList();  

            Console.WriteLine("nome com a:" + list[0]);
        }
    }
}
