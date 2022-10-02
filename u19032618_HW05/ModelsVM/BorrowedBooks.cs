using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u19032618_HW05.ModelsVM
{
    public class BorrowedBooks
    {
        public string studentName { get; set; }

        public Nullable<int> borrowId { get; set; } //nalluable incase student hasn't taken a book out 

        public Nullable<DateTime> takenDate { get; set; }

        public Nullable<DateTime> broughtDate { get; set; }
    }
}