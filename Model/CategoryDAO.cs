using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataSetCategoryTableAdapters;       

namespace Model
{
    internal class CategoryDAO
    {
        private TabCategoryTableAdapter _adapter = new TabCategoryTableAdapter();

        public void AddCategory(string name)
        {
            // Assuming the Insert method has been configured in the TableAdapter.
            // If not, you'll need to configure it using the DataSet designer.
            _adapter.Insert(name);
        }

        public void UpdateCategory(int id, string newName)
        {
            // Fetch the current category to get the original name
            var category = _adapter.GetData().FirstOrDefault(c => c.CID == id);
            if (category != null)
            {
                // Use the original category name as the third parameter
                _adapter.Update(newName, id, category.CategoryName);
            }
            else
            {
                // Handle the case where the category is not found
                throw new Exception("Category not found for update.");
            }
        }

        public void DeleteCategory(int id)
        {
            // Fetch the current category to get the original name
            var category = _adapter.GetData().FirstOrDefault(c => c.CID == id);
            if (category != null)
            {
                // Use the original category name as the second parameter
                _adapter.Delete(id, category.CategoryName);
            }
            else
            {
                // Handle the case where the category is not found
                throw new Exception("Category not found");
            }
        }

        public DataSetCategory.TabCategoryDataTable GetAllCategories()
        {
            // The return type should match the actual DataTable name for categories in your DataSet
            return _adapter.GetData();
        }
    }
}
