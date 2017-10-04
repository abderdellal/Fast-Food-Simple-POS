using Logic.Model;
using Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                        if (myListView.SelectedItem != null)
                            viewmodel.SelectedSale = (Sale)myListView.SelectedItem;
                        else
                            viewmodel.SelectedSale = null;
                    }
                }
            }
        }
    }
}
