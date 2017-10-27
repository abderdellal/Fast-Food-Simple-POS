using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Logic.Model;
using Logic.ViewModel.Messages;

namespace Logic.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
        {
            Sales = new ObservableCollection<Sale>();
            PopulateItems();

            AddSale = new RelayCommand<Item>(item =>
            {
                var exists = false;
                foreach (var sale in Sales)
                    if (sale.ItemName == item.ItemName)
                    {
                        sale.Amount++;
                        SelectedSale = sale;
                        exists = true;
                        break;
                    }
                if (!exists)
                {
                    var s = new Sale();
                    s.ItemName = item.ItemName;
                    s.UnitPrice = item.ItemPrice;
                    s.Amount = 1;
                    s.ItemType = item.ItemType;

                    Sales.Add(s);
                    SelectedSale = s;
                }
                RaisePropertyChanged("Total");
                ComandesRaiseCanExecuteChange();
            });

            DeleteSale = new RelayCommand(() =>
                {
                    if (SelectedSale != null)
                    {
                        Sales.Remove(SelectedSale);
                        if (Sales.Count != 0)
                            SelectedSale = Sales.Last();
                        else
                            SelectedSale = null;
                        ComandesRaiseCanExecuteChange();
                    }
                },
                () => SelectedSale != null);

            ResetSales = new RelayCommand(() =>
                {
                    Sales.Clear();
                    RaisePropertyChanged("Total");
                    SelectedSale = null;
                    ComandesRaiseCanExecuteChange();
                },
                () => Sales.Count > 0);

            IncreaseSaleAmount = new RelayCommand(() =>
                {
                    if (SelectedSale != null)
                    {
                        SelectedSale.Amount++;
                        RaisePropertyChanged("Total");
                        ComandesRaiseCanExecuteChange();
                    }
                },
                () => SelectedSale != null);

            DecreaseSaleAmount = new RelayCommand(() =>
                {
                    if (SelectedSale != null)
                    {
                        if (SelectedSale.Amount > 1)
                        {
                            SelectedSale.Amount--;
                            RaisePropertyChanged("Total");
                        }
                        ComandesRaiseCanExecuteChange();
                    }
                },
                () => SelectedSale != null && SelectedSale.Amount > 1);

            SaveInvoice = new RelayCommand(() =>
                {
                    using (var ctx = new FastFoodContext())
                    {
                        var invoice = new Invoice();

                        invoice.InvoiceDate = DateTime.Now;

                        foreach (var sale in Sales)
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
                    ComandesRaiseCanExecuteChange();
                },
                () => Sales.Count > 0);

            MessengerInstance.Register<ItemAddedMessage>(this, m => { PopulateItems(); });
            MessengerInstance.Register<ItemDeletedMessage>(this, m => { PopulateItems(); });
        }

        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Sale> Sales { get; set; }
        public Sale SelectedSale { get; set; }
        public RelayCommand<Item> AddSale { get; set; }
        public RelayCommand DeleteSale { get; set; }
        public RelayCommand ResetSales { get; set; }
        public RelayCommand IncreaseSaleAmount { get; set; }
        public RelayCommand DecreaseSaleAmount { get; set; }
        public RelayCommand SaveInvoice { get; set; }

        public int Total
        {
            get
            {
                if (Sales != null && Sales.Count > 0)
                    return Sales.Sum(s => s.UnitPrice * s.Amount);
                return 0;
            }
        }

        private void PopulateItems()
        {
            Items = new ObservableCollection<Item>();

            using (var ctx = new FastFoodContext())
            {
                foreach (var item in ctx.Items)
                    Items.Add(item);
            }
        }

        public void SelectSale(Sale sale)
        {
            SelectedSale = sale;
            ComandesRaiseCanExecuteChange();
        }

        private void ComandesRaiseCanExecuteChange()
        {
            DeleteSale.RaiseCanExecuteChanged();
            IncreaseSaleAmount.RaiseCanExecuteChanged();
            DecreaseSaleAmount.RaiseCanExecuteChanged();
            ResetSales.RaiseCanExecuteChanged();
            SaveInvoice.RaiseCanExecuteChanged();
        }
    }
}