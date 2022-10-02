using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u19032618_HW05.Models
{ 
    public class Books
    {
        // taken from db
        public int bookId { get; set; }
        public string name { get; set; }
        public int authorId { get; set; }
        public int typeid { get; set; }
        public int pagecount { get; set; }
        public int point { get; set; }

        public Books()
        {

        }

    }
}