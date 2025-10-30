using Moq;
using Microsoft.Extensions.Options;
using ColleagueCash.Domain.Entities;
using ColleagueCash.Domain.Contracts.Interfaces.IRepository;
using ColleagueCash.Domain.Contracts.Interfaces.IService;
using ColleagueCash.Domain.Contracts.Services;
using ColleagueCash.Application.Configuration;

namespace CollegueCashTests;

[TestFixture]
public class LoanServiceTests : IDisposable
{
    private string _tempDir = null!;
    private string _loanCsv = null!;
    private string _borrowerCsv = null!;
    private string _lastBorrowerId = null!;
    private string _lastLoanId = null!;

    private Mock<ILoanRepository> _loanRepo = null!;
    private Mock<IBorrowerRepository> _borrowerRepo = null!;
    private Mock<IBorrowerService> _borrowerService = null!;

    private LoanService _sut = null!;

    [SetUp]
    public void Setup()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), "ccash_tests_" + Guid.NewGuid());
        Directory.CreateDirectory(_tempDir);

        _loanCsv = Path.Combine(_tempDir, "loans.csv");
        _borrowerCsv = Path.Combine(_tempDir, "borrowers.csv");
        _lastBorrowerId = Path.Combine(_tempDir, "last-borrowerId.txt");
        _lastLoanId = Path.Combine(_tempDir, "last-loanId.txt");
        
        AppConfig appConfig = new AppConfig
        {
            DataFilesCSV = new AppConfig.DataFilesCSVConfig
            {
                LoanPath = _loanCsv,
                BorrowerPath = _borrowerCsv,
            
            }
        };

        _loanRepo = new Mock<ILoanRepository>(MockBehavior.Strict);
        _borrowerRepo = new Mock<IBorrowerRepository>(MockBehavior.Strict);
        _borrowerService = new Mock<IBorrowerService>(MockBehavior.Strict);

        //_sut = new LoanService(
        //    Options.Create(appConfig),
        //    _loanRepo.Object,
        //    _borrowerRepo.Object,
        //    _borrowerService.Object
        //);
    }

    [TearDown]
    public void TearDown()
    {
        Dispose();
    }

    public void Dispose()
    {
        try { if (Directory.Exists(_tempDir)) Directory.Delete(_tempDir, true); } catch { }
    }

    [Test]
    public void RegisterNewLoan_BorrowerExists_UsesExistingId_AndDoesNotCreateBorrower()
    {
        // Arrange
        _borrowerRepo
            .Setup(r => r.GetBorrowerByFullname("Ana", "Silva"))
            .Returns(new Borrower { BorrowerId = 42, Name = "Ana", FamilyName = "Silva" });

        _loanRepo.Setup(r => r.GetNextId()).Returns(7);

        string? captured = null;
        _loanRepo
            .Setup(r => r.AddNewLoan(It.IsAny<string>()))
            .Callback<string>(s => captured = s);

        // Act
        _sut.RegisterNewLoan(1500.00m, "Notebook", "Ana", "Silva", 21992345);

        // Assert
        _borrowerService.Verify(s => s.AddNewBorrower(It.IsAny<string>(), It.IsAny<string>(), 21992345), Times.Never);
        _loanRepo.Verify(r => r.GetNextId(), Times.Once);
        _loanRepo.Verify(r => r.AddNewLoan(It.IsAny<string>()), Times.Once);

        Assert.That(captured, Is.Not.Null);
        Assert.That(captured!, Does.StartWith("7;Notebook;"));
        Assert.That(captured!.TrimEnd('\r', '\n'), Does.EndWith(";42"));
    }

    [Test]
    public void RegisterNewLoan_BorrowerMissing_CreatesBorrower_AndUsesReturnedId()
    {
        // Arrange
        _borrowerRepo.Setup(r => r.GetBorrowerByFullname("Joao", "Souza")).Returns((Borrower?)null);
        _borrowerService.Setup(s => s.AddNewBorrower("Joao", "Souza", 21992345)).Returns(99);
        _loanRepo.Setup(r => r.GetNextId()).Returns(1);

        string? captured = null;
        _loanRepo
            .Setup(r => r.AddNewLoan(It.IsAny<string>()))
            .Callback<string>(s => captured = s);

        // Act
        _sut.RegisterNewLoan(10.5m, "Cafe", "Joao", "Souza", 21992345);

        // Assert
        _borrowerService.Verify(s => s.AddNewBorrower("Joao", "Souza", 21992345), Times.Once);
        Assert.That(captured, Is.Not.Null);
        Assert.That(captured!.TrimEnd('\r', '\n'), Does.EndWith(";99"));
    }

    [Test]
    public void RegisterNewLoan_CreatesHeader_WhenLoanCsvDoesNotExist()
    {
        // Arrange
        Assert.That(File.Exists(_loanCsv), Is.False);
        _borrowerRepo.Setup(r => r.GetBorrowerByFullname(It.IsAny<string>(), It.IsAny<string>()))
                     .Returns(new Borrower { BorrowerId = 1, Name = "X", FamilyName = "Y" });
        _loanRepo.Setup(r => r.GetNextId()).Returns(1);
        _loanRepo.Setup(r => r.AddNewLoan(It.IsAny<string>()));

        // Act
        _sut.RegisterNewLoan(1m, "x", "X", "Y", 21992345);

        // Assert
        Assert.That(File.Exists(_loanCsv), Is.True);
        var firstLine = File.ReadLines(_loanCsv).First();
        Assert.That(firstLine, Is.EqualTo("id;description;amount;date;idBorrower"));
    }
}