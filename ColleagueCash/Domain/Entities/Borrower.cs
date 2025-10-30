namespace ColleagueCash.Domain.Entities
{
    public class Borrower
    {
        public int BorrowerId { get; set; } 

        public string Name { get; set; }

        public string FamilyName { get; set; }

        public string? Cellphone { get; set; }

        public List<Loan> Loans { get; set; } = new List<Loan>();
    }

}
