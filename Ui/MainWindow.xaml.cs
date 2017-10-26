using System.Windows;

namespace Ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //WindowState = WindowState.Maximized;
            InitializeComponent();
        }

        //click on Exit Menu Item
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
