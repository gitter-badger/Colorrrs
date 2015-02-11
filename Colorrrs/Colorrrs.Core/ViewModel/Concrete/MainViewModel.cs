using System;
using System.Windows.Input;
using Colorrrs.Core.Model;
using Colorrrs.Core.ViewModel.Abstract;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Colorrrs.Core.ViewModel.Concrete
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private readonly Random _random = new Random();

        private readonly Colorrr _currentColor = new Colorrr();
        public Colorrr CurrentColor { get { return _currentColor; } }

        public bool IsBrightness { get { return _currentColor.Brightness > 128f; } }


        public ICommand RandomizeColorCommand { get; private set; }


        public MainViewModel()
        {
            RandomizeColorCommand = new RelayCommand(RandomizeColor);

            //if (IsInDesignMode)
            //{
            //    // Code runs in Blend --> create design time data.
            //}
            //else
            //{
            //    // Code runs "for real"
            //}
        }


        private void RandomizeColor()
        {
            CurrentColor.Red = (byte)_random.Next(0, 256);
            CurrentColor.Green = (byte)_random.Next(0, 256);
            CurrentColor.Blue = (byte)_random.Next(0, 256);
        }
    }
}