using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using Controller;
using ClassLibraryDTO;

namespace View
{
    public partial class FormUserInformationMaintainance : Form
    {
        private UserController _userController;

        public FormUserInformationMaintainance()
        {
            InitializeComponent();
            _userController = new UserController();
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            int selectedUserId = GetSelectedUserId();
            if (selectedUserId <= 0)
            {
                MessageBox.Show("Please select a user to update.");
                return;
            }

            UserDTO updatedUser = new UserDTO
            {
                Uid = selectedUserId,
                UserName = textBox2.Text,
                Password = textBox3.Text, // Hash the password before storing
                UserLevel = Convert.ToInt32(textBox4.Text) // Convert the user level to an integer
            };

            bool success = _userController.UpdateUser(selectedUserId,updatedUser);
            if (success)
            {
                MessageBox.Show("User updated successfully.");
                PopulateDataGridView();
            }
            else
            {
                MessageBox.Show("Failed to update the user.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserDTO newUser = new UserDTO
            {
                UserName = textBox2.Text,
                Password = textBox3.Text, // Hash the password before storing
                UserLevel = Convert.ToInt32(textBox4.Text) // Convert the user level to an integer
            };

            bool success = _userController.AddUser(newUser);
            if (success)
            {
                MessageBox.Show("User added successfully.");
                PopulateDataGridView();
            }
            else
            {
                MessageBox.Show("Failed to add the user.");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int selectedUserId = GetSelectedUserId();
            if (selectedUserId <= 0)
            {
                MessageBox.Show("Please select a user to delete.");
                return;
            }

            UserDTO userToDelete = new UserDTO { Uid = selectedUserId };
            bool success = _userController.DeleteUser(selectedUserId, userToDelete);
            if (success)
            {
                MessageBox.Show("User deleted successfully.");
                PopulateDataGridView();
            }
            else
            {
                MessageBox.Show("Failed to delete the user.");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormUserInformationMaintainance_Load(object sender, EventArgs e)
        {
            PopulateDataGridView();

        }
        private void PopulateDataGridView()
        {
            // Assuming GetAllUsers() returns a list or a binding source that can be directly assigned to the DataSource property
            dataGridView1.DataSource = _userController.GetAllUsers();
            dataGridView1.AutoGenerateColumns = false; // Set to false if you are creating custom columns
                                                       // Define columns manually if needed
        }
        private int GetSelectedUserId()
        {
            // Assuming your DataGridView has a hidden column (e.g., the first column) that holds the user's ID
            if (dataGridView1.SelectedRows.Count > 0)
            {
                return Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Uid"].Value); // Replace "UserIdColumn" with your actual column name
            }
            return -1; // Return -1 or some other indicator that no row is selected
        }
        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
