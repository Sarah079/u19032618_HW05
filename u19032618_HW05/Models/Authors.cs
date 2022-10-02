using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u19032618_HW05.Models
{
    public class Authors
    {
      //  [authorId]
      //,[name]
      //,[surname]
      //  FROM[Library].[dbo].[authors]
        public int authorID { get; set; }
         public string name { get; set; }
        public string surname  { get; set; }

    }
}