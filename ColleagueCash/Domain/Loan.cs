namespace ColleagueCash.Domain
{
    public class Loan
    {
        public int? LoanId { get; set; }

        public string? Description { get; set; }

        public decimal Amount { get; set; }
        
        public DateTime LoanDate { get; set; } = DateTime.Now;  

        public int BorrowerId { get; set; }

    }
}
