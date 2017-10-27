using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Logic.Model;

namespace Logic.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(ViewModelLocator locator)
        {
            Locator = locator;

            // Home view/viewModel will be selected at startup
            SelectedViewModel = locator.Home;

            ChangeViewCommand = new RelayCommand<ViewModelBase>(vm => { SelectedViewModel = vm; });
        }

        //the menu won't be displayed if the logged user type is simple
        public string DisplayMenu =>
            LoginViewModel.LoggedUser != null && LoginViewModel.LoggedUser.UserType == UserType.Admin
                ? "Visible"
                : "Collapsed";

        public string DisplayIconBar =>
            LoginViewModel.LoggedUser != null && LoginViewModel.LoggedUser.UserType == UserType.Admin
                ? "Collapsed"
                : "Visible";

        /// <summary>
        ///     View/ViewModel to be displayed in the main frame (the View will be selected according to the selected ViewModel)
        /// </summary>
        public ViewModelBase SelectedViewModel { get; set; }

        /// <summary>
        ///     the view model locaor
        /// </summary>
        public ViewModelLocator Locator { get; set; }

        /// <summary>
        ///     command to be called from the main window to change the selected View/ViewModel
        /// </summary>
        public RelayCommand<ViewModelBase> ChangeViewCommand { get; set; }
    }
}