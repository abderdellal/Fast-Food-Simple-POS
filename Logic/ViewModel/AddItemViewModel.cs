using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Logic.Model;
using Logic.Properties;
using Logic.ViewModel.Messages;

namespace Logic.ViewModel
{
    public class AddItemViewModel : ViewModelBase
    {
        public AddItemViewModel()
        {
            Item = new Item();
            FormulaireValide = null;
            SaveCommand = new RelayCommand(Save, IsValid);
            Item.PropertyChanged += (x, y) => { SaveCommand.RaiseCanExecuteChanged(); };
        }

        public Item Item { get; set; }
        public string FormulaireValide { get; set; }
        public RelayCommand SaveCommand { get; set; }

        //the list of types an Item can have
        public IEnumerable<ItemType> MyEnumTypeValues => Enum.GetValues(typeof(ItemType))
            .Cast<ItemType>();

        public bool IsValid()
        {
            //remove the displayed message if any when user start typing
            FormulaireValide = null;
            return Item.IsValid();
        }

        private void Save()
        {
            using (var ctx = new FastFoodContext())
            {
                ctx.Items.Add(Item);
                ctx.SaveChanges();
            }
            FormulaireValide = Resources.ItemAdded;
            MessengerInstance.Send(new ItemAddedMessage());
            Item.ItemName = "";
            Item.ItemPrice = 0;
        }
    }
}