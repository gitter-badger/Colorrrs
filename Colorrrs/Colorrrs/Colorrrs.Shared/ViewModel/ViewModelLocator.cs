/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Colorrrs"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using Windows.UI.Xaml;
using Colorrrs.Core.Model;
using Colorrrs.Core.Services;
using Colorrrs.Core.ViewModel.Abstract;
using Colorrrs.Core.ViewModel.Concrete;
using Colorrrs.Services;
using Colorrrs.Views;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace Colorrrs.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        #region Constructor

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

            // Model
            if (!SimpleIoc.Default.IsRegistered<Theme>())
            {
                SimpleIoc.Default.Register<Theme>(() => new Theme
                {
                    IsDarkTheme = Application.Current.RequestedTheme == ApplicationTheme.Dark
                });
            }

            // Services
            if (!SimpleIoc.Default.IsRegistered<INavigationService>())
            {
                var navigationService = CreateNavigationService();
                SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            }

            SimpleIoc.Default.Register<ILocalSettingsService, LocalSettingsService>();
            SimpleIoc.Default.Register<IColorPalletService, ColorPalletService>();

            // ViewModels
            SimpleIoc.Default.Register<IMainViewModel, MainViewModel>();
        }

        #endregion


        #region Navigation Service (Page declaration)

        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            
            navigationService.Configure("Main", typeof(MainPage));
            navigationService.Configure("SelectColor", typeof(SelectColorPage));

            return navigationService;
        }

        #endregion


        #region ViewModels

        public IMainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IMainViewModel>();
            }
        }

        #endregion
    }
}