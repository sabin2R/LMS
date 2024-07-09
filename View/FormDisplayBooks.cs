using ClassLibraryDTO;
using Controller;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace View
{
    public partial class FormDisplayBooks : Form
    {
        private System.Windows.Forms.DataGridView dataGridViewBooks;


        public FormDisplayBooks()
        {
            InitializeComponent();
           
        }

        private void FormDisplayBooks_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Connect to the BookController
            BookController bookController = new BookController();
            List<BookDTO> books = bookController.GetAllBooks();

            // Display the books in the DataGridView
            dataGridView2.DataSource = books;

        }
        public void DisplaySearchResults(string bookName, int author, int publishYear)
        {
            BookController bookController = new BookController();
            List<BookDTO> books = bookController.SearchBooks(bookName, author, publishYear);

            dataGridViewBooks.DataSource = books;
        }

    }

}
