﻿using System.ComponentModel.DataAnnotations;

namespace Logic.Model
{
    public class Item : ModelBase
    {
        public Item()
        {
        }

        public Item(string name, int price)
        {
            ItemName = name;
            ItemPrice = price;
        }

        public int ItemId { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int ItemPrice { get; set; }

        [Required]
        public ItemType ItemType { get; set; }
    }
}