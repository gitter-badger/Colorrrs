using System.Collections.Generic;
using Colorrrs.Core.Model;

namespace Colorrrs.Core.Services
{
    public interface IColorPalletService
    {
        IEnumerable<Colorrr> GetColors();
    }
}
