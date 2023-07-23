using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.Stores
{
    public abstract class Store<T> : IStore<T> where T : BaseEntity
    {
        public virtual event Action Changed;
        public virtual event Action SelectedItemChanged;
        public virtual List<T> Collection { get; } = new List<T>();
        public virtual T SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                SelectedItemChanged?.Invoke();
            }
        }
        private T _selectedItem;
        public virtual void AddItem(T item)
        {
            AddItems(new[] { item });
        }
        public virtual void AddItems(ICollection<T> contacts)
        {
            Collection.AddRange(contacts);
            Changed?.Invoke();
        }
        public virtual void RemoveItem(Guid id)
        {
            var itemToRemove = Collection.FirstOrDefault(x => x.Id == id);
            if (itemToRemove != null)
            {
                Collection.Remove(itemToRemove);
                Changed?.Invoke();
            }
        }
        public virtual void ReplaceItem(T oldItem, T newItem)
        {
            var index = Collection.IndexOf(oldItem);
            Collection.RemoveAt(index);
            Collection.Insert(index, newItem);
            Changed?.Invoke();
        }
        public virtual void ClearCollection()
        {
            Collection.Clear();
            Changed?.Invoke();
        }
    }
}
