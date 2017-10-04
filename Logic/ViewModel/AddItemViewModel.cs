using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Logic.Model;
using Logic.Properties;
using Logic.ViewModel.Messages;

namespace Logic.ViewModel
{
    public class AddItemViewModel : ViewModelBase
    {
        public Item item { get; set; }
        public string formulaireValide { get; set; }
        public RelayCommand SaveCommande { get; set; }

        public AddItemViewModel()
        {
            item = new Item();
            formulaireValide = null;
            SaveCommande = new RelayCommand(Save, IsValid);
            item.PropertyChanged += (x, y) =>
            {
                SaveCommande.RaiseCanExecuteChanged();
            };
        }

        public bool IsValid()
        {
            formulaireValide = null;
            return item.IsValid();
        }
        private void Save()
        {
            using (var ctx = new FastFoodContext())
            {

                ctx.Items.Add(item);
                ctx.SaveChanges();
            }
            formulaireValide = Resources.ItemAdded;
            MessengerInstance.Send(new ItemAddedMessage());
            item.ItemName = "";
            item.ItemPrice = 0;
        }
    }
}
