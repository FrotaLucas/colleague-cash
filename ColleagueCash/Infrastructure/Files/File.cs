using ColleagueCash.Domain.Contracts.Interfaces.IFiles;
using ColleagueCash.Domain.Entities;
using CollegueCashV2.Application.Configuration;
using System.IO;

namespace ColleagueCash.Infrastructure.Files
{
    public class File : IFile
    {

        public static bool Exists(string path) //PQ precisa ser estatico ??
        {
             return File.Exists(path);
        }

        public string[] ReadAllLinesBorrowers(string path)
        {
            var response =  File.ReadAllLines(path);

            return response.ToArray();
        }

        public static List<Loan> ReadAllLinesLoan(string path)
        {
            throw new NotImplementedException();
        }

        public static string ReadAllText(string idPath)
        {
            return File.ReadAllText(idPath);
        }

        public static void WriteAllText(string path, string content)
        {
            File.WriteAllText(path, content + Environment.NewLine);

        }

        public static void AppendAllText(string path, string content)
        {
            File.AppendAllText(path, content + Environment.NewLine);

        }
    }
}
