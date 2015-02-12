using System;
using System.Windows.Input;
using Colorrrs.Core.Model;
using Colorrrs.Core.ViewModel.Abstract;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;

namespace Colorrrs.Core.ViewModel.Concrete
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private readonly Random _random = new Random();
        private readonly bool _darkTheme;

        private readonly Colorrr _currentColor = new Colorrr();
        public Colorrr CurrentColor { get { return _currentColor; } }

        public bool IsBrightness { get { return _currentColor.Brightness > 128f; } }

        private string _HEXText;
        public string HEXText { get { return _HEXText; } set { _HEXText = value; RaisePropertyChanged(); } }

        private string _RGBText;
        public string RGBText { get { return _RGBText; } set { _RGBText = value; RaisePropertyChanged(); } }


        public ICommand RandomizeColorCommand { get; private set; }


        public MainViewModel()
        {
            RandomizeColorCommand = new RelayCommand(RandomizeColor);


            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                _currentColor.Red = 124;
                _currentColor.Green = 200;
                _currentColor.Blue = 142;

                Update();
            }
            else
            {
                // Code runs "for real"

                if (ServiceLocator.IsLocationProviderSet)
                    _darkTheme = ServiceLocator.Current.GetInstance<Theme>().IsDarkTheme;

                if (_darkTheme)
                {
                    _currentColor.Red = 0;
                    _currentColor.Green = 0;
                    _currentColor.Blue = 0;
                }
                else
                {
                    _currentColor.Red = 255;
                    _currentColor.Green = 255;
                    _currentColor.Blue = 255;
                }

                Update();
            }
        }


        private void RandomizeColor()
        {
            CurrentColor.Red = (byte)_random.Next(0, 256);
            CurrentColor.Green = (byte)_random.Next(0, 256);
            CurrentColor.Blue = (byte)_random.Next(0, 256);

            Update();
        }

        private void Update()
        {
            RaisePropertyChanged("CurrentColor");
            RaisePropertyChanged("IsBrightness");
            RaisePropertyChanged("HEXText");
            RaisePropertyChanged("RGBText");
        }
    }
}