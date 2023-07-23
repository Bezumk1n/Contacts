using Contacts.Application.Common.Interfaces;
using Contacts.Application.Contacts.Commands.AddContact;
using Contacts.Application.Contacts.Commands.UpdateContact;
using Contacts.Domain.Entities;
using Contacts.Services.Stores;
using Contacts.WPF.Commands;
using Contacts.WPF.Common;
using Contacts.WPF.Common.Navigation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contacts.WPF.ViewModels
{
    /// <summary>
    /// Вью модель окна обновления контакта
    /// </summary>
    public class UpdateContactVM : BaseVM
    {
        #region Properties
        /// <summary>
        /// Заголовок
        /// </summary>
        public override string Title => "Добавить новый контакт";
        /// <summary>
        /// Сервис навигации
        /// </summary>
        private readonly INavigationService _navigationService;
        /// <summary>
        /// Посредник для выполнения команды
        /// </summary>
        private readonly ISender _mediator;
        /// <summary>
        /// Кэш контактов
        /// </summary>
        private readonly IStore<Contact> _store;
        /// <summary>
        /// Новый контакт
        /// </summary>
        public Contact Contact { get; }
        #endregion
        #region Commands
        /// <summary>
        /// Команда закрытия диалогового окна
        /// </summary>
        public ICommand CommandCloseDialog { get; }
        /// <summary>
        /// Команда обновления контакта
        /// </summary>
        public ICommand CommandUpdateContact { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="mediator"></param>
        /// <param name="store"></param>
        public UpdateContactVM(INavigationService navigationService, ISender mediator, IStore<Contact> store)
        {
            _navigationService = navigationService;
            _mediator = mediator;
            _store = store;
            CommandCloseDialog = new DelegateCommand(_ => _navigationService.CloseDialog());
            CommandUpdateContact = new DelegateCommand(async _ => await UpdateContact(), () => !string.IsNullOrWhiteSpace(Contact!.Name));

            Contact = _store.SelectedItem.Clone();
        }
        #endregion
        #region Methods
        /// <summary>
        /// Обновить контакт
        /// </summary>
        /// <returns></returns>
        private async Task UpdateContact()
        {
            var command = new UpdateContactCommand()
            {
                Contact = this.Contact
            };
            await _mediator.Send(command);
            _navigationService.CloseDialog();
        }
        #endregion
    }
}
