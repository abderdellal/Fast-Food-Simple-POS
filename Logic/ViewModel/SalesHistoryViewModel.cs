using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Logic.Model;

namespace Logic.ViewModel
{
    public class SalesHistoryViewModel : ViewModelBase
    {
        public SalesHistoryViewModel()
        {
            //tomorow
            MaxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 0, 0, 0);
            //today
            MinDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            PopulateSales();

            RefreshCommand = new RelayCommand(PopulateSales);
        }

        public ObservableCollection<Sale> Sales { get; set; } = new ObservableCollection<Sale>();

        //interval of time
        public DateTime MaxDate { get; set; }
        public DateTime MinDate { get; set; }

        //type of items to be displayed
        public ItemType? TypeSelected { get; set; }

        //total sum of sales displayed
        public int TotalSum
        {
            get { return Sales.Sum(s => s.TotalPrice); }
        }

        //list of types an item can have
        public IEnumerable<ItemType> MyEnumTypeValues => Enum.GetValues(typeof(ItemType))
            .Cast<ItemType>();

        public RelayCommand RefreshCommand { get; set; }

        private void PopulateSales()
        {
            if (Sales != null)
                Sales.Clear();
            else
                Sales = new ObservableCollection<Sale>();

            using (var ctx = new FastFoodContext())
            {
                List<Sale> sales;
                if (TypeSelected != null)
                    sales = ctx.Sales.Include(s => s.Invoice).Where(s =>
                        s.Invoice.InvoiceDate >= MinDate && s.Invoice.InvoiceDate <= MaxDate &&
                        s.ItemType == TypeSelected).ToList();
                else
                    sales = ctx.Sales.Include(s => s.Invoice)
                        .Where(s => s.Invoice.InvoiceDate >= MinDate && s.Invoice.InvoiceDate <= MaxDate).ToList();

                foreach (var sale in sales)
                    Sales.Add(sale);

                //updates the total sum
                RaisePropertyChanged("TotalSum");

                TypeSelected = null;
            }
        }
    }
}