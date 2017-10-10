using Logic.Model;
using Logic.ViewModel;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace Ui.View
{
    /// <summary>
    /// Interaction logic for SalesHistoryView.xaml
    /// </summary>
    public partial class SalesHistoryView : UserControl
    {
        public SalesHistoryView()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            InitializeComponent();

            
        }

        public override void EndInit()
        {
            base.EndInit();
            if (DataContext != null && DataContext is SalesHistoryViewModel)
            {
                var viewModel = DataContext as SalesHistoryViewModel;
                DateTime minDate = viewModel.minDate;
                DateTime maxDate = viewModel.maxDate;

                minDatePicker.SelectedDate = minDate;
                maxDatePicker.SelectedDate = maxDate;
            }
        }

        private void minDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext != null && DataContext is SalesHistoryViewModel)
            {
                var viewModel = DataContext as SalesHistoryViewModel;

                var picker = sender as DatePicker;
                DateTime? date = picker.SelectedDate;

                if (date != null)
                {
                    viewModel.minDate = (DateTime) date;
                }
            }
        }

        private void maxDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext != null && DataContext is SalesHistoryViewModel)
            {
                var viewModel = DataContext as SalesHistoryViewModel;

                var picker = sender as DatePicker;
                DateTime? date = picker.SelectedDate;

                if (date != null)
                {
                    viewModel.maxDate = (DateTime)date;
                }
            }
        }

        private void Print_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string nomRestaurant = "NOM DU RESTAURANT"; //majuscule !
            string adresseRestaurant = "Adresse du Restaurant";
            string Telephone = "0123456789";
            const int ITEMS_PER_PAGE = 32;

            //get the view model
            SalesHistoryViewModel vm = (SalesHistoryViewModel)DataContext;

            var pageSize = new Size(8.26 * 96, 11.69 * 96); // A4 page, at 96 dpi
            var document = new FixedDocument();
            document.DocumentPaginator.PageSize = pageSize;

            for (int i = 0; i < (vm.Sales.Count / ITEMS_PER_PAGE) + 1; i++)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Margin = new Thickness(15, 15, 15, 15);
                stackPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
                stackPanel.Width = 8 * 94;

                TextBlock FastFoodName = new TextBlock();
                FastFoodName.Text = nomRestaurant;
                FastFoodName.HorizontalAlignment = HorizontalAlignment.Center;
                stackPanel.Children.Add(FastFoodName);

                TextBlock Adress = new TextBlock();
                Adress.Text = adresseRestaurant;
                Adress.HorizontalAlignment = HorizontalAlignment.Center;
                stackPanel.Children.Add(Adress);

                TextBlock ZipCode = new TextBlock();
                ZipCode.Text = "14000 Tiaret";
                ZipCode.HorizontalAlignment = HorizontalAlignment.Center;
                stackPanel.Children.Add(ZipCode);

                TextBlock PhoneNumber = new TextBlock();
                PhoneNumber.Text = "Tel : " + Telephone;
                PhoneNumber.HorizontalAlignment = HorizontalAlignment.Center;
                PhoneNumber.Margin = new Thickness(0, 2, 0, 10);
                stackPanel.Children.Add(PhoneNumber);
                {
                    Grid grid = new Grid();
                    ColumnDefinition cd = new ColumnDefinition() { Width = new GridLength(10, GridUnitType.Star) };
                    grid.ColumnDefinitions.Add(cd);
                    ColumnDefinition cd2 = new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Star) };
                    grid.ColumnDefinitions.Add(cd2);
                    ColumnDefinition cd3 = new ColumnDefinition() { Width = new GridLength(20, GridUnitType.Star) };
                    grid.ColumnDefinitions.Add(cd3);
                    ColumnDefinition cd4 = new ColumnDefinition() { Width = new GridLength(15, GridUnitType.Star) };
                    grid.ColumnDefinitions.Add(cd4);
                    ColumnDefinition cd5 = new ColumnDefinition() { Width = new GridLength(20, GridUnitType.Star) };
                    grid.ColumnDefinitions.Add(cd5);
                    ColumnDefinition cd6 = new ColumnDefinition() { Width = new GridLength(70, GridUnitType.Star) };
                    grid.ColumnDefinitions.Add(cd6);

                    grid.Margin = new Thickness(5, 10, 15, 5);
                    grid.HorizontalAlignment = HorizontalAlignment.Stretch;


                    TextBlock invoiceID = new TextBlock();
                    invoiceID.Text = "#";
                    invoiceID.FontWeight = FontWeights.Bold;
                    grid.Children.Add(invoiceID);
                    Grid.SetColumn(invoiceID, 0);

                    TextBlock itemName = new TextBlock();
                    itemName.Text = "Article";
                    itemName.FontWeight = FontWeights.Bold;
                    grid.Children.Add(itemName);
                    Grid.SetColumn(itemName, 1);

                    TextBlock UnitPrice = new TextBlock();
                    UnitPrice.Text = "Prix";
                    UnitPrice.FontWeight = FontWeights.Bold;
                    grid.Children.Add(UnitPrice);
                    Grid.SetColumn(UnitPrice, 2);

                    TextBlock amount = new TextBlock();
                    amount.Text = "Qte";
                    amount.FontWeight = FontWeights.Bold;
                    grid.Children.Add(amount);
                    Grid.SetColumn(amount, 3);

                    TextBlock totalPrice = new TextBlock();
                    totalPrice.Text = "TOT";
                    totalPrice.FontWeight = FontWeights.Bold;
                    grid.Children.Add(totalPrice);
                    Grid.SetColumn(totalPrice, 4);

                    TextBlock Date = new TextBlock();
                    Date.Text = "Date";
                    Date.FontWeight = FontWeights.Bold;
                    grid.Children.Add(Date);
                    Grid.SetColumn(Date, 5);

                    stackPanel.Children.Add(grid);

                    stackPanel.Children.Add(new Separator());
                }
                int index = 0;
                while ( index < ITEMS_PER_PAGE && (i * ITEMS_PER_PAGE) + index < vm.Sales.Count)
                {
                    Sale sale = vm.Sales[(i * ITEMS_PER_PAGE) + index ];

                    Border border = new Border();
                    border.Padding = new Thickness(10, 1, 5, 5);

                    Grid grid = new Grid();
                    ColumnDefinition cd = new ColumnDefinition() { Width = new GridLength(10, GridUnitType.Star) };
                    grid.ColumnDefinitions.Add(cd);
                    ColumnDefinition cd2 = new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Star) };
                    grid.ColumnDefinitions.Add(cd2);
                    ColumnDefinition cd3 = new ColumnDefinition() { Width = new GridLength(20, GridUnitType.Star) };
                    grid.ColumnDefinitions.Add(cd3);
                    ColumnDefinition cd4 = new ColumnDefinition() { Width = new GridLength(15, GridUnitType.Star) };
                    grid.ColumnDefinitions.Add(cd4);
                    ColumnDefinition cd5 = new ColumnDefinition() { Width = new GridLength(20, GridUnitType.Star) };
                    grid.ColumnDefinitions.Add(cd5);
                    ColumnDefinition cd6 = new ColumnDefinition() { Width = new GridLength(70, GridUnitType.Star) };
                    grid.ColumnDefinitions.Add(cd6);

                    //grid.Margin = new Thickness(10, 1, 5, 5);
                    grid.HorizontalAlignment = HorizontalAlignment.Stretch;
                    

                    TextBlock invoiceID = new TextBlock();
                    invoiceID.Text = sale.Invoice.InvoiceID + "";
                    grid.Children.Add(invoiceID);
                    Grid.SetColumn(invoiceID, 0);

                    TextBlock itemName = new TextBlock();
                    itemName.Text = sale.ItemName;
                    grid.Children.Add(itemName);
                    Grid.SetColumn(itemName, 1);

                    TextBlock UnitPrice = new TextBlock();
                    UnitPrice.Text = sale.UnitPrice + "";
                    grid.Children.Add(UnitPrice);
                    Grid.SetColumn(UnitPrice, 2);

                    TextBlock amount = new TextBlock();
                    amount.Text = sale.Amount + "";
                    grid.Children.Add(amount);
                    Grid.SetColumn(amount, 3);

                    TextBlock totalPrice = new TextBlock();
                    totalPrice.Text = sale.totalPrice + "";
                    grid.Children.Add(totalPrice);
                    Grid.SetColumn(totalPrice, 4);

                    TextBlock Date = new TextBlock();
                    Date.Text = sale.Invoice.InvoiceDate.ToString();
                    grid.Children.Add(Date);
                    Grid.SetColumn(Date, 5);

                    if (index % 2 == 1)
                    {
                        border.Background = new SolidColorBrush(Color.FromRgb(200, 180, 220));
                    }

                    border.Child = grid;
                    stackPanel.Children.Add(border);
                    stackPanel.Children.Add(new Separator());

                    index++;
                }
                if (i >= vm.Sales.Count / ITEMS_PER_PAGE)
                {
                    TextBlock totalPriceInvoice = new TextBlock();
                    totalPriceInvoice.Text = "TOTAL: " + vm.TotalSum + " DA";
                    totalPriceInvoice.HorizontalAlignment = HorizontalAlignment.Right;
                    totalPriceInvoice.FontWeight = FontWeights.Bold;
                    totalPriceInvoice.Margin = new Thickness(0, 20, 0, 25);
                    stackPanel.Children.Add(totalPriceInvoice);
                }

                // Create FixedPage
                var fixedPage = new FixedPage();
                fixedPage.Width = pageSize.Width;
                fixedPage.Height = pageSize.Height;
                // Add visual, measure/arrange page.
                fixedPage.Children.Add(stackPanel);
                fixedPage.Measure(pageSize);
                fixedPage.Arrange(new Rect(new Point(), pageSize));
                fixedPage.UpdateLayout();

                // Add page to document
                var pageContent = new PageContent();
                ((IAddChild)pageContent).AddChild(fixedPage);
                document.Pages.Add(pageContent);
            }
            // Send to the printer.
            var pd = new PrintDialog();
            pd.PrintDocument(document.DocumentPaginator, "Rapport de ventes");
        }
    }
}
