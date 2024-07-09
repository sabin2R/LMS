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
    public partial class BorrowDialog : Form
    {
        private BookDAO _bookDAO;
        public string ISBN { get; private set; }
        public BorrowDialog(string isbn)
        {
            InitializeComponent();
            _bookDAO = new BookDAO();
            ISBN = isbn;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public string FullName
        {
            get { return textBox1.Text; }
        }

        public int StudentId
        {
            get
            {
                if (int.TryParse(textBox2.Text, out int studentId))
                {
                    return studentId;
                }
                else
                {
                    throw new InvalidOperationException("Please enter a valid Student ID.");
                }
            }
        }
        private void BorrowDialog_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Close the dialog without doing anything
            this.DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_bookDAO == null)
            {
                MessageBox.Show("Book data access object is not initialized.");
                return;
            }

            try
            {
                // Assume you have TextBox controls or some other way to get the following information:
                //string isbn;
                string studentName = this.textBox1.Text; // TextBox containing the student's name
                string studentIdText = this.textBox2.Text; // TextBox containing the student's ID as a string

                // Validate that the necessary information is provided
                if ( string.IsNullOrEmpty(studentName) || string.IsNullOrEmpty(studentIdText))
                {
                    MessageBox.Show("Please provide all required information.");
                    return;
                }

                // Convert student ID from string to int
                if (!int.TryParse(studentIdText, out int studentId))
                {
                    MessageBox.Show("Student ID must be a valid number.");
                    return;
                }

                // Here you would call the method to perform the borrowing
                // For example, _bookDAO.BorrowBook(bookId, studentName, studentId);
                // This is a placeholder for the actual borrowing logic
                bool isBorrowed = _bookDAO.BorrowBook(ISBN, studentName, studentId);

                if (isBorrowed)
                {
                    MessageBox.Show($"The book has been borrowed at {DateTime.Now}.");
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Failed to borrow the book. It may already be borrowed by someone else.");
                    this.DialogResult = DialogResult.None;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while borrowing the book: {ex.Message}");
                this.DialogResult = DialogResult.None;
            }

        }
    }
}
