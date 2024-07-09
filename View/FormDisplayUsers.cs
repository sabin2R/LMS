using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controller;
using Model;
using ClassLibraryDTO;

namespace View
{
    public partial class FormDisplayUsers : Form
    {
        public FormDisplayUsers()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //connect to the Controller
            UserController userController = new UserController();
            List<UserDTO> users = userController.GetAllUsers();

            //display the data
            dataGridView1.DataSource = users;
        }

        private void FormDisplayUsers_Load(object sender, EventArgs e)
        {

        }
    }
}
