using Logic.Model;
using Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Printing;

namespace Ui.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();

            m_printDocument.PrintPage += new PrintPageEventHandler(printPage);

        }

        //on selection change, set the selected sale in the view model
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is HomeViewModel)
            {
                HomeViewModel viewmodel = (HomeViewModel)DataContext;
                if (viewmodel != null)
                {
                    if (myListView != null)
                    {
                        if (myListView.SelectedItem != null && myListView.SelectedItem is Sale)
                            viewmodel.SelectSale((Sale)myListView.SelectedItem);
                    }
                }
            }
        }

        private void Print_Invoice(object sender, System.Windows.RoutedEventArgs e)
        {
            string nomRestaurant = "NOM DU RESTAURANT"; //majuscule !
            string adresseRestaurant = "Adresse du Restaurant";
            string Telephone = "0123456789";

            HomeViewModel vm = (HomeViewModel)DataContext;

            PrintDialog dialog = new PrintDialog();

            StackPanel myPanel = new StackPanel();
            myPanel.Margin = new Thickness(15, 0, 15, 15);

            System.Windows.Controls.Image Icon = new System.Windows.Controls.Image();
            Icon.Width = 228;
            Icon.Stretch = Stretch.Uniform;
            Icon.Source = new BitmapImage(new Uri("C:\\Logo.png", UriKind.Absolute));
            myPanel.Children.Add(Icon);

            TextBlock FastFoodName = new TextBlock();
            FastFoodName.Text = nomRestaurant;
            FastFoodName.HorizontalAlignment = HorizontalAlignment.Center;
            myPanel.Children.Add(FastFoodName);

            TextBlock Adress = new TextBlock();
            Adress.Text = adresseRestaurant;
            Adress.HorizontalAlignment = HorizontalAlignment.Center;
            myPanel.Children.Add(Adress);

            TextBlock ZipCode = new TextBlock();
            ZipCode.Text = "14000 Tiaret";
            ZipCode.HorizontalAlignment = HorizontalAlignment.Center;
            myPanel.Children.Add(ZipCode);

            TextBlock PhoneNumber = new TextBlock();
            PhoneNumber.Text = "Tel : " + Telephone;
            PhoneNumber.HorizontalAlignment = HorizontalAlignment.Center;
            PhoneNumber.Margin = new Thickness(0, 2, 0, 10);
            myPanel.Children.Add(PhoneNumber);

            {
                Grid grid = new Grid();
                ColumnDefinition cd = new ColumnDefinition() { Width = new GridLength(20, GridUnitType.Star) };
                grid.ColumnDefinitions.Add(cd);
                ColumnDefinition cd2 = new ColumnDefinition() { Width = new GridLength(60, GridUnitType.Star) };
                grid.ColumnDefinitions.Add(cd2);
                ColumnDefinition cd3 = new ColumnDefinition() { Width = new GridLength(20, GridUnitType.Star) };
                grid.ColumnDefinitions.Add(cd3);
                grid.Margin = new Thickness(10, 1, 5, 5);

                TextBlock amount = new TextBlock();
                amount.Text = "Qte";
                amount.FontWeight = FontWeights.Bold;
                grid.Children.Add(amount);
                Grid.SetColumn(amount, 0);

                TextBlock itemName = new TextBlock();
                itemName.Text = "Article";
                itemName.FontWeight = FontWeights.Bold;
                grid.Children.Add(itemName);
                Grid.SetColumn(itemName, 1);

                TextBlock totalPrice = new TextBlock();
                totalPrice.Text = "TOT(DA)";
                totalPrice.FontWeight = FontWeights.Bold;
                grid.Children.Add(totalPrice);
                Grid.SetColumn(totalPrice, 2);

                myPanel.Children.Add(grid);
                myPanel.Children.Add(new Separator());

            }

            foreach (Sale sale in vm.Sales)
            {
                Grid grid = new Grid();
                ColumnDefinition cd = new ColumnDefinition() { Width = new GridLength(20, GridUnitType.Star) };
                grid.ColumnDefinitions.Add(cd);
                ColumnDefinition cd2 = new ColumnDefinition() { Width = new GridLength(60, GridUnitType.Star) };
                grid.ColumnDefinitions.Add(cd2);
                ColumnDefinition cd3 = new ColumnDefinition() { Width = new GridLength(20, GridUnitType.Star) };
                grid.ColumnDefinitions.Add(cd3);
                grid.Margin = new Thickness(10, 1, 5, 0);

                TextBlock amount = new TextBlock();
                amount.Text = sale.Amount + "";
                grid.Children.Add(amount);
                Grid.SetColumn(amount, 0);

                TextBlock itemName = new TextBlock();
                itemName.Text = sale.ItemName;
                grid.Children.Add(itemName);
                Grid.SetColumn(itemName, 1);



                TextBlock totalPrice = new TextBlock();
                totalPrice.Text = sale.totalPrice + "";
                grid.Children.Add(totalPrice);
                Grid.SetColumn(totalPrice, 2);

                myPanel.Children.Add(grid);
                myPanel.Children.Add(new Separator());
            }

            TextBlock totalPriceInvoice = new TextBlock();
            totalPriceInvoice.Text = "TOTAL: " + vm.Total + " DA";
            totalPriceInvoice.HorizontalAlignment = HorizontalAlignment.Right;
            totalPriceInvoice.FontWeight = FontWeights.Bold;
            totalPriceInvoice.Margin = new Thickness(0, 5, 0, 10);
            myPanel.Children.Add(totalPriceInvoice);

            myPanel.Children.Add(new Separator());

            TextBlock welcom = new TextBlock();
            welcom.Text = nomRestaurant;
            welcom.HorizontalAlignment = HorizontalAlignment.Center;
            myPanel.Children.Add(welcom);

            TextBlock welcom2 = new TextBlock();
            welcom2.Text = " VOUS SOUHAITE LA BIEN VENUE";
            welcom2.HorizontalAlignment = HorizontalAlignment.Center;
            myPanel.Children.Add(welcom2);

            myPanel.Measure(new System.Windows.Size(dialog.PrintableAreaWidth,
              dialog.PrintableAreaHeight));
            myPanel.Arrange(new Rect(new System.Windows.Point(0, 0),
              myPanel.DesiredSize));

            dialog.PrintVisual(myPanel, "A Great Image.");

        }

        List<string> itemList = new List<string>()
{
    "201", //fill from somewhere in your code
    "202"
};

        PrintDocument m_printDocument = new PrintDocument();

        private void Print_Invoice2(object sender, System.Windows.RoutedEventArgs e)
        {
            m_printDocument.Print();
        }

        public void printPage(object sender, PrintPageEventArgs e)
        {

            Graphics graphics = e.Graphics;

            Font regular = new Font(System.Drawing.FontFamily.GenericSansSerif, 10.0f, System.Drawing.FontStyle.Regular);
            Font bold = new Font(System.Drawing.FontFamily.GenericSansSerif, 10.0f, System.Drawing.FontStyle.Bold);

            //print header
            graphics.DrawString("FERREIRA MATERIALS PARA CONSTRUCAO LTDA", bold, System.Drawing.Brushes.Black, 20, 10);
            graphics.DrawString("EST ENGENHEIRO MARCILAC, 116, SAO PAOLO - SP", regular, System.Drawing.Brushes.Black, 30, 30);
            graphics.DrawString("Telefone: (11)5921-3826", regular, System.Drawing.Brushes.Black, 110, 50);
            graphics.DrawLine(Pens.Black, 80, 70, 320, 70);
            graphics.DrawString("CUPOM NAO FISCAL", bold, System.Drawing.Brushes.Black, 110, 80);
            graphics.DrawLine(Pens.Black, 80, 100, 320, 100);

            //print items
            graphics.DrawString("COD | DESCRICAO                      | QTY | X | Vir Unit | Vir Total |", bold, System.Drawing.Brushes.Black, 10, 120);
            graphics.DrawLine(Pens.Black, 10, 140, 430, 140);

            for (int i = 0; i < itemList.Count; i++)
            {
                graphics.DrawString(itemList[i].ToString(), regular, System.Drawing.Brushes.Black, 20, 150 + i * 20);
            }

            //print footer
            //...

            regular.Dispose();
            bold.Dispose();

            // Check to see if more pages are to be printed.
            e.HasMorePages = (itemList.Count > 20);
        }
    }
}
