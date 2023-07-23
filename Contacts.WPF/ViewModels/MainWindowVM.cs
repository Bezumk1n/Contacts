using Contacts.Application.Common.Interfaces;
using Contacts.Application.Contacts.Commands.DeleteContact;
using Contacts.Application.Contacts.Queries.GetContacts;
using Contacts.Application.Users.Queries;
using Contacts.Domain.Common;
using Contacts.Domain.Entities;
using Contacts.WPF.Commands;
using Contacts.WPF.Common;
using Contacts.WPF.Common.Navigation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Contacts.WPF.ViewModels
{
    /// <summary>
    /// Вью модель оснвого окна
    /// </summary>
    public class MainWindowVM : BaseVM
    {
        #region Navigation
        /// <summary>
        /// Сервис навигации
        /// </summary>
        private readonly INavigationService _navigationService;
        /// <summary>
        /// Контент диалогового окна
        /// </summary>
        public IViewModel DialogContent => _navigationService.DialogContent;
        /// <summary>
        /// Контент основоного окна
        /// </summary>
        public IViewModel ViewModelContent => _navigationService.ViewModelContent;
        /// <summary>
        /// Состояние - открыто ли диалоговое окно
        /// </summary>
        public bool IsDialogOpen => _navigationService.IsDialogOpen;
        #endregion
        #region Properties
        /// <summary>
        /// Заголовок
        /// </summary>
        public override string Title => "Контакты";
        #endregion
        #region Commands
        /// <summary>
        /// Команда закзрытия диалогового окна
        /// </summary>
        public ICommand CommandCloseDialog { get; }
        /// <summary>
        /// Команда перехода к экрану контактов
        /// </summary>
        public ICommand CommandNavigateToContacts { get; }
        /// <summary>
        /// Команда перехода к экрану информации
        /// </summary>
        public ICommand CommandNavigateToInformation { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        /// <param name="navigationService"></param>
        public MainWindowVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationService.DialogChanged += RefreshDialogContent;
            _navigationService.ViewModelChanged += RefreshViewModelContent;

            CommandCloseDialog = new DelegateCommand(_ => _navigationService.CloseDialog());
            CommandNavigateToContacts = new DelegateCommand(_ => _navigationService.NavigateTo<ContactsVM>());
            CommandNavigateToInformation = new DelegateCommand(_ => _navigationService.NavigateTo<InfoVM>());
            _navigationService.NavigateTo<ContactsVM>();
        }
        #endregion
        #region Methods
        /// <summary>
        /// Обновить основной контент
        /// </summary>
        private void RefreshViewModelContent()
        {
            OnPropertyChanged(() => ViewModelContent);
        }
        /// <summary>
        /// Обновить контент диалогового окна
        /// </summary>
        private void RefreshDialogContent()
        {
            OnPropertyChanged(() => DialogContent);
            OnPropertyChanged(() => IsDialogOpen);
        }
        #endregion
    }
}
