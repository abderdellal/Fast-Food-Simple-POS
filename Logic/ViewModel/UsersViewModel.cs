using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ViewModel
{
    public class UsersViewModel : ViewModelBase
    {
        public List<User> users { get; set; }
        public User SelectedUser { get; set; }

        private string _NewUserName = "";
        public string NewUserName
        {
            get
            {
                return _NewUserName;
            }
            set
            {
                _NewUserName = value;
                RaisePropertyChanged("NewUserName");
                saveCommand.RaiseCanExecuteChanged();
            }
        }

        private string _NewPassword = "";
        public string NewPassword {
            get
            {
               return  _NewPassword;
            }
            set
            {
                _NewPassword = value;
                RaisePropertyChanged("NewPassword");
                saveCommand.RaiseCanExecuteChanged();
            }
        }
        public RelayCommand<User> SelectUserCommand { get; set; }
        public RelayCommand saveCommand { get; set; }

        public UsersViewModel()
        {
            saveCommand = new RelayCommand(Save, CanSave);

            using (var ctx = new FastFoodContext())
            {
                users = ctx.Users.ToList();
            }
            if (users.Count > 0)
            {
                SelectedUser = users.First();
                NewUserName = SelectedUser.UserName;
            }

            SelectUserCommand = new RelayCommand<User>(user =>
            {
                SelectedUser = user;
                NewUserName = user.UserName;
                NewPassword = "";
                saveCommand.RaiseCanExecuteChanged();
            });

            //SelectedUser.PropertyChanged += (x, y) =>
            //{
            //    saveCommand.RaiseCanExecuteChanged();
            //};
        }

        private bool CanSave()
        {
            if (NewUserName != null && NewUserName.Length > 1
                && NewPassword != null && NewPassword.Length > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Save()
        {
            SelectedUser.UserName = NewUserName;
            SelectedUser.MotDePass = NewPassword;

            using (var ctx = new FastFoodContext())
            {
                ctx.Entry(SelectedUser).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

    }
}
