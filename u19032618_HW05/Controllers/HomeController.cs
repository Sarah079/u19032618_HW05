using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u19032618_HW05.Models;
using u19032618_HW05.ModelsVM;

namespace u19032618_HW05.Controllers
{
    public class HomeController : Controller
    {
        //Service connection
        database database = new database();
        //lists 
        public static List<BooksOut> borrowedbooks = new List<BooksOut>();
        public static List<BookAuthors> BookList = new List<BookAuthors>();
        public static List<Students> students = new List<Students>();
        public static List<Borrows> borrows = new List<Borrows>();
        public static List<BookAuthors> search = null;

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                database.clearLists();
                database.getAllBooks();
                database.getAllStudents();
                database.getAllBorrows();
                database.UpdateBooks();

                ViewBag.Types = database.GetAllTypes();
                ViewBag.Authors = database.GetAllAuthors();
            }
            catch(Exception err)
            {
                ViewBag.Message = err.Message;
            }
            finally
            {
                database.CloseDB();
            }
            return View(); //return book list
        }
        [HttpPost]
        public ActionResult Search(string name, int? typeId, int? authorId)
        {
            try
            {
                if (name != "" && typeId != null && authorId != null)
                {
                    //if search contains all 3
                    database.getAllBooks();
                    search = BookList.Where(x => x.name == name && x.typeId == typeId && x.authorId == authorId).ToList();
                }
                else if (name != "" && typeId != null && authorId == null)
                {
                    // name and type 
                }
                else if (name != "" && typeId == null && authorId != null)
                {
                    //  name and author
                }
                else if (name == "" && typeId != null && authorId != null)
                {
                    //type and author
                }
                else if (name == "" && typeId == null && authorId != null)
                {
                    //just author
                }
                else if (name == "" && typeId != null && authorId == null)
                {
                    // just type
                    search = BookList.Where(x => x.typeId == typeId).ToList();
                }
                else if (name != "" && typeId == null && authorId == null)
                {
                    // just name 
                }
                else
                {
                    TempData["Message"] = "Please enter a value";
                }
            }
            catch (Exception message)
            {
                TempData["Message"] = message;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Bookdetails(int bookId)
        {
            //workout whether book is taken out or not
                BookAuthors book = BookList.Where(x => x.bookId == bookId).FirstOrDefault();
                if (book != null)
                {
                    var borrowedbooks = borrows.Where(x => x.bookId == bookId).ToList();
                    book.totalBorrows = borrowedbooks.Count();
                    List<BorrowedBooks> BorrowRecords = new List<BorrowedBooks>();
                    for (int i = 0; i < borrowedbooks.Count(); i++)
                    {
                        BorrowedBooks record = new BorrowedBooks();
                        record.borrowId = borrowedbooks[i].borrowId;
                        record.studentName = students.Where(x => x.studentId == borrowedbooks[i].studentId).FirstOrDefault().name;
                        record.takenDate = borrowedbooks[i].takenDate;
                        record.broughtDate = borrowedbooks[i].broughtDate;
                        BorrowRecords.Add(record);
                        
                    }

                    book.borrowedHistory = BorrowRecords;
                    ViewBag.Message = book; //return using viewbag message
                    ViewBag.Status = book.status;
                }
                else
                {
                    ViewBag.Message = "Book Not Found";
                }
                return View(book);

           
        }
        [HttpGet]
        public ActionResult ViewStudents(int bookId)
        {
            try
            {
                database.clearLists();
                database.getAllBooks();
                database.getAllStudents();
                database.getAllBorrows();
                database.UpdateBooks();

                BookAuthors book = BookList.Where(x => x.bookId == bookId).FirstOrDefault();
                ViewBag.Status = book.status;
            }
            catch (Exception err)
            {
                ViewBag.Message = err.Message;
            }
            finally
            {
                database.CloseDB();
            }
            return View();
        }
        

    }
}