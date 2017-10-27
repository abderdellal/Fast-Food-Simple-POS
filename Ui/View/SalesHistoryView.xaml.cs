using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using Logic.ViewModel;

namespace Ui.View
{
    /// <summary>
    ///     Interaction logic for SalesHistoryView.xaml
    /// </summary>
    public partial class SalesHistoryView : UserControl
    {
        public SalesHistoryView()
        {
            var ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
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
                var minDate = viewModel.MinDate;
                var maxDate = viewModel.MaxDate;

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
                var date = picker.SelectedDate;

                if (date != null)
                    viewModel.MinDate = (DateTime) date;
            }
        }

        private void maxDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext != null && DataContext is SalesHistoryViewModel)
            {
                var viewModel = DataContext as SalesHistoryViewModel;

                var picker = sender as DatePicker;
                var date = picker.SelectedDate;

                if (date != null)
                    viewModel.MaxDate = (DateTime) date;
            }
        }

        private void Print_Button_Click(object sender, RoutedEventArgs e)
        {
            var pd = new PrintDialog();
            if (pd.ShowDialog() != true)
                return;

            var nomRestaurant = "EL BEYLIK SHAWARMA"; //majuscule !
            //string adresseRestaurant = "Adresse du Restaurant";
            //string Telephone = "0123456789";
            const int itemsPerPage = 32;

            //get the view model
            var vm = (SalesHistoryViewModel) DataContext;

            var pageSize = new Size(8.26 * 96, 11.69 * 96); // A4 page, at 96 dpi
            var document = new FixedDocument();
            document.DocumentPaginator.PageSize = pageSize;

            for (var i = 0; i < vm.Sales.Count / itemsPerPage + 1; i++)
            {
                var stackPanel = new StackPanel();
                stackPanel.Margin = new Thickness(15, 15, 15, 15);
                stackPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
                stackPanel.Width = 8 * 94;

                var fastFoodName = new TextBlock();
                fastFoodName.Text = nomRestaurant;
                fastFoodName.HorizontalAlignment = HorizontalAlignment.Center;
                stackPanel.Children.Add(fastFoodName);

                //TextBlock Adress = new TextBlock();
                //Adress.Text = adresseRestaurant;
                //Adress.HorizontalAlignment = HorizontalAlignment.Center;
                //stackPanel.Children.Add(Adress);

                //TextBlock ZipCode = new TextBlock();
                //ZipCode.Text = "14000 Tiaret";
                //ZipCode.HorizontalAlignment = HorizontalAlignment.Center;
                //stackPanel.Children.Add(ZipCode);

                //TextBlock PhoneNumber = new TextBlock();
                //PhoneNumber.Text = "Tel : " + Telephone;
                //PhoneNumber.HorizontalAlignment = HorizontalAlignment.Center;
                //PhoneNumber.Margin = new Thickness(0, 2, 0, 10);
                //stackPanel.Children.Add(PhoneNumber);
                {
                    var grid = new Grid();
                    var cd = new ColumnDefinition {Width = new GridLength(10, GridUnitType.Star)};
                    grid.ColumnDefinitions.Add(cd);
                    var cd2 = new ColumnDefinition {Width = new GridLength(50, GridUnitType.Star)};
                    grid.ColumnDefinitions.Add(cd2);
                    var cd3 = new ColumnDefinition {Width = new GridLength(20, GridUnitType.Star)};
                    grid.ColumnDefinitions.Add(cd3);
                    var cd4 = new ColumnDefinition {Width = new GridLength(15, GridUnitType.Star)};
                    grid.ColumnDefinitions.Add(cd4);
                    var cd5 = new ColumnDefinition {Width = new GridLength(20, GridUnitType.Star)};
                    grid.ColumnDefinitions.Add(cd5);
                    var cd6 = new ColumnDefinition {Width = new GridLength(70, GridUnitType.Star)};
                    grid.ColumnDefinitions.Add(cd6);

                    grid.Margin = new Thickness(5, 10, 15, 5);
                    grid.HorizontalAlignment = HorizontalAlignment.Stretch;


                    var invoiceId = new TextBlock();
                    invoiceId.Text = "#";
                    invoiceId.FontWeight = FontWeights.Bold;
                    grid.Children.Add(invoiceId);
                    Grid.SetColumn(invoiceId, 0);

                    var itemName = new TextBlock();
                    itemName.Text = "Article";
                    itemName.FontWeight = FontWeights.Bold;
                    grid.Children.Add(itemName);
                    Grid.SetColumn(itemName, 1);

                    var unitPrice = new TextBlock();
                    unitPrice.Text = "Prix";
                    unitPrice.FontWeight = FontWeights.Bold;
                    grid.Children.Add(unitPrice);
                    Grid.SetColumn(unitPrice, 2);

                    var amount = new TextBlock();
                    amount.Text = "Qte";
                    amount.FontWeight = FontWeights.Bold;
                    grid.Children.Add(amount);
                    Grid.SetColumn(amount, 3);

                    var totalPrice = new TextBlock();
                    totalPrice.Text = "TOT";
                    totalPrice.FontWeight = FontWeights.Bold;
                    grid.Children.Add(totalPrice);
                    Grid.SetColumn(totalPrice, 4);

                    var date = new TextBlock();
                    date.Text = "Date";
                    date.FontWeight = FontWeights.Bold;
                    grid.Children.Add(date);
                    Grid.SetColumn(date, 5);

                    stackPanel.Children.Add(grid);

                    stackPanel.Children.Add(new Separator());
                }
                var index = 0;
                while (index < itemsPerPage && i * itemsPerPage + index < vm.Sales.Count)
                {
                    var sale = vm.Sales[i * itemsPerPage + index];

                    var border = new Border();
                    border.Padding = new Thickness(10, 1, 5, 5);

                    var grid = new Grid();
                    var cd = new ColumnDefinition {Width = new GridLength(10, GridUnitType.Star)};
                    grid.ColumnDefinitions.Add(cd);
                    var cd2 = new ColumnDefinition {Width = new GridLength(50, GridUnitType.Star)};
                    grid.ColumnDefinitions.Add(cd2);
                    var cd3 = new ColumnDefinition {Width = new GridLength(20, GridUnitType.Star)};
                    grid.ColumnDefinitions.Add(cd3);
                    var cd4 = new ColumnDefinition {Width = new GridLength(15, GridUnitType.Star)};
                    grid.ColumnDefinitions.Add(cd4);
                    var cd5 = new ColumnDefinition {Width = new GridLength(20, GridUnitType.Star)};
                    grid.ColumnDefinitions.Add(cd5);
                    var cd6 = new ColumnDefinition {Width = new GridLength(70, GridUnitType.Star)};
                    grid.ColumnDefinitions.Add(cd6);

                    //grid.Margin = new Thickness(10, 1, 5, 5);
                    grid.HorizontalAlignment = HorizontalAlignment.Stretch;


                    var invoiceId = new TextBlock();
                    invoiceId.Text = sale.Invoice.InvoiceId + "";
                    grid.Children.Add(invoiceId);
                    Grid.SetColumn(invoiceId, 0);

                    var itemName = new TextBlock();
                    itemName.Text = sale.ItemName;
                    grid.Children.Add(itemName);
                    Grid.SetColumn(itemName, 1);

                    var unitPrice = new TextBlock();
                    unitPrice.Text = sale.UnitPrice + "";
                    grid.Children.Add(unitPrice);
                    Grid.SetColumn(unitPrice, 2);

                    var amount = new TextBlock();
                    amount.Text = sale.Amount + "";
                    grid.Children.Add(amount);
                    Grid.SetColumn(amount, 3);

                    var totalPrice = new TextBlock();
                    totalPrice.Text = sale.TotalPrice + "";
                    grid.Children.Add(totalPrice);
                    Grid.SetColumn(totalPrice, 4);

                    var date = new TextBlock();
                    date.Text = sale.Invoice.InvoiceDate.ToString();
                    grid.Children.Add(date);
                    Grid.SetColumn(date, 5);

                    if (index % 2 == 1)
                        border.Background = new SolidColorBrush(Color.FromRgb(200, 180, 220));

                    border.Child = grid;
                    stackPanel.Children.Add(border);
                    stackPanel.Children.Add(new Separator());

                    index++;
                }
                if (i >= vm.Sales.Count / itemsPerPage)
                {
                    var totalPriceInvoice = new TextBlock();
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
                ((IAddChild) pageContent).AddChild(fixedPage);
                document.Pages.Add(pageContent);
            }
            // Send to the printer.
            pd.PrintDocument(document.DocumentPaginator, "Rapport de ventes");
        }
    }
}