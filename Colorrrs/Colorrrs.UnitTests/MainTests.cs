using System;
using System.Collections.Generic;
using Colorrrs.Core.Model;
using Colorrrs.Core.Services;
using Colorrrs.Core.ViewModel.Abstract;
using Colorrrs.Core.ViewModel.Concrete;
using GalaSoft.MvvmLight.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Colorrrs.UnitTests
{
    [TestClass]
    public class MainTests
    {
        private int _canRetrieveCompositeCalls;
        private int _retrieveCompositeCalls;
        private int _saveCompositeCalls;

        private IMainViewModel _mainViewModel;

        private Mock<ILocalSettingsService> _localSettingsService;
        private Mock<IColorPalletService> _colorPalletService;
        private Mock<INavigationService> _navigationService;


        [TestInitialize]
        public void TestInitialize()
        {
            _canRetrieveCompositeCalls = 0;
            _retrieveCompositeCalls = 0;
            _saveCompositeCalls = 0;


            _localSettingsService = new Mock<ILocalSettingsService>();
            _colorPalletService = new Mock<IColorPalletService>();
            _navigationService = new Mock<INavigationService>();


            _colorPalletService.Setup(s => s.GetColors())
                .Returns(() => new List<Colorrr>
                {
                    new Colorrr {ColorName = "Aqua", Red = 0, Green = 255, Blue = 255},
                    new Colorrr {ColorName = "White", Red = 255, Green = 255, Blue = 255},
                    new Colorrr {ColorName = "Black", Red = 0, Green = 0, Blue = 0}
                });

            _localSettingsService.Setup(s => s.CanRetrieveComposite(It.IsAny<string>()))
                .Returns(false)
                .Callback(() => _canRetrieveCompositeCalls++);

            _localSettingsService.Setup(s => s.RetrieveComposite(It.IsAny<string>()))
                .Returns(new Dictionary<string, object>())
                .Callback(() => _retrieveCompositeCalls++);

            _localSettingsService.Setup(s => s.SaveComposite(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
                .Callback(() => _saveCompositeCalls++);


            _mainViewModel = new MainViewModel(_localSettingsService.Object,
                _colorPalletService.Object,
                _navigationService.Object);
        }


        #region Basic Color Implementation

        [TestMethod]
        public void Can_Get_Default_Values()
        {
            // Arrange

            // Act

            // Assert
            Assert.IsNotNull(_mainViewModel.CurrentColor);
            Assert.AreEqual(_mainViewModel.CurrentColor.Red, 255);
            Assert.AreEqual(_mainViewModel.CurrentColor.Green, 255);
            Assert.AreEqual(_mainViewModel.CurrentColor.Blue, 255);
            Assert.AreEqual(_mainViewModel.HEXText, "#FFF");
            Assert.AreEqual(_mainViewModel.RGBText, "rgb(255,255,255)");
            Assert.AreEqual(_mainViewModel.ColorName, "White");
        }

        [TestMethod]
        public void Can_Get_A_Random_Color()
        {
            // Arrange
            var ran = new Random();

            // Act
            var redResult = ran.Next(0, 256);
            var greenResult = ran.Next(0, 256);
            var blueResult = ran.Next(0, 256);
            _mainViewModel.RandomizeColorCommand.Execute(null);

            // Assert
            Assert.IsNotNull(_mainViewModel.CurrentColor);
            Assert.AreEqual(redResult, _mainViewModel.CurrentColor.Red);
            Assert.AreEqual(greenResult, _mainViewModel.CurrentColor.Green);
            Assert.AreEqual(blueResult, _mainViewModel.CurrentColor.Blue);
        }

        #endregion

        #region Brightness

        [TestMethod]
        public void Can_Get_Brightness_Of_White_Color()
        {
            // Arrange
            _mainViewModel.CurrentColor.Red = 255;
            _mainViewModel.CurrentColor.Green = 255;
            _mainViewModel.CurrentColor.Blue = 255;

            // Act

            // Assert
            Assert.AreEqual(_mainViewModel.CurrentColor.Red, 255);
            Assert.AreEqual(_mainViewModel.CurrentColor.Green, 255);
            Assert.AreEqual(_mainViewModel.CurrentColor.Blue, 255);
            Assert.AreEqual(_mainViewModel.CurrentColor.Brightness, 255);
            Assert.IsTrue(_mainViewModel.IsBrightness);
        }

        [TestMethod]
        public void Can_Get_Brightness_Of_Black_Color()
        {
            // Arrange
            _mainViewModel.CurrentColor.Red = 0;
            _mainViewModel.CurrentColor.Green = 0;
            _mainViewModel.CurrentColor.Blue = 0;

            // Act

            // Assert
            Assert.AreEqual(_mainViewModel.CurrentColor.Red, 0);
            Assert.AreEqual(_mainViewModel.CurrentColor.Green, 0);
            Assert.AreEqual(_mainViewModel.CurrentColor.Blue, 0);
            Assert.AreEqual(_mainViewModel.CurrentColor.Brightness, 0);
            Assert.IsFalse(_mainViewModel.IsBrightness);
        }

        [TestMethod]
        public void Can_Get_Brightness_Of_Random_Color()
        {
            // Arrange
            _mainViewModel.CurrentColor.Red = 122;
            _mainViewModel.CurrentColor.Green = 201;
            _mainViewModel.CurrentColor.Blue = 42;

            // Act

            // Assert
            Assert.IsTrue(_mainViewModel.IsBrightness);
        }

        #endregion

        #region Auto update of Color

        [TestMethod]
        public void Can_Update_Correctly_When_Updating_Hex_Value_To_Another_Six_Based_Character_With_Sharp()
        {
            // Arrange
            _mainViewModel.CurrentColor.Red = 254;
            _mainViewModel.CurrentColor.Green = 254;
            _mainViewModel.CurrentColor.Blue = 254;

            _mainViewModel.Update();

            // Act
            for (short i = 0; i < 2; i++)
                _mainViewModel.HEXText = _mainViewModel.HEXText.Substring(0, _mainViewModel.HEXText.Length - 1);
            for (short i = 0; i < 2; i++)
                _mainViewModel.HEXText += "2";

            // Assert
            Assert.AreEqual(_mainViewModel.HEXText, "#FEFE22");
            Assert.AreEqual(_mainViewModel.RGBText, "rgb(254,254,34)");
            Assert.AreEqual(_mainViewModel.ColorName, string.Empty);
        }

        [TestMethod]
        public void Can_Update_Correctly_When_Updating_Hex_Value_To_Another_Six_Based_Character_Without_Sharp()
        {
            // Arrange
            _mainViewModel.CurrentColor.Red = 254;
            _mainViewModel.CurrentColor.Green = 254;
            _mainViewModel.CurrentColor.Blue = 254;

            _mainViewModel.Update();

            // Act
            _mainViewModel.HEXText = _mainViewModel.HEXText.Substring(1);

            for (short i = 0; i < 2; i++)
                _mainViewModel.HEXText = _mainViewModel.HEXText.Substring(0, _mainViewModel.HEXText.Length - 1);
            for (short i = 0; i < 2; i++)
                _mainViewModel.HEXText += "2";

            // Assert
            Assert.AreEqual(_mainViewModel.HEXText, "FEFE22");
            Assert.AreEqual(_mainViewModel.RGBText, "rgb(254,254,34)");
            Assert.AreEqual(_mainViewModel.ColorName, string.Empty);
        }

        [TestMethod]
        public void Can_Update_Correctly_When_Updating_Hex_Value_To_Three_Based_Character_With_Sharp()
        {
            // Arrange
            _mainViewModel.CurrentColor.Red = 255;
            _mainViewModel.CurrentColor.Green = 255;
            _mainViewModel.CurrentColor.Blue = 255;

            _mainViewModel.Update();

            // Act
            _mainViewModel.HEXText = _mainViewModel.HEXText.Substring(0, _mainViewModel.HEXText.Length - 1);
            _mainViewModel.HEXText += "2";

            // Assert
            Assert.AreEqual(_mainViewModel.HEXText, "#FF2");
            Assert.AreEqual(_mainViewModel.RGBText, "rgb(255,255,34)");
            Assert.AreEqual(_mainViewModel.ColorName, string.Empty);
        }

        [TestMethod]
        public void Can_Update_Correctly_When_Updating_Hex_Value_To_Three_Based_Character_Without_Sharp()
        {
            // Arrange
            _mainViewModel.CurrentColor.Red = 255;
            _mainViewModel.CurrentColor.Green = 255;
            _mainViewModel.CurrentColor.Blue = 255;

            _mainViewModel.Update();

            // Act
            _mainViewModel.HEXText = _mainViewModel.HEXText.Substring(1);

            _mainViewModel.HEXText = _mainViewModel.HEXText.Substring(0, _mainViewModel.HEXText.Length - 1) + "2";

            // Assert
            Assert.AreEqual(_mainViewModel.HEXText, "FF2");
            Assert.AreEqual(_mainViewModel.RGBText, "rgb(255,255,34)");
            Assert.AreEqual(_mainViewModel.ColorName, string.Empty);
        }

        [TestMethod]
        public void Can_Update_Correctly_When_Updating_Rgb_Value_To_Another_Rgb_Value()
        {
            // Arrange
            _mainViewModel.CurrentColor.Red = 255;
            _mainViewModel.CurrentColor.Green = 255;
            _mainViewModel.CurrentColor.Blue = 255;

            _mainViewModel.Update();

            // Act
            _mainViewModel.RGBText = _mainViewModel.RGBText.Substring(0, _mainViewModel.RGBText.Length - 2) + "2)";

            // Assert
            Assert.AreEqual(_mainViewModel.RGBText, "rgb(255,255,252)");
            Assert.AreEqual(_mainViewModel.HEXText, "#FFFFFC");
            Assert.AreEqual(_mainViewModel.ColorName, string.Empty);
        }

        #endregion

        #region Color Selection

        [TestMethod]
        public void Can_Go_Select_Color()
        {
            // Arrange
            int goToSelectColor = 0;

            _navigationService.Setup(s => s.NavigateTo("SelectColor"))
                .Callback(() => goToSelectColor++);

            // Act
            _mainViewModel.GoToColorSelectionCommand.Execute(null);

            // Assert
            Assert.AreEqual(goToSelectColor, 1);
        }

        [TestMethod]
        public void Can_Select_Color()
        {
            // Arrange
            int goBack = 0;

            _navigationService.Setup(s => s.GoBack())
                .Callback(() => goBack++);

            // Act
            _mainViewModel.SelectColorCommand.Execute(new Colorrr
            {
                ColorName = "anything",
                Red = 0,
                Blue = 255,
                Green = 255
            });

            // Assert
            Assert.AreEqual(goBack, 1);
            Assert.AreEqual(_mainViewModel.CurrentColor.Red, 0);
            Assert.AreEqual(_mainViewModel.CurrentColor.Green, 255);
            Assert.AreEqual(_mainViewModel.CurrentColor.Blue, 255);
            Assert.AreEqual(_mainViewModel.ColorName, "Aqua");
        }

        #endregion
    }
}
