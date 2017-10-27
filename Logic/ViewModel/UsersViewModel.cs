using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Logic.Model;

namespace Logic.ViewModel
{
    public class UsersViewModel : ViewModelBase
    {
        private string _newPassword = "";

        private string _newUserName = "";

        public UsersViewModel()
        {
            SaveCommand = new RelayCommand(Save, CanSave);

            using (var ctx = new FastFoodContext())
            {
                Users = ctx.Users.ToList();
            }
            if (Users.Count > 0)
            {
                SelectedUser = Users.First();
                NewUserName = SelectedUser.UserName;
            }

            SelectUserCommand = new RelayCommand<User>(user =>
            {
                SelectedUser = user;
                NewUserName = user.UserName;
                NewPassword = "";
                SaveCommand.RaiseCanExecuteChanged();
            });
        }

        public List<User> Users { get; set; }
        public User SelectedUser { get; set; }

        public string NewUserName
        {
            get { return _newUserName; }
            set
            {
                _newUserName = value;
                RaisePropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                RaisePropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand<User> SelectUserCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }

        private bool CanSave()
        {
            if (NewUserName != null && NewUserName.Length > 1
                && NewPassword != null && NewPassword.Length > 1)
                return true;
            return false;
        }

        private void Save()
        {
            SelectedUser.UserName = NewUserName;
            SelectedUser.MotDePass = NewPassword;

            using (var ctx = new FastFoodContext())
            {
                ctx.Entry(SelectedUser).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}