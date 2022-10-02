using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u19032618_HW05.ModelsVM
{
    public class BooksOut
    {
        public int bookId { get; set; }

        public BooksOut(int bookId)
        {
            this.bookId = bookId;
        }
    }
}