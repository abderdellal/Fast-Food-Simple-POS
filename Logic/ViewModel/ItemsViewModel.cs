using System.Collections.ObjectModel;
using System.Data.Entity;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Logic.Model;
using Logic.ViewModel.Messages;

namespace Logic.ViewModel
{
    public class ItemsViewModel : ViewModelBase
    {
        public ItemsViewModel()
        {
            PopulateView();

            DeleteItemCommand = new RelayCommand<Item>(item =>
            {
                using (var ctx = new FastFoodContext())
                {
                    ctx.Entry(item).State = EntityState.Deleted;
                    ctx.SaveChanges();
                    ItemList.Remove(item);
                }
                MessengerInstance.Send(new ItemDeletedMessage());
            });

            MessengerInstance.Register<ItemAddedMessage>(this, m => { PopulateView(); });
        }

        public ObservableCollection<Item> ItemList { get; set; }
        public RelayCommand<Item> DeleteItemCommand { get; set; }

        private void PopulateView()
        {
            ItemList = new ObservableCollection<Item>();
            using (var ctx = new FastFoodContext())
            {
                foreach (var item in ctx.Items)
                    ItemList.Add(item);
            }
        }
    }
}