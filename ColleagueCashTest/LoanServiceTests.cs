using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColleagueCash.Domain.Contracts.Interfaces.IRepository;
using ColleagueCash.Domain.Contracts.Interfaces.IService;
using ColleagueCash.Domain.Contracts.Services;
using ColleagueCash.Domain.Entities;
using Moq;

namespace ColleagueCashTest
{
    public class LoanServiceTests
    {
        private readonly Mock<ILoanRepository> _loanRepositoryMock;
        private readonly Mock<IBorrowerRepository> _borrowerRepositoryMock;
        private readonly Mock<IBorrowerService> _borrowerServiceMock;
        private readonly LoanService _loanService;

        public LoanServiceTests()
        {
            _loanRepositoryMock = new Mock<ILoanRepository>();
            _borrowerRepositoryMock = new Mock<IBorrowerRepository>();
            _borrowerServiceMock = new Mock<IBorrowerService>();

            _loanService = new LoanService(
                _loanRepositoryMock.Object,
                _borrowerRepositoryMock.Object,
                _borrowerServiceMock.Object
            );
        }

        [Fact]
        public void RegisterNewLoan_WhenBorrowerExists()
        {
            var borrower = new Borrower { BorrowerId = 1, Name = "Joao", FamilyName = "Silva" };

            //mocking existed borrower
            _borrowerRepositoryMock
                .Setup( r => r.GetBorrowerByFullname("Joao", "Silva") )
                .Returns( borrower );   

            //creating new loan id registration
            _loanRepositoryMock.Setup( r => r.GetNextId())
                .Returns( 10 );

            //creating new registration
            _loanService.RegisterNewLoan(100, "new test", "Joao", "Silva", 123456);

            //check if AddNewLoan was called once 
            _loanRepositoryMock.Verify(r => 
                r.AddNewLoan( It.Is<String>(s => 
                    s.Contains("10") && 
                    s.Contains("new test") &&
                    s.Contains("100") && 
                    s.Contains(borrower.BorrowerId.ToString()) 
                    )),
                Times.Once);




        }


    }
}
