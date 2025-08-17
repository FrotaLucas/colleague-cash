namespace ColleagueCash.Domain
{
    public class Loan
    {
        public int LoanId { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }
        
        public DateTime LoanDate { get; set; }

    }
}
