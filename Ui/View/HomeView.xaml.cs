using Logic.Model;
using Logic.ViewModel;
using System.Windows.Controls;

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
                        if (myListView.SelectedItem != null && myListView.SelectedItem is Sale)
                            viewmodel.SelectSale((Sale)myListView.SelectedItem);
                    }
                }
            }
        }
    }
}
