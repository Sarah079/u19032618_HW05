using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u19032618_HW05.Models
{
    public class Borrows
    {
        // taken from db
        public int borrowId { get; set; }
        public DateTime takenDate { get; set; }
        public Nullable< DateTime> broughtDate { get; set; } //make nullable incase hasn't come back
        public Nullable< int> bookId { get; set; }
        public int studentId { get; set; }

        public Borrows()
        {

        }

    }
}