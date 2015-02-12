using System;
using Colorrrs.Core.Helpers;
using Colorrrs.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Colorrrs.UnitTests
{
    [TestClass]
    public class ColorConvertTests
    {
        [TestMethod]
        public void Can_Get_HEX_of_Color_Six_Chars_Based()
        {
            // Arrange
            var color = new Colorrr
            {
                Red = 0,
                Green = 6,
                Blue = 255
            };

            // Act
            string hex = color.ColorrrToHex();

            // Assert
            Assert.AreEqual(hex, "#0006FF");
        }

        [TestMethod]
        public void Can_Get_RGB_of_Color_Six_Chars_Based_With_Sharp()
        {
            // Arrange
            const string hex = "#0006FF";
            var color = new Colorrr();

            // Act
            color.HexToColorrr(hex);

            // Assert
            Assert.AreEqual(color.Red, 0);
            Assert.AreEqual(color.Green, 6);
            Assert.AreEqual(color.Blue, 255);
        }

        [TestMethod]
        public void Can_Get_RGB_of_Color_Six_Chars_Based_Without_Sharp()
        {
            // Arrange
            const string hex = "0006FF";
            var color = new Colorrr();

            // Act
            color.HexToColorrr(hex);

            // Assert
            Assert.AreEqual(color.Red, 0);
            Assert.AreEqual(color.Green, 6);
            Assert.AreEqual(color.Blue, 255);
        }

        [TestMethod]
        public void Can_Get_HEX_of_Color_Three_Chars_Based()
        {
            // Arrange
            var color = new Colorrr
            {
                Red = 170,
                Green = 187,
                Blue = 204
            };

            // Act
            string hex = color.ColorrrToHex();

            // Assert
            Assert.AreEqual(hex, "#ABC");
        }

        [TestMethod]
        public void Can_Get_RGB_of_Color_Three_Chars_Based_With_Sharp()
        {
            // Arrange
            const string hex = "#ABC";
            var color = new Colorrr();

            // Act
            color.HexToColorrr(hex);

            // Assert
            Assert.AreEqual(color.Red, 170);
            Assert.AreEqual(color.Green, 187);
            Assert.AreEqual(color.Blue, 204);
        }

        [TestMethod]
        public void Can_Get_RGB_of_Color_Three_Chars_Based_Without_Sharp()
        {
            // Arrange
            const string hex = "ABC";
            var color = new Colorrr();

            // Act
            color.HexToColorrr(hex);

            // Assert
            Assert.AreEqual(color.Red, 170);
            Assert.AreEqual(color.Green, 187);
            Assert.AreEqual(color.Blue, 204);
        }
        
        [TestMethod]
        public void Can_Convert_HEX_To_RGB()
        {
            // Arrange
            const string hex = "#ABC";

            // Act
            string result = hex.HexToRgb();

            // Assert
            Assert.AreEqual(result, "rgb(170,187,204)");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Cant_Convert_Not_Correct_HEX_Color_To_RGB()
        {
            // Arrange
            const string hex = "#A4BC";

            // Act
            string result = hex.HexToRgb();

            // Assert
        }

        [TestMethod]
        public void Can_Convert_RGB_To_HEX()
        {
            // Arrange
            const string rgb = "rgb(170,187,204)";

            // Act
            string result = rgb.RgbToHex();

            // Assert
            Assert.AreEqual(result, "#ABC");
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void Cant_Convert_Not_Correct_RGB_Color_To_HEX_With_Overflow_Value()
        {
            // Arrange
            const string rgb = "rgb(288,187,204)";

            // Act
            string result = rgb.RgbToHex();

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Cant_Convert_Not_Correct_RGB_Color_To_HEX_With_Too_Few_Values()
        {
            // Arrange
            const string rgb = "rgb(,187,204)";

            // Act
            string result = rgb.RgbToHex();

            // Assert
        }
    }
}
