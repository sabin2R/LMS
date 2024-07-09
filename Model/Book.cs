using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Book
    {
        public string ISBN { get; set; }
        public string BookName { get; set; }
        public int Author { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public int PublishYear { get; set; }
        public int Pages { get; set; }
        public string Publisher { get; set; }

    }
}
