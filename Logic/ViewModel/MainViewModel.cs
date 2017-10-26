using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Logic.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public string DisplayMenu
        {
            get
            {
                return (LoginViewModel.LoggedUser != null && LoginViewModel.LoggedUser.UserType == Model.typeUser.Admin)? "Visible" : "Collapsed";
            }
        }
        public string DisplayIconBar
        {
            get
            {
                return (LoginViewModel.LoggedUser != null && LoginViewModel.LoggedUser.UserType == Model.typeUser.Admin) ? "Collapsed" : "Visible";
            }
        }
        /// <summary>
        /// View/ViewModel to be displayed in the main frame (the View will be selected according to the selected ViewModel)
        /// </summary>
        public ViewModelBase SelectedViewModel { get; set; }

        /// <summary>
        /// the view model locaor
        /// </summary>
        public ViewModelLocator locator { get; set; }

        /// <summary>
        /// command to be called from the main window to change the selected View/ViewModel
        /// </summary>
        public RelayCommand<ViewModelBase> changeViewCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(ViewModelLocator locator)
        {
            this.locator = locator;

            //Home view/viewModel will be selected at startup
            SelectedViewModel = locator.Home;

            changeViewCommand = new RelayCommand<ViewModelBase>(vm =>
                                                                    {
                                                                        SelectedViewModel = vm;
                                                                    });
        }
    }
}