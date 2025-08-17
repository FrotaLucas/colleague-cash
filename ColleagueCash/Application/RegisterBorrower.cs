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

        public readonly IRepositoryBorrower RepositoryBorrower;

        public RegisterBorrower(IRepositoryBorrower repositoryBorrower)
        {
            RepositoryBorrower = repositoryBorrower;
        }



    }
}
