using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataSetLanguageTableAdapters;

namespace Model
{
    internal class LanguageDAO
    {
        private TabLanguageTableAdapter _adapter = new TabLanguageTableAdapter();

        public void AddLanguage(string name)
        {
            // The Insert method should be defined in your TabLanguageTableAdapter
            _adapter.Insert(name);
        }

        public void UpdateLanguage(int id, string name)
        {
            // Before calling this, make sure to define or configure the Update method in your TabLanguageTableAdapter
            // The Update method typically requires the new value and the original ID and possibly the original name
            var language = _adapter.GetData().FirstOrDefault(l => l.LID == id);
            if (language != null)
            {
                _adapter.Update(name, id, language.LanguageName);
            }
            else
            {
                throw new Exception("Language not found for update.");
            }
        }

        public void DeleteLanguage(int id)
        {
            // Before calling this, make sure to define or configure the Delete method in your TabLanguageTableAdapter
            // The Delete method typically requires the original ID and possibly the original name for concurrency checks
            var language = _adapter.GetData().FirstOrDefault(l => l.LID == id);
            if (language != null)
            {
                _adapter.Delete(id, language.LanguageName);
            }
            else
            {
                throw new Exception("Language not found for delete.");
            }
        }

        public DataSetLanguage.TabLanguageDataTable GetAllLanguages()
        {
            // This will return all languages using the GetData method defined in your TabLanguageTableAdapter
            return _adapter.GetData();
        }
    }
}
