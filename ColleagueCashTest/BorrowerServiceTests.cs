using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColleagueCash.Domain.Contracts.Interfaces.IRepository;
using ColleagueCash.Domain.Contracts.Services;
using ColleagueCash.Domain.Entities;
using Moq;

namespace ColleagueCashTest
{
    public class BorrowerServiceTests
    {

        private readonly Mock<IBorrowerRepository> _borrowerRepositoryMock;
        private readonly Mock<ILoanRepository> _loanRepositoryMock;
        private readonly BorrowerService _borrowerService;

        public BorrowerServiceTests()
        {
            _borrowerRepositoryMock = new Mock<IBorrowerRepository>();
            _loanRepositoryMock = new Mock<ILoanRepository>();

            _borrowerService = new BorrowerService(
                _borrowerRepositoryMock.Object,
                _loanRepositoryMock.Object
            );
        }

        [Fact]
        public void GetAllBorrowersWithLoans_ShouldReturnBorrowersWithTheirLoans()
        {

            var borrowers = new List<Borrower>
            {
                new Borrower { BorrowerId = 1, Name = "joao", FamilyName = "silva" },
                new Borrower { BorrowerId = 2, Name = "maria", FamilyName = "souza" }
            };

            var loansJoao = new List<Loan>
            {
                new Loan { LoanId = 101, Amount = 100, Description = "1. loan" },
                new Loan { LoanId = 102, Amount = 200, Description = "2. loan" }
            };

            var loansMaria = new List<Loan>
            {
                new Loan { LoanId = 201, Amount = 300, Description = "3. loan" }
            };


            _borrowerRepositoryMock
               .Setup(r => r.GetAllBorrowers())
               .Returns(borrowers);

            _loanRepositoryMock
                .Setup(r => r.GetAllLoansByBorrowerId(1))
                .Returns(loansJoao);

            _loanRepositoryMock
                .Setup(r => r.GetAllLoansByBorrowerId(2))
                .Returns(loansMaria);

            var result = _borrowerService.GetAllBorrowersWithLoans();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(2, result[0].Loans.Count);
            Assert.Contains(result[0].Loans, l => l.Description == "1. loan");
            Assert.Contains(result[0].Loans, l => l.Description == "2. loan");

            Assert.Single(result[1].Loans);
            Assert.Equal("3. loan", result[1].Loans[0].Description);

            // check if loan repository borrower repository was called
            _borrowerRepositoryMock.Verify(r => r.GetAllBorrowers(), Times.Once);
            _loanRepositoryMock.Verify(r => r.GetAllLoansByBorrowerId(It.IsAny<int>()), Times.Exactly(2));
        }
    }
}
