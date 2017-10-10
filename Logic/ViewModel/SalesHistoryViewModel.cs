using System;
using System.Linq;
using System.Data.Entity;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using Logic.Model;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;

namespace Logic.ViewModel
{
    public class SalesHistoryViewModel : ViewModelBase
    {
        public ObservableCollection<Sale> Sales { get; set; } = new ObservableCollection<Sale>();

        public DateTime maxDate { get; set; }
        public DateTime minDate { get; set; }
        public ItemType? typeSelected { get; set; }

        public int TotalSum
        {
            get
            {
                return Sales.Sum(s => s.totalPrice);
            }
        }

        public IEnumerable<ItemType> MyEnumTypeValues
        {
            get
            {
                return  Enum.GetValues(typeof(ItemType))
                    .Cast<ItemType>();
            }
        }

        public RelayCommand refreshCommand { get; set; }

        public SalesHistoryViewModel()
        {
            maxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 0, 0, 0);
            minDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0,0,0);

            populateSales();

            refreshCommand = new RelayCommand(() =>
            {
                populateSales();
            });
        }

        private void populateSales()
        {
            if (Sales != null)
                Sales.Clear();
            else
                Sales = new ObservableCollection<Sale>();

            using (var ctx = new FastFoodContext())
            {
                List<Sale> sales;
                if (typeSelected != null)
                {
                    sales = ctx.Sales.Include(s => s.Invoice).Where(s => (s.Invoice.InvoiceDate >= minDate && s.Invoice.InvoiceDate <= maxDate && s.ItemType == typeSelected)).ToList();
                }
                else
                {
                    sales = ctx.Sales.Include(s => s.Invoice).Where(s => (s.Invoice.InvoiceDate >= minDate && s.Invoice.InvoiceDate <= maxDate)).ToList();
                }

                foreach (Sale sale in sales)
                {
                    Sales.Add(sale);
                }
                RaisePropertyChanged("TotalSum");
                typeSelected = null;
            }
        }
    }
}
