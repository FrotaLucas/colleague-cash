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

    }
}
