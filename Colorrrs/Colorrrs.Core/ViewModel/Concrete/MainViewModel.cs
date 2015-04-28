using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Colorrrs.Core.Helpers;
using Colorrrs.Core.Model;
using Colorrrs.Core.ViewModel.Abstract;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;

namespace Colorrrs.Core.ViewModel.Concrete
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        #region Fields

        private readonly Random _random = new Random();
        private readonly bool _darkTheme;

        #endregion


        #region Properties

        private bool _canRandomize = true;
        public bool CanRandomize
        {
            get { return _canRandomize; }
            set
            {
                _canRandomize = value;
                RaisePropertyChanged();
                ((RelayCommand)RandomizeColorCommand).RaiseCanExecuteChanged();
            }
        }
        
        private readonly Colorrr _currentColor = new Colorrr();
        public Colorrr CurrentColor { get { return _currentColor; } }

        public bool IsBrightness { get { return _currentColor.Brightness > 128f; } }

        private string _HEXText;
        public string HEXText
        {
            get
            {
                return _HEXText;
            }
            set
            {
                if (_HEXText != value)
                {
                    _HEXText = value;
                    RaisePropertyChanged();
                    Update();
                }
            }
        }

        private string _RGBText;
        public string RGBText
        {
            get
            {
                return _RGBText;
            }
            set
            {
                if (_RGBText != value)
                {
                    _RGBText = value;
                    RaisePropertyChanged();
                    Update();
                }
            }
        }

        #endregion


        #region Commands

        public ICommand RandomizeColorCommand { get; private set; }

        #endregion


        #region Constructor

        public MainViewModel()
        {
            RandomizeColorCommand = new RelayCommand(RandomizeColor, CanRandomizeColor);

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

        #endregion


        #region Command Methods

        private bool CanRandomizeColor()
        {
            return CanRandomize;
        }
        private void RandomizeColor()
        {
            CurrentColor.Red = (byte)_random.Next(0, 256);
            CurrentColor.Green = (byte)_random.Next(0, 256);
            CurrentColor.Blue = (byte)_random.Next(0, 256);

            Update();
        }

        #endregion


        #region Methods

        public void Update([CallerMemberName] string property = null)
        {
            if (property == "HEXText")
            {
                try
                {
                    CurrentColor.HexToColorrr(HEXText);
                    _RGBText = HEXText.HexToRgb();

                    RaisePropertyChanged("CurrentColor");
                    RaisePropertyChanged("IsBrightness");
                    RaisePropertyChanged("RGBText");
                }
                catch { }
            }
            else if (property == "RGBText")
            {
                try
                {
                    _HEXText = RGBText.RgbToHex();
                    CurrentColor.HexToColorrr(HEXText);

                    RaisePropertyChanged("CurrentColor");
                    RaisePropertyChanged("IsBrightness");
                    RaisePropertyChanged("HEXText");
                }
                catch { }
            }
            else
            {
                _HEXText = CurrentColor.ColorrrToHex();
                _RGBText = HEXText.HexToRgb();

                RaisePropertyChanged("CurrentColor");
                RaisePropertyChanged("IsBrightness");
                RaisePropertyChanged("HEXText");
                RaisePropertyChanged("RGBText");
            }
        }

        #endregion
    }
}