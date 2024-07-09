using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataSetAuthorTableAdapters;

namespace Model
{
    internal class AuthorDAO
    {
        private TabAuthorTableAdapter _adapter = new TabAuthorTableAdapter();

        public void AddAuthor(string name)
        {
            // The Insert method must be configured in your TabAuthorTableAdapter
            _adapter.Insert(name);
        }

        public void UpdateAuthor(int id, string name)
        {
            // Fetch the original author to get the original name for concurrency check
            var author = _adapter.GetData().FirstOrDefault(a => a.AID == id);
            if (author != null)
            {
                // The Update method should be configured to match this signature in your TabAuthorTableAdapter
                _adapter.Update(name, id, author.AuthorName);
            }
            else
            {
                throw new Exception("Author not found for update.");
            }
        }

        public void DeleteAuthor(int id)
        {
            // Fetch the original author to get the original name for concurrency check
            var author = _adapter.GetData().FirstOrDefault(a => a.AID == id);
            if (author != null)
            {
                // The Delete method should be configured to match this signature in your TabAuthorTableAdapter
                _adapter.Delete(id, author.AuthorName);
            }
            else
            {
                throw new Exception("Author not found for delete.");
            }
        }

        public DataSetAuthor.TabAuthorDataTable GetAllAuthors()
        {
            // This will return all authors using the GetData method defined in your TabAuthorTableAdapter
            return _adapter.GetData();
        }
    }
}
