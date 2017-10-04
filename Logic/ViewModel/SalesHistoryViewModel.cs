using System;
using System.Linq;
using System.Data.Entity;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using Logic.Model;
using GalaSoft.MvvmLight.Command;

namespace Logic.ViewModel
{
    public class SalesHistoryViewModel : ViewModelBase
    {
        public ObservableCollection<Sale> Sales { get; set; }

        public DateTime maxDate { get; set; }
        public DateTime minDate { get; set; }

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
                foreach (Sale sale in ctx.Sales.Include(s => s.Invoice).Where(s => (s.Invoice.InvoiceDate >= minDate && s.Invoice.InvoiceDate <= maxDate)))
                {
                    Sales.Add(sale);
                }
            }
        }
    }
}
