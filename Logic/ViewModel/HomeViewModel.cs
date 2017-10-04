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
    public class HomeViewModel : ViewModelBase
    {
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Sale> Sales { get; set; }
        public Sale SelectedSale { get; set; }
        public RelayCommand<Item> addSale { get; set; }
        public RelayCommand DeleteSale { get; set; }
        public RelayCommand ResetSales { get; set; }
        public RelayCommand IncreaseSaleAmount { get; set; }
        public RelayCommand DecreaseSaleAmount { get; set; }
        public RelayCommand SaveInvoice { get; set; }
        public int Total { get
            {
                if (Sales != null && Sales.Count > 0)
                {
                    return Sales.Sum(s => (s.UnitPrice * s.Amount));
                }
                else return 0;
            }
        }


        public HomeViewModel()
        {
            Sales = new ObservableCollection<Sale>();
            populateItems();

            addSale = new RelayCommand<Item>(item =>
            {
                Sale s = new Sale();
                s.ItemName = item.ItemName;
                s.UnitPrice = item.ItemPrice;
                s.Amount = 1;

                Sales.Add(s);
                SelectedSale = s;
                RaisePropertyChanged("Total");
            });

            DeleteSale = new RelayCommand(() =>
            {
                if (SelectedSale != null)
                {
                    Sales.Remove(SelectedSale);
                    if (Sales.Count == 0)
                    {
                        SelectedSale = null;
                    }
                }
            });

            ResetSales = new RelayCommand(() =>
            {
                Sales.Clear();
                RaisePropertyChanged("Total");
                SelectedSale = null;
            });

            IncreaseSaleAmount = new RelayCommand(() =>
            {
                if (SelectedSale != null)
                {
                    SelectedSale.Amount++;
                    RaisePropertyChanged("Total");
                }
            });

            DecreaseSaleAmount = new RelayCommand(() =>
            {
                if (SelectedSale != null)
                {
                    if (SelectedSale.Amount > 1)
                    {
                        SelectedSale.Amount--;
                        RaisePropertyChanged("Total");

                    }
                }
            });

            SaveInvoice = new RelayCommand(() =>
            {
                
                using (var ctx = new FastFoodContext())
                {
                    Invoice invoice = new Invoice();

                    invoice.InvoiceDate = DateTime.Now;

                    foreach (Sale sale in Sales)
                    {
                        invoice.Sales.Add(sale);
                        ctx.Sales.Add(sale);
                    }
                    ctx.Invoices.Add(invoice);
                    ctx.SaveChanges();

                    Sales.Clear();

                    SelectedSale = null;
                }
                RaisePropertyChanged("Total");
            });

            MessengerInstance.Register<ItemAddedMessage>(this, m =>
            {
                populateItems();
            });
            MessengerInstance.Register<ItemDeletedMessage>(this, m =>
            {
                populateItems();
            });
        }

        private void populateItems()
        {
            Items = new ObservableCollection<Item>();

            using (var ctx = new FastFoodContext())
            {
                foreach (Item item in ctx.Items)
                {
                    Items.Add(item);
                }
            }
        }
    }
}
