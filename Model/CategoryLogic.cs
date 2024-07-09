using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CategoryLogic
    {
        private CategoryDAO _categoryDAO;

        public CategoryLogic()
        {
            _categoryDAO = new CategoryDAO();
        }

        public void AddCategory(string name)
        {
            // Here you can add business rules or validations if needed
            _categoryDAO.AddCategory(name);
        }

        public void UpdateCategory(int id, string newName)
        {
            // Business rules or validations go here
            _categoryDAO.UpdateCategory(id, newName);
        }

        public void DeleteCategory(int id)
        {
            // Business rules or validations go here
            _categoryDAO.DeleteCategory(id);
        }

        public DataTable GetAllCategories()
        {
            return _categoryDAO.GetAllCategories();
        }
    }
}
