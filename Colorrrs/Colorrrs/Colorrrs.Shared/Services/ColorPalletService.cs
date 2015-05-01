using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Windows.UI;
using Colorrrs.Core.Model;
using Colorrrs.Core.Services;

namespace Colorrrs.Services
{
    public class ColorPalletService : IColorPalletService
    {
        public IEnumerable<Colorrr> GetColors()
        {
            var colors = typeof (Colors).GetRuntimeProperties();

            return (from colorProperty in colors
                let color = (Color) colorProperty.GetValue(null, null)
                where color.A == 255
                select new Colorrr
                {
                    ColorName = colorProperty.Name, Red = color.R, Blue = color.G, Green = color.B,
                }).ToList();
        }
    }
}
