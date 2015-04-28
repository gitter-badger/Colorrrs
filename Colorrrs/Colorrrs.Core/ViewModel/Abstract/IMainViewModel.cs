using System.Windows.Input;
using Colorrrs.Core.Model;

namespace Colorrrs.Core.ViewModel.Abstract
{
    public interface IMainViewModel
    {
        bool CanRandomize { get; set; }
        Colorrr CurrentColor { get; }
        bool IsBrightness { get; }
        string HEXText { get; set; }
        string RGBText { get; set; }
        
        ICommand RandomizeColorCommand { get; }

        void Update(string property = null);
    }
}
