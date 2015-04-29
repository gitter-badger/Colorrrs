using System;
using Colorrrs.Core.Services;
using Colorrrs.Core.ViewModel.Abstract;
using Colorrrs.Core.ViewModel.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Colorrrs.UnitTests
{
    [TestClass]
    public class MainTests
    {
        private IMainViewModel _mainViewModel;

        private Mock<ILocalSettingsService> _localSettingsService;


        [TestInitialize]
        public void TestInitialize()
        {
            _localSettingsService = new Mock<ILocalSettingsService>();

            _mainViewModel = new MainViewModel(_localSettingsService.Object);
        }


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

            _mainViewModel.HEXText = _mainViewModel.HEXText.Substring(0, _mainViewModel.HEXText.Length - 1);
            _mainViewModel.HEXText += "2";

            // Assert
            Assert.AreEqual(_mainViewModel.HEXText, "FF2");
            Assert.AreEqual(_mainViewModel.RGBText, "rgb(255,255,34)");
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
            for (short i = 0; i < 2; i++)
                _mainViewModel.RGBText = _mainViewModel.RGBText.Substring(0, _mainViewModel.RGBText.Length - 1);

            _mainViewModel.RGBText += "2)";

            // Assert
            Assert.AreEqual(_mainViewModel.HEXText, "#FFFFFC");
            Assert.AreEqual(_mainViewModel.RGBText, "rgb(255,255,252)");
        }
    }
}
