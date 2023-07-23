using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.WPF.Common.Navigation
{
    public interface INavigationService
    {
        event Action DialogChanged;
        event Action ViewModelChanged;
        public IViewModel DialogContent { get; }
        public IViewModel ViewModelContent { get; }
        bool IsDialogOpen { get; }
        void OpenDialog<IViewModel>();
        void CloseDialog();
        void NavigateTo<IViewModel>();
    }
}
