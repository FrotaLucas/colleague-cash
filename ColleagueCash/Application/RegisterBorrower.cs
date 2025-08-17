using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColleagueCash.Domain;
using ColleagueCash.Infrastructure;

namespace ColleagueCash.Application
{
    public class RegisterBorrower
    {

        public readonly IRepositoryBorrower _repositoryBorrower;

        public RegisterBorrower(IRepositoryBorrower repositoryBorrower)
        {
            _repositoryBorrower = repositoryBorrower;
        }


        public void Execute(Borrower borrower)
        {
            _repositoryBorrower.AddNewBorrower(borrower);
        }
    }
}
