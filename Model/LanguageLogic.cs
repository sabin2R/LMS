using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LanguageLogic
    {
        private LanguageDAO _languageDAO;

        public LanguageLogic()
        {
            _languageDAO = new LanguageDAO();
        }

        public void AddLanguage(string name)
        {
            // Add business rules or validations
            _languageDAO.AddLanguage(name);
        }

        public void UpdateLanguage(int id, string newName)
        {
            // Add business rules or validations
            _languageDAO.UpdateLanguage(id, newName);
        }

        public void DeleteLanguage(int id)
        {
            // Add business rules or validations
            _languageDAO.DeleteLanguage(id);
        }

        public DataTable GetAllLanguages()
        {
            return _languageDAO.GetAllLanguages();
        }
    }
}
