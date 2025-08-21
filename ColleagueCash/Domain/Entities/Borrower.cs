namespace ColleagueCash.Domain.Entities
{
    public class Borrower
    {
        public int BorrowerId { get; set; } 

        public string Name { get; set; }

        public string FamilyName { get; set; }

        public int? Cellphone { get; set; }
    }

}
