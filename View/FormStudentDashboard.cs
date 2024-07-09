using ClassLibraryDTO;
using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    public partial class FormStudentDashboard : Form
    {
        private Form _loginForm2;
        private BookDAO _bookDAO;


        public FormStudentDashboard()
        {
            InitializeComponent();
            this.button2.Click += new EventHandler(this.button2_Click_1);
            this.button3.Click += new System.EventHandler(this.button3_Click);
            _bookDAO = new BookDAO();
        }

        private void FormStudentDashboard_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Retrieve the search criteria from the text boxes
            string bookName = textBox1.Text.Trim();
            string authorText = textBox2.Text.Trim();
            string publishYearText = textBox3.Text.Trim();

            // Attempt to parse the author text to an integer if it's not empty
            bool isAuthorParsed = int.TryParse(authorText, out int author);
            int? publishYear = int.TryParse(publishYearText, out int pYear) ? pYear : (int?)null;

            // Prepare the search pattern for book name to allow partial matches
            string bookNamePattern = string.IsNullOrEmpty(bookName) ? null : $"%{bookName}%";

            // Check if at least one search criterion is provided
            if (string.IsNullOrEmpty(bookName) && !isAuthorParsed)
            {
                MessageBox.Show("Please enter at least one search criterion: Book Name or Author.");
                return;
            }

            // Create an instance of the BookController
            BookController bookController = new BookController();

            // Call the SearchBooks method with the provided search criteria
            // This method should return a List<BookDTO> or a DataTable that can be directly assigned as a DataSource
            var books = bookController.SearchBooks(bookNamePattern, isAuthorParsed ? author : (int?)null, publishYear);


            // Assign the result to the DataGridView's DataSource to display the books
            dataGridView2.DataSource = books;

            // Check if any books were found and inform the user if none were found
            if (books == null || books.Count == 0)
            {
                MessageBox.Show("No books found matching the search criteria.");
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Create an instance of the BookController
            BookController bookController = new BookController();


            // Call the GetAllBooks method to retrieve all books
            var books = bookController.GetAllBooks();

            // Assign the result to the DataGridView's DataSource to display the books
            dataGridView2.DataSource = books;

            // Check if any books were found and inform the user if none were found
            if (books == null || books.Count == 0)
            {
                MessageBox.Show("No books available.");
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Close the current dashboard form
            this.Hide();

            // Create an instance of the login form and show it
            FormLogin2 formLogin = new FormLogin2();
            formLogin.ShowDialog();
            //after the login form is closed, close dashboard
            this.Close();
            SessionManager.Logout();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to borrow.");
                return;
            }
            // Get the selected book's ISBN
            var selectedRow = dataGridView2.SelectedRows[0];
            var isbn = selectedRow.Cells[0].Value.ToString();
            //int userID = GetCurrentUserId();

            // Prompt for student name and ID
            using (var borrowDialog = new BorrowDialog(isbn)) // Pass the ISBN to the BorrowDialog constructor
            {
                if (borrowDialog.ShowDialog() == DialogResult.OK)
                {
                    string studentName = borrowDialog.FullName;
                    int studentId = borrowDialog.StudentId; // This should not throw an exception, ensure it's handled inside BorrowDialog

                    // Call BookDAO to execute the borrow operation
                    bool success = _bookDAO.BorrowBook(borrowDialog.ISBN, studentName, studentId);
                    if (success)
                    {
                        MessageBox.Show("Book borrowed successfully.");
                        // Optionally: Refresh the DataGridView to reflect the book's new status
                    }
                    else
                    {
                        MessageBox.Show("Failed to borrow the book.");
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to return.");
                return;
            }

            // Get the selected book's ISBN from the DataGridView
            var selectedRow = dataGridView2.SelectedRows[0];
            string isbn = selectedRow.Cells["ISBN"].Value.ToString();

            int? studentId = PromptForStudentId();

            if (!studentId.HasValue)
            {
                MessageBox.Show("A student ID is required to return a book.");
                return;
            }

            // Call the ReturnBook method with the obtained student ID
            bool success = _bookDAO.ReturnBook(isbn, studentId.Value);
            if (success)
            {
                MessageBox.Show($"The book has been returned on {DateTime.Now}.");
                // Optionally: Refresh the DataGridView to reflect the book's updated status
            }
            else
            {
                MessageBox.Show("Failed to return the book. Please check the details and try again.");
            }


        }

        private static int? PromptForStudentId()
        {
            // This method will show a dialog box asking the user to input their Student ID.
            // You need to create this input dialog form (or use an existing one if you have).
            using (var inputDialog = new InputDialog("Enter Student ID:"))
            {
                var result = inputDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(inputDialog.InputText))
                {
                    if (int.TryParse(inputDialog.InputText, out int studentId))
                    {
                        return studentId;
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid Student ID.");
                    }
                }
            }
            return null; // Return null if the user cancels or enters an invalid ID
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to reserve.");
                return;
            }
            var selectedBookID = dataGridView2.SelectedRows[0].Cells["ISBN"].Value.ToString();
            bool isBorrowed = _bookDAO.IsBookBorrowed(selectedBookID);
            if (isBorrowed)
            {
                MessageBox.Show("This book is currently borrowed and cannot be reserved.");
                return;
            }

            // Open the BorrowDialog to get student name and ID
            using (BorrowDialog borrowDialog = new BorrowDialog(selectedBookID)) // Assuming you pass the book ID to the dialog
            {
                // Show the BorrowDialog as a modal dialog and check the result
                if (borrowDialog.ShowDialog() == DialogResult.OK)
                {
                    // Retrieve studentName and studentId from the BorrowDialog
                    string studentName = borrowDialog.FullName;
                    string studentId = borrowDialog.StudentId.ToString();

                    // Check if the book is already borrowed or reserved
                    bool isAvailable = !_bookDAO.IsBookBorrowed(selectedBookID) && !_bookDAO.IsBookReserved(selectedBookID);

                    if (!isAvailable)
                    {
                        MessageBox.Show("This book is currently borrowed or reserved and cannot be reserved.");
                        return;
                    }

                    // Now that we have a student name and ID, we can try to reserve the book
                    bool success = _bookDAO.ReserveBook(selectedBookID, studentName, studentId);
                    if (success)
                    {
                        MessageBox.Show("The book has been successfully reserved.");
                        // Optionally: Refresh the DataGridView to reflect the book's new reserved status
                    }
                    else
                    {
                        MessageBox.Show("Failed to reserve the book.");
                    }
                }
            }
        }
    }
}
//MessageBox.Show($"An error occurred: {ex.Message}");