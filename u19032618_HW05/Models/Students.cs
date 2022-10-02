using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u19032618_HW05.Models
{
    public class Students
    {
        // taken from db
        public int studentId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime birthdate { get; set; }
        public string gender { get; set; }
        public string Class { get; set; } //made capital letter
        public int point { get; set; }


        public Students()
        {

        }

    }
}