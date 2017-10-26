using Logic.ViewModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Ui
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        LoginViewModel vm = new LoginViewModel();
        string identityHP = "716CB92AF26EE983251B03F76C9470BA";
        string identityAcer = "973D74ECDA12B8F1225EB1744B888278";
        string identityDELL = "BCE3A330182F568222C90978F7F53433";
        string identityFinal = "3B39A1971AA9D2888045F9FC56FAD56F";



        public LoginWindow()
        {

            InitializeComponent();
            DataContext = vm;
            vm.OnRequestClose += ((s, e) =>
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            });

            string macAdress = GetMacAddress();
            string hash = GetHashString(macAdress);

            //string identity = identitydell;
            string identity = identityFinal;
            if (hash != identity)
            {
                var textblock  = new TextBlock();
                textblock.Text = "Contactez abder.dellal@gmail.com";
               // System.IO.File.WriteAllText(@".\Result.txt", hash);
                this.Content = textblock;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((LoginViewModel)this.DataContext).Password = ((PasswordBox)sender).Password; }
        }

        private static string GetMacAddress()
        {
            string macAddress = string.Empty;

            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            NetworkInterface nic = interfaces.Where(n => n.NetworkInterfaceType == NetworkInterfaceType.Ethernet).FirstOrDefault();
            return nic.GetPhysicalAddress().ToString();
        }


        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}

