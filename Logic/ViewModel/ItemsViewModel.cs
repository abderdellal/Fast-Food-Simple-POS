using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Logic.Model;
using Logic.ViewModel.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ViewModel
{
    public class ItemsViewModel : ViewModelBase
    {
        public ObservableCollection<Item> ItemList { get; set; }
        public RelayCommand<Item> DeleteItemCommand { get; set; } 

        public ItemsViewModel()
        {
            populateView();

            DeleteItemCommand = new RelayCommand<Item>(item =>
            {
                using (var ctx = new FastFoodContext())
                {
                    ctx.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    ctx.SaveChanges();
                    ItemList.Remove(item);
                }
                MessengerInstance.Send(new ItemDeletedMessage());
            });

            MessengerInstance.Register<ItemAddedMessage>(this, m =>
            {
                populateView();
            });
        }

        private void populateView()
        {
            ItemList = new ObservableCollection<Item>();
            using (var ctx = new FastFoodContext())
            {
                foreach (Item item in ctx.Items)
                {
                    ItemList.Add(item);
                }
            }

        }
    }
}
