using Controller;
using Model;
using System;
using System.Windows.Forms;

namespace View
{
    public partial class FormLogin2 : Form
    {
        public FormLogin2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sUserName = textBox1.Text;
            string sPassword = maskedTextBox1.Text; 

            // Connect to the Controller
            UserController userController = new UserController();
            User user = userController.ValidateLogin(sUserName, sPassword);

            if (user != null)
            {
                int userLevel = user.UserLevel;

                // Determine the form to open based on user level
                Form dashboardForm = null;

                switch (userLevel)
                {
                    case 1:
                        dashboardForm = new FormStudentDashboard();
                        break;
                    //case 2:
                        //dashboardForm = new FormSupervisorDashboard();
                        //break;
                    case 3:
                        dashboardForm = new FormAdminDashboard();
                        break;
                    default:
                        MessageBox.Show("Invalid user level. Please contact the administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                if (dashboardForm != null)
                {
                    // Redirect to the appropriate dashboard
                    this.Hide();
                    dashboardForm.ShowDialog();
                    //closes the login form 
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid credentials. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormLogin2_Load(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.LightBlue;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            MessageBox.Show("Invalid input. Please enter a valid password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
