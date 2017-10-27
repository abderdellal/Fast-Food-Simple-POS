/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Ui"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Logic.ViewModel
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        ///     Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<AddItemViewModel>();
            SimpleIoc.Default.Register<SalesHistoryViewModel>();
            SimpleIoc.Default.Register<ItemsViewModel>();
            SimpleIoc.Default.Register<UsersViewModel>();
            SimpleIoc.Default.Register(() => new MainViewModel(this));
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public HomeViewModel Home => ServiceLocator.Current.GetInstance<HomeViewModel>();

        public AddItemViewModel AddItem => ServiceLocator.Current.GetInstance<AddItemViewModel>();

        public SalesHistoryViewModel SalesHistory => ServiceLocator.Current.GetInstance<SalesHistoryViewModel>();

        public ItemsViewModel Items => ServiceLocator.Current.GetInstance<ItemsViewModel>();

        public UsersViewModel Users => ServiceLocator.Current.GetInstance<UsersViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}