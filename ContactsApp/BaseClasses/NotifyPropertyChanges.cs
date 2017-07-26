using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.BaseClasses
{
    /// <summary>
    /// <para>
    ///     This base class is an adaptation of 
    ///     <a href="https://rehansaeed.com/model-view-viewmodel-mvvm-part1-overview/">Muhammad Rehan Saeed</a>'s version
    /// </para>
    /// </summary>
    public abstract class NotifyPropertyChanges : INotifyPropertyChanged, INotifyPropertyChanging
    {

        private event PropertyChangedEventHandler _propertyChanged = null;

        private event PropertyChangingEventHandler _propertyChanging = null;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { this._propertyChanged += value; }
            remove { this._propertyChanged -= value; }
        }

        event PropertyChangingEventHandler INotifyPropertyChanging.PropertyChanging
        {
            add { this._propertyChanging += value; }
            remove { this._propertyChanging -= value; }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this._propertyChanged;
            if (handler != null)
                // if handler has a subscriber, raise the event to the handler
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(params string[] names)
        {
            if (names == null)
                throw new ArgumentNullException(
                    "OnPropertyChanged was called with a null argument!"
                    );
            foreach (string name in names)
                this.OnPropertyChanged(name);
        }

        protected bool SetProperty<T>(
            ref T currentPropValue, 
            T newValue, 
            [CallerMemberName] string propName = null)
        {
            if(!object.Equals(currentPropValue, newValue))
            {
                currentPropValue = newValue;
                this.OnPropertyChanged(propName);
                return true;
            }
            return false;
        }

        protected bool SetProperty<TProp>(
            ref TProp currentValue,
            TProp newValue,
            params string[] propertyNames)
        {
            if (!object.Equals(currentValue, newValue))
            {
                currentValue = newValue;
                this.OnPropertyChanged(propertyNames);

                return true;
            }

            return false;
        }

        protected bool SetProperty(
            Func<bool> equal,
            Action action,
            [CallerMemberName] string propertyName = null)
        {
            if (equal())
            {
                return false;
            }

            action();
            this.OnPropertyChanged(propertyName);

            return true;
        }

        protected bool SetProperty(
            Func<bool> equal,
            Action action,
            params string[] propertyNames)
        {
            if (equal())
            {
                return false;
            }

            action();
            this.OnPropertyChanged(propertyNames);

            return true;
        }

    }
}
