using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataSetBookTableAdapters;
using Model.DataSetBorrowTableAdapters;

namespace Model
{
    public class BookDAO
    {
        private readonly TabBookTableAdapter _viewBookTableAdapter;
        private DataSetBorrow.TabBorrowDataTable _borrowTable;
        private DataSetBorrowTableAdapters.TabBorrowTableAdapter _borrowTableAdapter;
        public BookDAO()
        {
            _viewBookTableAdapter = new TabBookTableAdapter();
            _borrowTable = new DataSetBorrow.TabBorrowDataTable();
            _borrowTableAdapter = new TabBorrowTableAdapter();
        }

        // Search books by two criteria: BookName and Author
        public List<Book> SearchBooksByBookNameAndAuthor(string bookNamePattern, int? author, int? publishYear)
        {
            try
            {
                DataTable table;

                // Check if book name pattern is provided
                if (!string.IsNullOrEmpty(bookNamePattern))
                {
                    // Prepare the pattern for a LIKE search
                    bookNamePattern = $"%{bookNamePattern}%";
                }

                // Call the appropriate method from the TableAdapter based on provided criteria
                if (!string.IsNullOrEmpty(bookNamePattern) && author.HasValue && publishYear.HasValue)
                {
                    // Search by book name pattern, author , and publish year
                    table = _viewBookTableAdapter.SearchByBookNameAuthorAndYear(bookNamePattern, author.Value, publishYear.Value);
                }
                else if (!string.IsNullOrEmpty(bookNamePattern) && author.HasValue)
                {
                    // Search by book name pattern and author
                    table = _viewBookTableAdapter.SearchByBookNameAndAuthor(bookNamePattern, author.Value);
                }
                else if (author.HasValue && publishYear.HasValue)
                {
                    // Search by author and publish year
                    table = _viewBookTableAdapter.SearchByAuthorAndYear(author.Value, publishYear.Value);
                }
                else if (!string.IsNullOrEmpty(bookNamePattern))
                {
                    // Search only by book name pattern
                    table = _viewBookTableAdapter.SearchByBookName(bookNamePattern);
                }
                else if (author.HasValue)
                {
                    // Search only by author ID
                    table = _viewBookTableAdapter.SearchByAuthor(author.Value);
                }
                else if (publishYear.HasValue)
                {
                    // Search only by publish year
                    table = _viewBookTableAdapter.SearchByPublishYear(publishYear.Value);
                }
                else
                {
                    // No search criteria provided, get all books
                    table = _viewBookTableAdapter.GetData();
                }

                return ConvertTableToList(table);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw;
            }
        }


        // Browsing all books
        public List<Book> GetAllBooks()
        {
            try
            {
                var dataTable = _viewBookTableAdapter.GetData();
                return ConvertTableToList(dataTable);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw;
            }
        }

        // Convert DataTable to List of Book
        private List<Book> ConvertTableToList(DataTable table)
        {
            List<Book> books = new List<Book>();

            foreach (DataRow row in table.Rows)
            {
                Book book = new Book
                {
                    // Adjust these property assignments to match the actual schema and data types of your Book class
                    ISBN = row["ISBN"].ToString(),
                    BookName = row["BookName"].ToString(),
                    Author = Convert.ToInt32(row["Author"]),
                    Category = row["Category"].ToString(),
                    Language = row["Language"].ToString(),
                    PublishYear = int.Parse(row["PublishYear"].ToString()),
                    Pages = int.Parse(row["Pages"].ToString()),
                    Publisher = row["Publisher"].ToString(),
                };
                books.Add(book);
            }
            return books;
        }
        public bool BorrowBook(string isbn, string studentName, int studentId)
        {
            // Create a new DataRow instance for the TabBorrow table
            DataSetBorrow.TabBorrowRow newBorrowRow = _borrowTable.NewTabBorrowRow();

            newBorrowRow.UID = studentId;
            newBorrowRow.ISBN = isbn;
            newBorrowRow.BorrowDate = DateTime.Now;
            newBorrowRow.ReturnDate = DateTime.Now.AddDays(14); // for example, 2 weeks
            newBorrowRow.ActualReturnDate = DateTime.Now.AddDays(25); // This sets the ActualReturnDate to a DBNull value in the database
            newBorrowRow.LateFee = 0;
            // Add the new row to the TabBorrow table
            _borrowTable.AddTabBorrowRow(newBorrowRow);

            // Update the database using the TableAdapter
            int rowsAffected = _borrowTableAdapter.Update(_borrowTable);

                return rowsAffected > 0;
        }
        public bool ReturnBook(string isbn, int studentId)
        {
            var borrowRecords = _borrowTableAdapter.GetBorrowingByISBNAndUID(isbn, studentId);
            if (borrowRecords.Count == 0)
            {
                // No borrowing record found for this ISBN and student ID
                return false;
            }

            // Get the first (and should be only) borrowing record
            var borrowRow = borrowRecords[0];

            // Set the actual return date
            borrowRow.ActualReturnDate = DateTime.Now;

            // Calculate late fees (assuming $1 per day late)
            if (DateTime.Now > borrowRow.ReturnDate)
            {
                TimeSpan lateDuration = DateTime.Now - borrowRow.ReturnDate;
                borrowRow.LateFee = (int)Math.Ceiling(lateDuration.TotalDays); // For example, $1 per day late
            }
            else
            {
                borrowRow.LateFee = 0; // No late fee if not late
            }

            // Update the database using the TableAdapter
            int rowsAffected = _borrowTableAdapter.Update(borrowRecords);

            return rowsAffected > 0;
        }
        public bool BorrowBook(string isbn, int userId)
        {
            
            // Create a new borrow record
            var newBorrow = _borrowTable.NewTabBorrowRow();
            newBorrow.UID = userId;
            newBorrow.ISBN = isbn;
            newBorrow.BorrowDate = DateTime.Now;
            newBorrow.ReturnDate = DateTime.Now.AddDays(14); // Assume a two-week borrowing period
            newBorrow.ActualReturnDate = new DateTime(2000, 1, 1); // Default value indicating not yet returned

            _borrowTable.AddTabBorrowRow(newBorrow);
            return _borrowTableAdapter.Update(_borrowTable) == 1;
        }
        public bool IsBookBorrowed(string bookID)
        {
            using (var borrowingsTableAdapter = new DataSetBorrowingsTableAdapters.BorrowingsTableAdapter())
            {
                var result = borrowingsTableAdapter.QueryBorrowedStatus(bookID);
                return result != null && result.Count > 0;
            }
        }
        public bool ReserveBook(string bookID, string studentName, string studentId)
        {
            using (var borrowingsTableAdapter = new DataSetBorrowingsTableAdapters.BorrowingsTableAdapter())
            {
                try
                {
                    // Assuming you have added an InsertQuery method in BorrowingsTableAdapter with a 'Status' parameter
                    borrowingsTableAdapter.InsertQuery(
                        bookID,
                        studentId,
                        studentName,
                        DateTime.Now,  // Reservation date
                        null,          // Borrow date can be null for reservations
                        null,          // Return date can be null for reservations
                        "Reserved"     // Status is 'Reserved'
                    );
                    return true;
                }
                catch (Exception ex)
                {
                    // Log or handle the exception
                    return false;
                }
            }
        }
        public bool IsBookReserved(string bookID)
        {
            using (var borrowingsTableAdapter = new DataSetBorrowingsTableAdapters.BorrowingsTableAdapter())
            {
                // This query checks if there's an entry in the Borrowings table with a Reserved status for the book
                var borrowing = borrowingsTableAdapter.GetDataByBookIDAndStatus(bookID, "Reserved");
                return borrowing != null && borrowing.Count > 0;
            }
        }
    }  
}

