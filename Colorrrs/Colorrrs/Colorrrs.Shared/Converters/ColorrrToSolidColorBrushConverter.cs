using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Colorrrs.Core.Model;

namespace Colorrrs.Converters
{
    public class ColorrrToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var colorrr = value as Colorrr;

            if (colorrr == null)
                throw new NullReferenceException();

            return new SolidColorBrush(new Color
            {
                A = 255,
                R = colorrr.Red,
                G = colorrr.Green,
                B = colorrr.Blue
            });
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var solidColorBrush = value as SolidColorBrush;

            if (solidColorBrush == null)
                throw new NullReferenceException();

            return new Colorrr
            {
                Red = solidColorBrush.Color.R,
                Green = solidColorBrush.Color.G,
                Blue = solidColorBrush.Color.B
            };
        }
    }
}
