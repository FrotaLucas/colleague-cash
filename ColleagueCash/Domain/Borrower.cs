using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColleagueCash.Domain
{
    public class Borrower
    {
        public int? BorrowerId { get; set; } 

        public string Name { get; set; }

        public string FamilyName { get; set; }

        public int? Cellphone { get; set; }
    }

}
