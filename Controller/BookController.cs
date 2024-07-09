using ClassLibraryDTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class BookController
    {
        private BookLogic _bookLogic;
        public BookController()
        {
            _bookLogic = new BookLogic();
        }
        public List<BookDTO> GetAllBooks()
        {
            BookLogic bookLogic = new BookLogic();
            return bookLogic.GetAllBooks();
        }
        
        public User Login(string username, string password)
        {
            
            var userLogic = new UserLogic();
            return userLogic.ValidateLogin(username, password);
        }

        // Search books by two criteria (For this example: BookName and Author)
        public List<BookDTO> SearchBooks(string bookNamePattern, int? author, int? publishYear)
        {
            BookLogic bookLogic= new BookLogic();
            return _bookLogic.SearchBooks(bookNamePattern, author, publishYear);
        }

        // Browsing books (Here, I'm just fetching all books. Adjust as needed.)
        public List<BookDTO> BrowseBooks()
        {
            return _bookLogic.GetAllBooks();
        }

    }
}
