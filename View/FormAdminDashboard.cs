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
using Model.DataSetBorrowingsTableAdapters;

namespace View
{
    public partial class FormAdminDashboard : Form
    {
        private Form _loginForm2;
        private DataSetBorrowings.BorrowingsDataTable _borrowingsTableAdapter;
        private DataSetBorrowings _dataSetBorrowings;
        public FormAdminDashboard()
        {
            InitializeComponent();
            _borrowingsTableAdapter = new DataSetBorrowings.BorrowingsDataTable();
            _dataSetBorrowings = new DataSetBorrowings();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create an instance of the FormMasterInformationMaintainence form
            FormMasterInformationMaintainence masterInfoForm = new FormMasterInformationMaintainence();

            // Optionally, if you want to hide the admin dashboard while the other form is open, you can do:
            //this.Hide();

            // Show the Master Information Maintenance form
            masterInfoForm.Show();

            // If you hid the admin dashboard form and want to show it again when the masterInfoForm is closed, you might do:
            masterInfoForm.FormClosed += (s, args) => this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormUserInformationMaintainance userInfoForm = new FormUserInformationMaintainance();

            // Show the UserInformationMaintainance form
            userInfoForm.Show();

        }

        private void FormAdminDashboard_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var dataSetBorrowings = new DataSetBorrowings();
            var borrowingsTableAdapter = new Model.DataSetBorrowingsTableAdapters.BorrowingsTableAdapter();
            // Use the GetAllBorrowings method to fill the borrowings table in the dataset
            borrowingsTableAdapter.Fill(dataSetBorrowings.Borrowings);
            // Now bind the Borrowings table to the DataGridView
            dataGridView1.DataSource = dataSetBorrowings.Borrowings;
            //ConfigureDataGridViewColumns();
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
    }

}
