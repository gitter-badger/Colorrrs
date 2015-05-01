using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Colorrrs.Core.Helpers;
using Colorrrs.Core.Model;
using Colorrrs.Core.Services;
using Colorrrs.Core.ViewModel.Abstract;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace Colorrrs.Core.ViewModel.Concrete
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        #region Services

        private readonly ILocalSettingsService _localSettingsService;
        private readonly IColorPalletService _colorPalletService;
        private readonly INavigationService _navigationService;

        #endregion


        #region Fields

        private readonly Random _random = new Random();
        private readonly bool _darkTheme;
        private readonly Dictionary<string, Colorrr> _colors;

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

        private string _colorName;
        public string ColorName
        {
            get { return _colorName; }
            set
            {
                if (_colorName != value)
                {
                    _colorName = value;
                    RaisePropertyChanged();
                    Update();
                }
            }
        }

        private readonly IEnumerable<string> _colorNames;
        public IEnumerable<string> ColorNames { get { return _colorNames; } }

        #endregion


        #region Commands

        public ICommand RandomizeColorCommand { get; private set; }
        public ICommand GoToColorSelectionCommand { get; private set; }
        public ICommand SelectColorCommand { get; private set; }

        #endregion


        #region Constructor

        public MainViewModel(ILocalSettingsService localSettingsService,
            IColorPalletService colorPalletService,
            INavigationService navigationService)
        {
            // Retrieve Services
            _localSettingsService = localSettingsService;
            _colorPalletService = colorPalletService;
            _navigationService = navigationService;

            // Create Commands
            RandomizeColorCommand = new RelayCommand(RandomizeColor, CanRandomizeColor);
            GoToColorSelectionCommand = new RelayCommand(GoToColorSelection);
            SelectColorCommand = new RelayCommand<Colorrr>(SelectColor);

            // Do some logic
            _colors = _colorPalletService.GetColors();
            _colorNames = _colors.Keys;


            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.

                _currentColor.Red = 0;
                _currentColor.Green = 255;
                _currentColor.Blue = 255;

                Update();
            }
            else
            {
                // Code runs "for real"

                // Get default color for the first time
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

                // If settings exists, let's use it
                if (_localSettingsService.CanRetrieveComposite("color"))
                {
                    var color = _localSettingsService.RetrieveComposite("color");

                    _currentColor.Red = (byte)color["red"];
                    _currentColor.Blue = (byte)color["blue"];
                    _currentColor.Green = (byte)color["green"];
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

        private void GoToColorSelection()
        {
            _navigationService.NavigateTo("SelectColor");
        }

        private void SelectColor(Colorrr color)
        {
            _navigationService.GoBack();
        }

        #endregion


        #region Methods

        public void Update([CallerMemberName] string property = null)
        {
            // Update UI
            if (property == "HEXText")
            {
                try
                {
                    CurrentColor.HexToColorrr(HEXText);
                    _RGBText = HEXText.HexToRgb();
                }
                catch { }
            }
            else if (property == "RGBText")
            {
                try
                {
                    _HEXText = RGBText.RgbToHex();
                    CurrentColor.HexToColorrr(HEXText);
                }
                catch { }
            }
            else
            {
                _HEXText = CurrentColor.ColorrrToHex();
                _RGBText = HEXText.HexToRgb();
            }

            // Try to get a Color Name that match the current color
            var matchedColor = _colors.FirstOrDefault(c => CurrentColor.Red == c.Value.Red &&
                                                           CurrentColor.Blue == c.Value.Blue &&
                                                           CurrentColor.Green == c.Value.Green);

            ColorName = (string.IsNullOrWhiteSpace(matchedColor.Key)) ? string.Empty : matchedColor.Key;

            // Save settings
            _localSettingsService.SaveComposite("color", new Dictionary<string, object>
            {
                {"red", _currentColor.Red},
                {"blue", _currentColor.Blue},
                {"green", _currentColor.Green}
            });

            // Notify UI
            RaisePropertyChanged("CurrentColor");
            RaisePropertyChanged("IsBrightness");
            RaisePropertyChanged("HEXText");
            RaisePropertyChanged("RGBText");
            RaisePropertyChanged("ColorName");
        }

        #endregion
    }
}