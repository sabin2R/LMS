using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AuthorLogic
    {
        private AuthorDAO _authorDAO;

        public AuthorLogic()
        {
            _authorDAO = new AuthorDAO();
        }

        public void AddAuthor(string name)
        {
            // Add business rules or validations
            _authorDAO.AddAuthor(name);
        }

        public void UpdateAuthor(int id, string newName)
        {
            // Add business rules or validations
            _authorDAO.UpdateAuthor(id, newName);
        }

        public void DeleteAuthor(int id)
        {
            // Add business rules or validations
            _authorDAO.DeleteAuthor(id);
        }

        public DataTable GetAllAuthors()
        {
            return _authorDAO.GetAllAuthors();
        }
    }
}
