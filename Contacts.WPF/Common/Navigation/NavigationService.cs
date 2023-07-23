using System;

namespace Contacts.WPF.Common.Navigation
{
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// Событие изменения текущей вью-модели
        /// </summary>
        public event Action? DialogChanged;
        public event Action? ViewModelChanged;
        private readonly Func<Type, IViewModel> _viewModelFactory;
        public IViewModel ViewModelContent
        {
            get => _viewModelContent;
            private set
            {
                _viewModelContent = value;
                ViewModelChanged?.Invoke();
            }
        }
        private IViewModel _viewModelContent;
        /// /// <summary>
        /// Текущее диалоговое окно
        /// </summary>
        public IViewModel DialogContent
        {
            get => _dialogContent;
            private set
            {
                _dialogContent = value;
                DialogChanged?.Invoke();
            }
        }
        private IViewModel _dialogContent;
        /// <summary>
        /// Статус открыто ли диалоговое окно
        /// </summary>
        public bool IsDialogOpen { get; private set; } = false;
        public NavigationService(Func<Type, IViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }
        public void OpenDialog<IViewModel>()
        {
            IsDialogOpen = true;
            DialogContent = _viewModelFactory.Invoke(typeof(IViewModel));
        }
        public void CloseDialog() 
        {
            IsDialogOpen = false;
            DialogContent = new BaseVM();
        }

        public void NavigateTo<IViewModel>()
        {
            ViewModelContent = _viewModelFactory.Invoke(typeof(IViewModel));
        }
    }
}
