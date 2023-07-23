using Contacts.Application.Common.Interfaces;
using Contacts.Application.Contacts.Commands.DeleteContact;
using Contacts.Application.Contacts.Queries.GetContacts;
using Contacts.Application.Users.Queries;
using Contacts.Domain.Entities;
using Contacts.WPF.Commands;
using Contacts.WPF.Common;
using Contacts.WPF.Common.Navigation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Contacts.WPF.ViewModels
{
    /// <summary>
    /// Вью модель окна списка и управления контактами
    /// </summary>
    public class ContactsVM : BaseVM
    {
        #region Properties

        /// <summary>
        /// Навигационный сервис 
        /// </summary>
        private readonly INavigationService _navigationService;
        /// <summary>
        /// Посредник для выполнения команды
        /// </summary>
        private readonly ISender _mediator;
        /// <summary>
        /// Кэш контактов
        /// </summary>
        private readonly IStore<Contact> _contactsStore;
        /// <summary>
        /// Кеш пользователей
        /// </summary>
        private readonly IStore<User> _usersStore;
        /// <summary>
        /// Коллекция отфильтроаных и отсортированых контактов
        /// </summary>
        public List<Contact> Contacts => _contactsStore.Collection
            .Where(q => _charSet.Contains(q.Name.ToLower().First()))
            .Where(q => q.ToString().ToLower().Contains(_searchText.ToLower()))
            .OrderBy(q => q.Name)
            .ToList();
        /// <summary>
        /// Коллекция символов для фильтрации контактов
        /// </summary>
        private char[] _charSet;
        /// <summary>
        /// Текст поиска контакта
        /// </summary>
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(() => Contacts);
            }
        }
        private string _searchText = string.Empty;
        /// <summary>
        /// Текущий выбранный контакт
        /// </summary>
        public Contact SelectedContact
        {
            get => _contactsStore.SelectedItem;
            set => _contactsStore.SelectedItem = value;
        }
        /// <summary>
        /// Коллекция пользователей
        /// </summary>
        public List<User> Users => _usersStore.Collection.ToList();
        #endregion
        #region Commands
        /// <summary>
        /// Команда добавленяия нового контакта
        /// </summary>
        public ICommand CommandAddContact { get; }
        /// <summary>
        /// Команда обновления контакта
        /// </summary>
        public ICommand CommandUpdateContact { get; }
        /// <summary>
        /// Команда удаления контакта
        /// </summary>
        public ICommand CommandRemoveContact { get; }
        /// <summary>
        /// Комнда фильтрации контактов
        /// </summary>
        public ICommand CommandSetFilterChars { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Баовый конструктор
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="mediator"></param>
        /// <param name="contactsStore"></param>
        /// <param name="usersStore"></param>
        public ContactsVM(INavigationService navigationService, ISender mediator, IStore<Contact> contactsStore, IStore<User> usersStore)
        {
            _mediator = mediator;
            _contactsStore = contactsStore;
            _usersStore = usersStore;
            _navigationService = navigationService;

            _contactsStore.Changed += () => OnPropertyChanged(() => Contacts);
            _usersStore.Changed += () => OnPropertyChanged(() => Users);

            CommandAddContact = new DelegateCommand(_ => _navigationService.OpenDialog<AddContactVM>());
            CommandUpdateContact = new DelegateCommand(_ => _navigationService.OpenDialog<UpdateContactVM>(), () => SelectedContact != null);

            CommandRemoveContact = new DelegateCommand(async _ =>
            await _mediator.Send(
                new RemoveContactCommand()
                {
                    Id = SelectedContact.Id
                }),
                () => SelectedContact != null);

            CommandSetFilterChars = new DelegateCommand(obj => SetFilterChars(obj));

            _mediator.Send(new GetContactsQuerry());
            _mediator.Send(new GetUserQuerry());

            SetDefaultCharSet();
        }
        #endregion
        #region Methods
        /// <summary>
        /// Задать символы по которым будет происходить фильтрация контактов
        /// </summary>
        /// <param name="obj"></param>
        private void SetFilterChars(object obj)
        {
            if (obj is string str)
            {
                if (string.IsNullOrWhiteSpace(str))
                    SetDefaultCharSet();
                else
                    SetCharSet(str);
                OnPropertyChanged(() => Contacts);
            }
        }
        /// <summary>
        /// Задать дефолтное состояние символов для фильтрации
        /// </summary>
        private void SetDefaultCharSet() => SetCharSet("ёйцукенгшщзъэждлорпавыфячсмитьбю");
        /// <summary>
        /// Задать символы для фильтрации
        /// </summary>
        /// <param name="str"></param>
        private void SetCharSet(string str) => _charSet = str.ToLower().ToCharArray();
        #endregion
    }
}
