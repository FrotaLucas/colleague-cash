using ColleagueCash.Domain.Contracts.Interfaces.IRepository;
using ColleagueCash.Domain.Contracts.Interfaces.IService;
using CollegueCashV2.Application.Configuration;
using Microsoft.Extensions.Options;

namespace ColleagueCash.Domain.Contracts.Services
{
    public class BorrowerService : IBorrowerService
    {

        private readonly AppConfig _appConfig;

        public readonly IRepositoryBorrower _repositoryBorrower;

        public BorrowerService(IRepositoryBorrower repositoryBorrower, IOptions<AppConfig> appConfig)
        {
            _repositoryBorrower = repositoryBorrower;
            _appConfig = appConfig.Value;
        }


        //public void AddNewBorrower(Borrower borrower)
        //{
        //    _repositoryBorrower.AddNewBorrower(borrower);
        //}

        //public List<Borrower> GetAllBorrowersByFamilyName() {

        //    var borrowers = _repositoryBorrower.GetAllBorrowers()
        //        .OrderBy( borrower => borrower.FamilyName )
        //        .ToList();  

        //    return borrowers;
        //}


        public void GetAllBorrowersOrderedByName()
        {
            var borrowers = _repositoryBorrower.GetAllBorrowers()
                .OrderBy(borrower => borrower.Name);


            foreach (var borrower in borrowers)
            {
                Console.WriteLine($"Colleague: {borrower.Name} {borrower.FamilyName}");
            }
        }

    }
}
