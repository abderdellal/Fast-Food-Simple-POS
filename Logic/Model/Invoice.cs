using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model
{
    public class Invoice
    {
        public Invoice()
        {
            InvoiceDate = DateTime.Now;
            Sales = new List<Sale>();
        }

        public int InvoiceID { get; set; }
        public DateTime InvoiceDate { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
