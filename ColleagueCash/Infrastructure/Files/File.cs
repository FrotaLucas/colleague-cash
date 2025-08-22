using ColleagueCash.Domain.Contracts.Interfaces.IFiles;
using ColleagueCash.Domain.Entities;

namespace ColleagueCash.Infrastructure.Files
{
    public class File : IFile
    {
        public void AppendAllText(string path, string content)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string path)
        {
            throw new NotImplementedException();
        }

        public List<Borrower> ReadAllLinesBorrowers(string path)
        {
            throw new NotImplementedException();
        }

        public List<Loan> ReadAllLinesLoan(string path)
        {
            throw new NotImplementedException();
        }

        public string ReadAllText(string path)
        {
            throw new NotImplementedException();
        }

        public void WriteAllText(string path, string content)
        {
            throw new NotImplementedException();
        }
    }
}
