﻿using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Colorrrs.Converters
{
    public class IsBrightnessToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool bValue = (bool) value;
            return bValue ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var solidColorBrush = value as SolidColorBrush;

            if (solidColorBrush == null)
                throw new NullReferenceException();

            if (solidColorBrush.Color == Colors.Black)
                return true;
            if (solidColorBrush.Color == Colors.White)
                return false;

            throw new NotImplementedException();
        }
    }
}
