using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace NewsReaderProject.Core
{
    /// <summary>
    /// my objectupdater is basicly just franks bindable class, it is just 
    /// used for updating ui when it should be updated.
    /// </summary>
    public class ObjectUpdater : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
