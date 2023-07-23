using Contacts.Application.Contacts.Commands.AddContact;
using Contacts.Domain.Entities;
using Contacts.WPF.Commands;
using Contacts.WPF.Common;
using Contacts.WPF.Common.Navigation;
using MediatR;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contacts.WPF.ViewModels
{
    /// <summary>
    /// Вью модель окна создания нового контакта
    /// </summary>
    public class AddContactVM : BaseVM
    {
        #region Properties
        /// <summary>
        /// Заголовок
        /// </summary>
        public override string Title => "Добавить новый контакт";
        /// <summary>
        /// Навигационный сервис
        /// </summary>
        private readonly INavigationService _navigationService;
        /// <summary>
        /// Посредник для выполнения команды
        /// </summary>
        private readonly ISender _mediator;
        /// <summary>
        /// Новый контакт
        /// </summary>
        public Contact Contact { get; set; } = new Contact();
        #endregion
        #region Commands
        /// <summary>
        /// Команда закрытия диалогового окна
        /// </summary>
        public ICommand CommandCloseDialog { get; }
        /// <summary>
        /// Команда добавления нового контакта
        /// </summary>
        public ICommand CommandAddContact { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Базовый конструктор
        /// </summary>
        public AddContactVM(INavigationService navigationService, ISender mediator)
        {
            _navigationService = navigationService;
            _mediator = mediator;

            CommandCloseDialog = new DelegateCommand(_ => _navigationService.CloseDialog());
            CommandAddContact = new DelegateCommand(async _ => await AddContact(), () => !string.IsNullOrWhiteSpace(Contact.Name));
        }
        #endregion
        #region Methods
        /// <summary>
        /// Метод добавления нового контакта
        /// </summary>
        private async Task AddContact()
        {
            var command = new AddContactCommand()
            {
                Contact = this.Contact
            };
            await _mediator.Send(command);
            _navigationService.CloseDialog();
        }
        #endregion
    }
}
