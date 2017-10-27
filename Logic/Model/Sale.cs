using System.ComponentModel.DataAnnotations.Schema;

namespace Logic.Model
{
    public class Sale : ModelBase
    {
        public Sale()
        {
        }

        public Sale(string itemName, int unitPrice, int amount)
        {
            ItemName = itemName;
            UnitPrice = unitPrice;
            Amount = amount;
        }

        public int SaleId { get; set; }
        public string ItemName { get; set; }
        public int UnitPrice { get; set; }
        public int Amount { get; set; }
        public ItemType ItemType { get; set; }

        [NotMapped]
        public int TotalPrice => UnitPrice * Amount;

        public Invoice Invoice { get; set; }
    }

    /// <summary>
    /// type of items
    /// </summary>
    public enum ItemType
    {
        Sandwich,
        Boisson
    }
}