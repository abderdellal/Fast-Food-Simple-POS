using System;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Logic.Model;
using Logic.Properties;

namespace Logic.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public static User LoggedUser;

        private string _password;

        private string _userName;

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(() =>
                {
                    using (var ctx = new FastFoodContext())
                    {
                        var user = ctx.Users.Where(u => u.UserName == UserName).FirstOrDefault();
                        if (user != null)
                            if (user.MotDePass == Password)
                            {
                                LoggedUser = user;
                                OnRequestClose(this, new EventArgs());
                            }
                            else
                            {
                                ErrorMessage = Resources.Wrong_Password;
                            }
                        else
                            ErrorMessage = Resources.Wrong_UserName;
                    }
                },
                () => UserName != null && UserName.Length > 0 && Password != null && Password.Length > 0);
        }

        public RelayCommand LoginCommand { get; set; }
        public string ErrorMessage { get; set; }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public event EventHandler OnRequestClose;
    }
}