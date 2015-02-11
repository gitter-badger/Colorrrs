using System;
using Colorrrs.Core.ViewModel.Abstract;
using Colorrrs.Core.ViewModel.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Colorrrs.UnitTests
{
    [TestClass]
    public class MainTests
    {
        private IMainViewModel _mainViewModel;


        [TestInitialize]
        public void TestInitialize()
        {
            _mainViewModel = new MainViewModel();
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

            // Act

            // Assert
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Can_Get_Brightness_Of_Black_Color()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Can_Get_Brightness_Of_Random_Color()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Can_Convert_HEX_To_RGB()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Cant_Convert_Not_Correct_HEX_Color_To_RGB()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Can_Convert_RGB_To_HEX()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Cant_Convert_Not_Correct_RGB_Color_To_HEX()
        {
            // Arrange

            // Act

            // Assert
            Assert.Inconclusive();
        }
    }
}
