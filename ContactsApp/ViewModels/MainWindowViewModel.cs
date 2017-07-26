using ContactsApp.BaseClasses;
using ContactsApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ContactsApp.ViewModels
{
    class MainWindowViewModel : ViewModelBase 
    {
        private ViewModelBase _currentViewModel;

        public MainWindowViewModel()
        { _currentViewModel = new ContactListViewModel(); }

        public ICommand DisplayContactsViewModel
        {
            get
            {
                return new RelayCommand(
                    param => !(this._currentViewModel is ContactListViewModel),
                    param => this._currentViewModel = new ContactListViewModel()
                    );
            }
        }

        public ViewModelBase CurrentViewModel
        {
            get { return this._currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }
    }
}
