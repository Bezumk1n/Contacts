using System;

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
