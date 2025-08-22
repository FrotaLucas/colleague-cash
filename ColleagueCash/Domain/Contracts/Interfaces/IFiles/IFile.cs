using ColleagueCash.Domain.Entities;

namespace ColleagueCash.Domain.Contracts.Interfaces.IFiles
{
    public interface IFile
    {
        bool Exists(string path);
        string ReadAllText(string path);
        void WriteAllText(string path, string content);
        void AppendAllText(string path, string content);
        List<Borrower> ReadAllLinesBorrowers(string path);

        List<Loan> ReadAllLinesLoan(string path);
    }
}
