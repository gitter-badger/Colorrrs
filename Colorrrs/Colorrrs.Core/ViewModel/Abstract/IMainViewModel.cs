using System.Windows.Input;
using Colorrrs.Core.Model;

namespace Colorrrs.Core.ViewModel.Abstract
{
    public interface IMainViewModel
    {
        Colorrr CurrentColor { get; }

        ICommand RandomizeColorCommand { get; }
    }
}
