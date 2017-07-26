using ContactsApp.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsApp.Models;
using ContactsApp.Common;
using System.Collections.ObjectModel;

namespace ContactsApp.ViewModels
{
    class ContactListViewModel : ViewModelBase
    {
        private ObservableCollection<Contact> _contactList = null;

        public ContactListViewModel() : base()
        {
            _contactList = new ObservableCollection<Contact> {
                new Contact("Bob Barker"),
                new Contact("Joe Dentinger"),
                new Contact("Alison Bjorker")
            };
        }

        public ObservableCollection<Contact> ContactList
        {
            get { return this._contactList; }
            set { this._contactList = value; }
        }
    }
}
