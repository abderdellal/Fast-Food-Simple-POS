using System.Windows;
using System.Windows.Controls;
using Logic.ViewModel;

namespace Ui
{
    /// <summary>
    ///     Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _vm = new LoginViewModel();

        public LoginWindow()
        {
            InitializeComponent();
            DataContext = _vm;
            _vm.OnRequestClose += (s, e) =>
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            };
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
                ((LoginViewModel) DataContext).Password = ((PasswordBox) sender).Password;
        }
    }
}