﻿using Contacts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Application.Common.Interfaces
{
    public interface IStore<T>
    {
        event Action Changed;
        event Action SelectedItemChanged;
        List<T> Collection { get; }
        T SelectedItem { get; set; }
        void AddItem(T contact);
        void AddItems(ICollection<T> contacts);
        void RemoveItem(Guid id);
        void ReplaceItem(T oldItem, T newItem);
        void ClearCollection();
    }
}
