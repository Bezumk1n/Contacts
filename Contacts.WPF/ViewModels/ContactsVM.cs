using Contacts.Application.Common.Interfaces;
using Contacts.Application.Contacts.Commands.DeleteContact;
using Contacts.Application.Contacts.Queries.GetContacts;
using Contacts.Application.Users.Queries;
using Contacts.Domain.Entities;
using Contacts.WPF.Commands;
using Contacts.WPF.Common;
using Contacts.WPF.Common.Navigation;
using Contacts.WPF.Models;
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
        public List<Contact> Contacts => GetFilteredContacts();

        private List<Contact> GetFilteredContacts()
        {
            List<Contact> result = new List<Contact>();
            if (_contactsStore.Collection.Any())
            {
                result = _contactsStore.Collection
                    .Where(q => q.ToString().ToLower().Contains(_searchText.ToLower()))
                    .ToList();
                if (!CurrentFilterString.IsAll)
                    result = result
                        .Where(q => CurrentFilterString.Chars.ToLower().Contains(q.Name.ToLower().First()))
                        .ToList();
            }
            return result.OrderBy(q => q.Name).ToList();
        }
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
        /// <summary>
        /// Коллекция фильтров
        /// </summary>
        public List<FilterString> FilterStrings { get; private set; }
        /// <summary>
        /// Текущий выбранный фильтр
        /// </summary>
        public FilterString CurrentFilterString
        {
            get => _currentFilterString;
            set
            {
                _currentFilterString = value;
                OnPropertyChanged();
                OnPropertyChanged(() => Contacts);
                OnPropertyChanged(() => FilterStrings);
            }
        }
        private FilterString _currentFilterString;
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

            CreateFilterStrings();

            _mediator.Send(new GetContactsQuerry());
            _mediator.Send(new GetUserQuerry());
        }
        #endregion
        #region Methods
        /// <summary>
        /// Создать коллекцию фильтров
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void CreateFilterStrings()
        {
            FilterStrings = new List<FilterString>
            {
                FilterString.Create("Все", isAll: true),
                FilterString.Create("АБВ"),
                FilterString.Create("ГДЕЁ"),
                FilterString.Create("ЖЗИЙ"),
                FilterString.Create("КЛМ"),
                FilterString.Create("НОП"),
                FilterString.Create("РСТ"),
                FilterString.Create("УФХ"),
                FilterString.Create("ЦЧШ"),
                FilterString.Create("ЩЪЫЬ"),
                FilterString.Create("ЭЮЯ")
            };
            CurrentFilterString = FilterStrings.First(q => q.IsAll);
        }
        /// <summary>
        /// Задать символы по которым будет происходить фильтрация контактов
        /// </summary>
        /// <param name="obj"></param>
        private void SetFilterChars(object obj)
        {
            if (obj is FilterString filter)
            {
                FilterStrings = FilterStrings.Select(q => { q.IsChecked = false; return q; }).ToList();
                filter.IsChecked = true;
                CurrentFilterString = filter;
            }
        }
        #endregion
    }
}
