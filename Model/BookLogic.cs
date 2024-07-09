using ClassLibraryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BookLogic
    {
        private readonly BookDAO _bookDAO;

        public BookLogic()
        {
            _bookDAO = new BookDAO();
        }

        // Retrieve all books
        public List<BookDTO> GetAllBooks()
        {
            try
            {
                List<Book> books = _bookDAO.GetAllBooks();
                return books.Select(ConvertToBookDTO).ToList();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw;
            }
        }

        // Search books by two criteria: BookName and Author
        public List<BookDTO> SearchBooks(string bookNamePattern, int? author, int? publishYear)
        {
            try
            {
                List<Book> books = _bookDAO.SearchBooksByBookNameAndAuthor(bookNamePattern, author, publishYear);
                return books.Select(ConvertToBookDTO).ToList();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw;
            }
        }

        // Conversion method
        private BookDTO ConvertToBookDTO(Book book)
        {
            return new BookDTO
            {
                ISBN = book.ISBN, // Assuming ISBN is a string as it often contains hyphens and leading zeros
                BookName = book.BookName,
                Author = book.Author,
                Category = book.Category,
                Language = book.Language,
                PublishYear = book.PublishYear,
                Pages = book.Pages,
                Publisher = book.Publisher
            };
        }
    }
}
