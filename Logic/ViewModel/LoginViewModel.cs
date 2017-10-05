using GalaSoft.MvvmLight.Command;
using Logic.Model;
using System;
using System.Linq;
using Logic.Properties;
using GalaSoft.MvvmLight;

namespace Logic.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {

        public static User LoggedUser;
        public event EventHandler OnRequestClose;
        public RelayCommand LoginCommand { get; set; }
        public string ErrorMessage { get; set; }

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(() =>
            {
                using (var ctx = new FastFoodContext())
                {
                    User user = ctx.Users.Where(u => u.UserName == UserName).FirstOrDefault();
                    if(user != null)
                    {
                        if(user.MotDePass == Password)
                        {
                            LoggedUser = user;
                            OnRequestClose(this, new EventArgs());
                        }
                        else
                        {
                            ErrorMessage = Resources.Wrong_Password;
                        }
                    }
                    else
                    {
                        ErrorMessage = Resources.Wrong_UserName;
                    }
                }
            },
            () => UserName != null && UserName.Length > 0 && Password != null && Password.Length > 0);
        }
    }
}
