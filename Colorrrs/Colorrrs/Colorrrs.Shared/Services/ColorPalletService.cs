using System.Collections.Generic;
using System.Reflection;
using Windows.UI;
using Colorrrs.Core.Model;
using Colorrrs.Core.Services;

namespace Colorrrs.Services
{
    public class ColorPalletService : IColorPalletService
    {
        public Dictionary<string, Colorrr> GetColors()
        {
            var colorsDictionary = new Dictionary<string, Colorrr>();
            var colors = typeof (Colors).GetRuntimeProperties();

            foreach (var colorProperty in colors)
            {
                var color = (Color)colorProperty.GetValue(null, null);

                colorsDictionary.Add(colorProperty.Name, new Colorrr
                {
                    Red = color.R,
                    Blue = color.G,
                    Green = color.B,
                });
            }

            return colorsDictionary;
        }
    }
}
