using Contacts.Application.Common.Interfaces;
using Contacts.Application.Contacts.Commands.DeleteContact;
using Contacts.Application.Contacts.Queries.GetContacts;
using Contacts.Domain.Entities;
using Contacts.MAUI.Commands;
using Contacts.MAUI.Common;
using Contacts.MAUI.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contacts.MAUI.Pages
{
    public class ContactsVM : BaseVM
    {
        private readonly ISender _mediator;
        private readonly IStore<Domain.Entities.Contact> _contactsStore;
        private readonly IStore<User> _usersStore;
        /// <summary>
        /// Коллекция отфильтроаных и отсортированых контактов
        /// </summary>
        public List<Domain.Entities.Contact> Contacts => GetFilteredContacts();
        private List<Domain.Entities.Contact> GetFilteredContacts()
        {
            List<Domain.Entities.Contact> result = new List<Domain.Entities.Contact>();
            if (_contactsStore.Collection.Any())
            {
                result = _contactsStore.Collection
                    .Where(q => q.ToString().ToLower().Contains(_searchText.ToLower()))
                    .ToList();
                if (!CurrentFilterString.IsAll)
                    result = result
                        .Where(q => CurrentFilterString.Filter.ToLower().Contains(q.Name.ToLower().First()))
                        .ToList();
            }
            return result.OrderBy(q => q.Name).ToList();
        }
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
        public Domain.Entities.Contact SelectedContact
        {
            get => _contactsStore.SelectedItem;
            set => _contactsStore.SelectedItem = value;
        }
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
        public ICommand CommandSetFilter { get; }
        #endregion
        public ContactsVM(ISender mediator, IStore<Domain.Entities.Contact> contactsStore, IStore<User> usersStore)
        {
            _mediator = mediator;
            _contactsStore = contactsStore;
            _usersStore = usersStore;

            _contactsStore.Changed += () => OnPropertyChanged(() => Contacts);
            //_usersStore.Changed += () => OnPropertyChanged(() => Users);

            CommandSetFilter = new Command(SetFilter);
            //CommandAddContact = new Command(_ => _navigationService.OpenDialog<AddContactVM>());
            //CommandUpdateContact = new Command(_ => _navigationService.OpenDialog<UpdateContactVM>(), obj => SelectedContact != null);

            //CommandRemoveContact = new Command(async _ =>
            //await _mediator.Send(
            //    new RemoveContactCommand()
            //    {
            //        Id = SelectedContact.Id
            //    }), obj => SelectedContact != null);
            CommandRemoveContact = new DelegateCommand(Remove);
            CreateFilterStrings();

            _mediator.Send(new GetContactsQuerry());
        }

        private void Remove(object obj)
        {
            throw new NotImplementedException();
        }

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
        private void SetFilter(object obj)
        {
            if (obj is FilterString filter)
            {
                FilterStrings = FilterStrings.Select(q => { q.IsChecked = false; return q; }).ToList();
                filter.IsChecked = true;
                CurrentFilterString = filter;
            }
        }
    }
}
