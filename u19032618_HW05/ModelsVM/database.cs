using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using u19032618_HW05.Models;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace u19032618_HW05.ModelsVM
{
    public class database
    {
        //get connectin string
        SqlConnection Dbconnection = new SqlConnection("Data Source=.;Initial Catalog=Library;Integrated Security=True;");

        //lists 
        public static List<BooksOut> borrowedbooks = new List<BooksOut>();
        public static List<BookAuthors> BookList = new List<BookAuthors>();
        public static List<Students> students = new List<Students>();
        public static List<Borrows> borrows = new List<Borrows>();
        

        public void clearLists()
        {
            BookList.Clear();
            borrowedbooks.Clear();
            borrows.Clear();
        }

        public void getAllBooks()
        {
            SqlCommand getAllBooks = new SqlCommand("SELECT book.[bookId] as bookId ,book.[name] as name ,book.[pagecount] as pagecount ,book.[point] as point, auth.[surname] as authorSurname ,type.[name] typeName " +
                                        "FROM [Library].[dbo].[books] book " +
                                        "JOIN [Library].[dbo].[authors] author on book.authorId = author.authorId " +
                                        "JOIN [Library].[dbo].[types] Types on book.typeId = type.typeId",
                                        Dbconnection);
            Dbconnection.Open();
            SqlDataReader readBooks = getAllBooks.ExecuteReader();
            while (readBooks.Read())
            {
                BookAuthors book = new BookAuthors();
                book.bookId = (int)readBooks["bookId"];
                book.name = (string)readBooks["name"];
                book.pagecount = (int)readBooks["pagecount"];
                book.point = (int)readBooks["point"];
                book.authorSurname = (string)readBooks["authorSurname"];
                book.typeName = (string)readBooks["typeName"];
                book.status = true;
                BookList.Add(book);
            }
            Dbconnection.Close();
        }

        public void getAllStudents()
        {
            SqlCommand getAllStudents = new SqlCommand("SELECT * FROM [Library].[dbo].[students]", Dbconnection);
            Dbconnection.Open();
            SqlDataReader readStudents = getAllStudents.ExecuteReader();
            while (readStudents.Read())
            {
                Students student = new Students();
                student.studentId = (int)readStudents["studentId"];
                student.name = (string)readStudents["name"];
                student.surname = (string)readStudents["surname"];
                student.birthdate = (DateTime)readStudents["birthdate"];
                student.gender = (string)readStudents["gender"];
                student.Class = (string)readStudents["class"];
                student.point = (int)readStudents["point"];
                students.Add(student);
            }
            Dbconnection.Close();
        }

        public void getAllBorrows()
        {
            SqlCommand getBorrows = new SqlCommand("SELECT * FROM [Library].[dbo].[borrows]", Dbconnection);
            Dbconnection.Open();
            SqlDataReader readBorrows = getBorrows.ExecuteReader();
            while (readBorrows.Read())
            {
                Borrows borrow = new Borrows();
                borrow.borrowId = (int)readBorrows["borrowId"];
                borrow.studentId = (int)readBorrows["studentId"];
                borrow.bookId = (int)readBorrows["bookId"];
                borrow.takenDate = Convert.ToDateTime(readBorrows["takenDate"]);
                var broughtDate = readBorrows["broughtDate"].ToString();
                if (broughtDate != "")
                {
                    borrow.broughtDate = Convert.ToDateTime(readBorrows["broughtDate"]);
                }
                else
                {
                    borrow.broughtDate = null;
                }

                borrows.Add(borrow);
            }
        }

        public void UpdateBooks()
        {
            for (int i = 0; i < borrows.Count; i++)
            {
                if (borrows[i].broughtDate == null)
                {
                    borrowedbooks.Add(new BooksOut((int)borrows[i].bookId));
                }
            }

            for (int i = 0; i < BookList.Count; i++)
            {

                for (int j = 0; j < borrowedbooks.Count; j++)
                {
                    if (BookList[i].bookId == borrowedbooks[j].bookId)
                    {
                        BookList[i].status = false;
                    }
                }
            }
        }

        public void CloseDB()
        {
            Dbconnection.Close();

        }

        public SelectList GetAllTypes()
        {
            List<Types> typesList = new List<Types>();
            SqlCommand getTypes = new SqlCommand("SELECT * FROM [Library].[dbo].[types]");
            Dbconnection.Open();
            SqlDataReader readTypes = getTypes.ExecuteReader();
                    while (readTypes.Read())
                    {
                        Types type = new Types();
                        type.typeid = (int)readTypes["typeId"];
                        type.name = (string)readTypes["name"];
                        typesList.Add(type);
                    }
            return new SelectList(typesList, "typeId", "name");
        }

        public SelectList GetAllAuthors()
        {
            List<Authors> authorsList = new List<Authors>();
            SqlCommand getAuthors = new SqlCommand ("SELECT * FROM [Library].[dbo].[authors]");
            Dbconnection.Open();
            SqlDataReader readAuthors = getAuthors.ExecuteReader();
            while (readAuthors.Read())
            {
                Authors author = new Authors();
                author.authorID = (int)readAuthors["authorId"];
                author.name = (string)readAuthors["name"];
                authorsList.Add(author);
            }
            return new SelectList(authorsList, "typeId", "name");
        }


        

}

}
