using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// <para>
///     In the MVVM architecture, ViewModels are the interface between the view and the data models for the app.
///     The idea is to decouple each of these architectural layers as much as possible so that we can use them in
///     different contexts or extend them to suit different needs.
/// </para>
/// <para>
///     This class is the base class to be inherited by all other ViewModels in the app.
///     This allows for a uniform implementation of the INotifyPropertyChanged interface between all VMs.
/// </para>
/// </summary>
namespace ContactsApp.BaseClasses
{
    class ViewModelBase : NotifyPropertyChanges
    {
    }
}
