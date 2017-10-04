using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model
{
    public class Sale : ModelBase
    {
        public Sale()
        {

        }
        public Sale(string ItemName, int unitPrice, int Amount)
        {
            this.ItemName = ItemName;
            this.UnitPrice = unitPrice;
            this.Amount = Amount;
        }

        public int SaleID { get; set; }
        public string ItemName { get; set; }
        public int UnitPrice { get; set; }
        public int Amount { get; set; }

        [NotMapped]
        public int totalPrice
        {
            get
            {
                return UnitPrice * Amount;
            }
        }

        public Invoice Invoice { get; set; }
    }
}
