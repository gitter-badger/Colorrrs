using System;
using System.Globalization;
using Colorrrs.Core.Model;

namespace Colorrrs.Core.Helpers
{
    // TODO : avoid exceptions (TryParse)
    // TODO : use REGEX
    public static class ColorConvertHelper
    {
        public static string ColorrrToHex(this Colorrr color)
        {
            string hexRed = color.Red.ToString("X2");
            string hexGreen = color.Green.ToString("X2");
            string hexBlue = color.Blue.ToString("X2");

            return SimplifyHex(hexRed, hexGreen, hexBlue);
        }

        public static void HexToColorrr(this Colorrr color, string hex)
        {
            byte decRed, decGreen, decBlue;
            HexToRgb(hex, out decRed, out decGreen, out decBlue);

            color.Red = decRed;
            color.Green = decGreen;
            color.Blue = decBlue;
        }

        public static string HexToRgb(this string hex)
        {
            byte decRed, decGreen, decBlue;
            HexToRgb(hex, out decRed, out decGreen, out decBlue);

            return string.Format("rgb({0},{1},{2})", decRed, decGreen, decBlue);
        }

        public static string RgbToHex(this string rgb)
        {
            string hexRed, hexGreen, hexBlue;
            RgbToHex(rgb, out hexRed, out hexGreen, out hexBlue);

            return SimplifyHex(hexRed, hexGreen, hexBlue);
        }


        private static void HexToRgb(string hex, out byte decRed, out byte decGreen, out byte decBlue)
        {
            if (hex == null)
                throw new NullReferenceException();

            string hexRed, hexGreen, hexBlue;

            // concat string 6 chars-based (like '#ABCDEF' or '123456')
            if (hex.Length == 6 || (hex.Length == 7 && hex[0] == '#'))
            {
                int stepSharp = (hex[0] == '#') ? 1 : 0;

                hexRed = hex.Substring(stepSharp, 2);
                hexGreen = hex.Substring(stepSharp + 2, 2);
                hexBlue = hex.Substring(stepSharp + 4, 2);
            }
            // concat string 3 chars-based (like '#ABC' or '123')
            else if (hex.Length == 3 || (hex.Length == 4 && hex[0] == '#'))
            {
                int stepSharp = (hex[0] == '#') ? 1 : 0;

                hexRed = hex.Substring(stepSharp, 1);
                hexGreen = hex.Substring(stepSharp + 1, 1);
                hexBlue = hex.Substring(stepSharp + 2, 1);

                hexRed += hexRed;
                hexGreen += hexGreen;
                hexBlue += hexBlue;
            }
            else
                throw new InvalidCastException();

            decRed = byte.Parse(hexRed, NumberStyles.HexNumber);
            decGreen = byte.Parse(hexGreen, NumberStyles.HexNumber);
            decBlue = byte.Parse(hexBlue, NumberStyles.HexNumber);
        }

        private static void RgbToHex(string rgb, out string hexRed, out string hexGreen, out string hexBlue)
        {
            string[] values = rgb.Split(new[] { "rgb(", ")", ",", " " }, StringSplitOptions.RemoveEmptyEntries);

            if (values.Length == 3)
            {
                byte decRed = byte.Parse(values[0]);
                byte decGreen = byte.Parse(values[1]);
                byte decBlue = byte.Parse(values[2]);

                hexRed = decRed.ToString("X2");
                hexGreen = decGreen.ToString("X2");
                hexBlue = decBlue.ToString("X2");
            }
            else
                throw new InvalidCastException();
        }

        private static string SimplifyHex(string hexRed, string hexGreen, string hexBlue)
        {
            if (hexRed[0] == hexRed[1] && hexGreen[0] == hexGreen[1] && hexBlue[0] == hexBlue[1])
                return string.Format("#{0}{1}{2}", hexRed[0], hexGreen[0], hexBlue[0]);

            return string.Format("#{0}{1}{2}", hexRed, hexGreen, hexBlue);
        }
    }
}
