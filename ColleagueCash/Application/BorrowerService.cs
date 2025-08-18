using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColleagueCash.Domain;
using ColleagueCash.Infrastructure;

namespace ColleagueCash.Application
{
    public class BorrowerService
    {

        public readonly IRepositoryBorrower _repositoryBorrower;

        public BorrowerService(IRepositoryBorrower repositoryBorrower)
        {
            _repositoryBorrower = repositoryBorrower;
        }


        public void AddNewBorrower(Borrower borrower)
        {
            _repositoryBorrower.AddNewBorrower(borrower);
        }
    }
}
