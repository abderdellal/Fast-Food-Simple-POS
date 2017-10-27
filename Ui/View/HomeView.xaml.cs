using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows;
using System.Windows.Controls;
using Logic.Model;
using Logic.ViewModel;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace Ui.View
{
    /// <summary>
    ///     Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private readonly List<string> _itemList = new List<string>
        {
            "201", //fill from somewhere in your code
            "202"
        };

        private readonly PrintDocument _mPrintDocument = new PrintDocument();

        public HomeView()
        {
            InitializeComponent();

            _mPrintDocument.PrintPage += PrintPage;
        }

        //on selection change, set the selected sale in the view model
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is HomeViewModel)
            {
                var viewmodel = (HomeViewModel) DataContext;
                if (viewmodel != null)
                    if (myListView != null)
                        if (myListView.SelectedItem != null && myListView.SelectedItem is Sale)
                            viewmodel.SelectSale((Sale) myListView.SelectedItem);
            }
        }

        private void Print_Invoice(object sender, RoutedEventArgs e)
        {
            var nomRestaurant = "EL BEYLIK SHAWARMA"; //majuscule !
            var adresseRestaurant = "En face de l'arret Sougueur";
            var telephone = "0770746106";
            var telephone2 = "0798252738";

            var vm = (HomeViewModel) DataContext;

            var dialog = new PrintDialog();

            var myPanel = new StackPanel();
            myPanel.Margin = new Thickness(15, 0, 0, 15);
            myPanel.Width = 240;
            //System.Windows.Controls.Image Icon = new System.Windows.Controls.Image();
            //Icon.Width = 57;
            //Icon.Stretch = Stretch.Uniform;
            ////Icon.Source = new BitmapImage(new Uri("C:\\Logo.png", UriKind.Absolute));
            //Icon.Source = new BitmapImage(new Uri(".\\FastFoodIconReceipt.png", UriKind.Relative));
            //myPanel.Children.Add(Icon);

            var fastFoodName = new TextBlock();
            fastFoodName.Text = nomRestaurant;
            fastFoodName.HorizontalAlignment = HorizontalAlignment.Center;
            myPanel.Children.Add(fastFoodName);

            var adress = new TextBlock();
            adress.Text = adresseRestaurant;
            adress.HorizontalAlignment = HorizontalAlignment.Center;
            myPanel.Children.Add(adress);

            //TextBlock ZipCode = new TextBlock();
            //ZipCode.Text = "14000 Tiaret";
            //ZipCode.HorizontalAlignment = HorizontalAlignment.Center;
            //myPanel.Children.Add(ZipCode);

            var phoneNumber = new TextBlock();
            phoneNumber.Text = "Tel : " + telephone;
            phoneNumber.HorizontalAlignment = HorizontalAlignment.Center;
            //PhoneNumber.Margin = new Thickness(0, 2, 0, 10);
            myPanel.Children.Add(phoneNumber);

            var phoneNumber2 = new TextBlock();
            phoneNumber2.Text = "Tel : " + telephone2;
            phoneNumber2.HorizontalAlignment = HorizontalAlignment.Center;
            phoneNumber2.Margin = new Thickness(0, 2, 0, 10);
            myPanel.Children.Add(phoneNumber2);

            {
                var grid = new Grid();
                var cd = new ColumnDefinition {Width = new GridLength(20, GridUnitType.Star)};
                grid.ColumnDefinitions.Add(cd);
                var cd2 = new ColumnDefinition {Width = new GridLength(60, GridUnitType.Star)};
                grid.ColumnDefinitions.Add(cd2);
                var cd3 = new ColumnDefinition {Width = new GridLength(20, GridUnitType.Star)};
                grid.ColumnDefinitions.Add(cd3);
                grid.Margin = new Thickness(10, 1, 0, 5);

                var amount = new TextBlock();
                amount.Text = "Qte";
                amount.FontWeight = FontWeights.Bold;
                grid.Children.Add(amount);
                Grid.SetColumn(amount, 0);

                var itemName = new TextBlock();
                itemName.Text = "Article";
                itemName.FontWeight = FontWeights.Bold;
                grid.Children.Add(itemName);
                Grid.SetColumn(itemName, 1);

                var totalPrice = new TextBlock();
                totalPrice.Text = "TOT(DA)";
                totalPrice.FontWeight = FontWeights.Bold;
                grid.Children.Add(totalPrice);
                Grid.SetColumn(totalPrice, 2);

                myPanel.Children.Add(grid);
                myPanel.Children.Add(new Separator());
            }

            foreach (var sale in vm.Sales)
            {
                var grid = new Grid();
                var cd = new ColumnDefinition {Width = new GridLength(20, GridUnitType.Star)};
                grid.ColumnDefinitions.Add(cd);
                var cd2 = new ColumnDefinition {Width = new GridLength(60, GridUnitType.Star)};
                grid.ColumnDefinitions.Add(cd2);
                var cd3 = new ColumnDefinition {Width = new GridLength(20, GridUnitType.Star)};
                grid.ColumnDefinitions.Add(cd3);
                grid.Margin = new Thickness(10, 1, 0, 0);

                var amount = new TextBlock();
                amount.Text = sale.Amount + "";
                grid.Children.Add(amount);
                Grid.SetColumn(amount, 0);

                var itemName = new TextBlock();
                itemName.Text = sale.ItemName;
                grid.Children.Add(itemName);
                Grid.SetColumn(itemName, 1);


                var totalPrice = new TextBlock();
                totalPrice.Text = sale.TotalPrice + "";
                grid.Children.Add(totalPrice);
                Grid.SetColumn(totalPrice, 2);

                myPanel.Children.Add(grid);
                myPanel.Children.Add(new Separator());
            }

            var totalPriceInvoice = new TextBlock();
            totalPriceInvoice.Text = "TOTAL: " + vm.Total + " DA";
            totalPriceInvoice.HorizontalAlignment = HorizontalAlignment.Right;
            totalPriceInvoice.FontWeight = FontWeights.Bold;
            totalPriceInvoice.Margin = new Thickness(0, 5, 0, 10);
            myPanel.Children.Add(totalPriceInvoice);

            myPanel.Children.Add(new Separator());

            var welcom = new TextBlock();
            welcom.Text = nomRestaurant;
            welcom.HorizontalAlignment = HorizontalAlignment.Center;
            myPanel.Children.Add(welcom);

            var welcom2 = new TextBlock();
            welcom2.Text = " VOUS SOUHAITE LA BIEN VENUE";
            welcom2.HorizontalAlignment = HorizontalAlignment.Center;
            myPanel.Children.Add(welcom2);

            myPanel.Measure(new Size(dialog.PrintableAreaWidth,
                dialog.PrintableAreaHeight));
            myPanel.Arrange(new Rect(new Point(0, 0),
                myPanel.DesiredSize));

            dialog.PrintVisual(myPanel, "A Great Image.");
        }

        private void Print_Invoice2(object sender, RoutedEventArgs e)
        {
            _mPrintDocument.Print();
        }

        public void PrintPage(object sender, PrintPageEventArgs e)
        {
            var graphics = e.Graphics;

            var regular = new Font(System.Drawing.FontFamily.GenericSansSerif, 10.0f, System.Drawing.FontStyle.Regular);
            var bold = new Font(System.Drawing.FontFamily.GenericSansSerif, 10.0f, System.Drawing.FontStyle.Bold);

            //print header
            graphics.DrawString("FERREIRA MATERIALS PARA CONSTRUCAO LTDA", bold, Brushes.Black, 20, 10);
            graphics.DrawString("EST ENGENHEIRO MARCILAC, 116, SAO PAOLO - SP", regular, Brushes.Black, 30, 30);
            graphics.DrawString("Telefone: (11)5921-3826", regular, Brushes.Black, 110, 50);
            graphics.DrawLine(Pens.Black, 80, 70, 320, 70);
            graphics.DrawString("CUPOM NAO FISCAL", bold, Brushes.Black, 110, 80);
            graphics.DrawLine(Pens.Black, 80, 100, 320, 100);

            //print items
            graphics.DrawString("COD | DESCRICAO                      | QTY | X | Vir Unit | Vir Total |", bold,
                Brushes.Black, 10, 120);
            graphics.DrawLine(Pens.Black, 10, 140, 430, 140);

            for (var i = 0; i < _itemList.Count; i++)
                graphics.DrawString(_itemList[i], regular, Brushes.Black, 20, 150 + i * 20);

            //print footer
            //...

            regular.Dispose();
            bold.Dispose();

            // Check to see if more pages are to be printed.
            e.HasMorePages = _itemList.Count > 20;
        }
    }
}