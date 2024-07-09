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

namespace View
{
    public partial class FormMasterInformationMaintainence : Form
    {
        private CategoryLogic _categoryLogic;
        private LanguageLogic _languageLogic;
        private AuthorLogic _authorLogic;
        public FormMasterInformationMaintainence()
        {
            InitializeComponent();
            _categoryLogic = new CategoryLogic();
            _languageLogic = new LanguageLogic();
            _authorLogic = new AuthorLogic();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormMasterInformationMaintainence_Load(object sender, EventArgs e)
        {


            comboBox2.SelectedIndex = 0;  // Default to first item ('Category')
            PopulateDataGridView();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDataGridView();

        }
        private void PopulateDataGridView()
        {
            switch (comboBox2.SelectedItem.ToString())
            {
                case "Category":
                    dataGridView1.DataSource = _categoryLogic.GetAllCategories();
                    break;
                case "Language":
                    dataGridView1.DataSource = _languageLogic.GetAllLanguages();
                    break;
                case "Author":
                    dataGridView1.DataSource = _authorLogic.GetAllAuthors();
                    break;
            }
        }


        private int GetSelectedIdFromDataGridView()
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                throw new InvalidOperationException("No row selected.");
            }

            // Assuming the first column of your DataGridView is the ID column
            return Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            if (!string.IsNullOrWhiteSpace(input))
            {
                ExecuteOperation("Add", input);
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a value to add.");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int selectedId = GetSelectedIdFromDataGridView();
            if (selectedId >= 0)
            {
                ExecuteOperation("Delete", null, selectedId);
            }
            else
            {
                MessageBox.Show("Please select an item to delete.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            int selectedId = GetSelectedIdFromDataGridView();
            if (!string.IsNullOrWhiteSpace(input) && selectedId >= 0)
            {
                ExecuteOperation("Update", input, selectedId);
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Please select an item to update and enter the new value.");
            }
        }
        private void ExecuteOperation(string operation, string value, int id = 0)
        {
            string type = comboBox2.SelectedItem.ToString();
            try
            {
                switch (type)
                {
                    case "Category":
                        ExecuteCategoryOperation(operation, id, value);
                        break;
                    case "Language":
                        ExecuteLanguageOperation(operation, id, value);
                        break;
                    case "Author":
                        ExecuteAuthorOperation(operation, id, value);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            PopulateDataGridView();
        }

        private void ExecuteCategoryOperation(string operation, int id, string value)
        {
            switch (operation)
            {
                case "Add":
                    _categoryLogic.AddCategory(value);
                    MessageBox.Show("Category added successfully.");
                    break;
                case "Update":
                    _categoryLogic.UpdateCategory(id, value);
                    MessageBox.Show("Category updated successfully.");
                    break;
                case "Delete":
                    _categoryLogic.DeleteCategory(id);
                    MessageBox.Show("Category deleted successfully.");
                    break;
            }
        }
        private void ExecuteLanguageOperation(string operation, int id, string value)
        {
            switch (operation)
            {
                case "Add":
                    _languageLogic.AddLanguage(value);
                    MessageBox.Show("Language added successfully.");
                    break;
                case "Update":
                    _languageLogic.UpdateLanguage(id, value);
                    MessageBox.Show("Language updated successfully.");
                    break;
                case "Delete":
                    _languageLogic.DeleteLanguage(id);
                    MessageBox.Show("Language deleted successfully.");
                    break;
            }
        }
        private void ExecuteAuthorOperation(string operation, int id, string value)
        {
            switch (operation)
            {
                case "Add":
                    _authorLogic.AddAuthor(value);
                    MessageBox.Show("Author added successfully.");
                    break;
                case "Update":
                    _authorLogic.UpdateAuthor(id, value);
                    MessageBox.Show("Author updated successfully.");
                    break;
                case "Delete":
                    _authorLogic.DeleteAuthor(id);
                    MessageBox.Show("Author deleted successfully.");
                    break;
            }
        }
        private void ExecuteUpdateOperation(int selectedId, string value)
        {
            switch (comboBox2.SelectedItem.ToString())
            {
                case "Category":
                    _categoryLogic.UpdateCategory(selectedId, value);
                    MessageBox.Show("Category updated successfully.");
                    break;
                case "Language":
                    _languageLogic.UpdateLanguage(selectedId, value);
                    MessageBox.Show("Language updated successfully.");
                    break;
                case "Author":
                    _authorLogic.UpdateAuthor(selectedId, value);
                    MessageBox.Show("Author updated successfully.");
                    break;
            }
            PopulateDataGridView(); // Refresh the DataGridView with updated list
        }
        private void ExecuteDeleteOperation(int selectedId)
        {
            if (MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                switch (comboBox2.SelectedItem.ToString())
                {
                    case "Category":
                        _categoryLogic.DeleteCategory(selectedId);
                        MessageBox.Show("Category deleted successfully.");
                        break;
                    case "Language":
                        _languageLogic.DeleteLanguage(selectedId);
                        MessageBox.Show("Language deleted successfully.");
                        break;
                    case "Author":
                        _authorLogic.DeleteAuthor(selectedId);
                        MessageBox.Show("Author deleted successfully.");
                        break;
                }
                PopulateDataGridView(); // Refresh the DataGridView with updated list
            }
        }
        private void ExecuteAddOperation(string value)
        {
            switch (comboBox2.SelectedItem.ToString())
            {
                case "Category":
                    _categoryLogic.AddCategory(value);
                    MessageBox.Show("Category added successfully.");
                    break;
                case "Language":
                    _languageLogic.AddLanguage(value);
                    MessageBox.Show("Language added successfully.");
                    break;
                case "Author":
                    _authorLogic.AddAuthor(value);
                    MessageBox.Show("Author added successfully.");
                    break;
            }
            PopulateDataGridView(); // Refresh the DataGridView with updated list
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
    

