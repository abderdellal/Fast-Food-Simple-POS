using System;
using System.Collections.Generic;

namespace Logic.Model
{
    public class Invoice
    {
        public Invoice()
        {
            InvoiceDate = DateTime.Now;
            Sales = new List<Sale>();
        }

        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}