using NewsReaderProject.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsReaderProject.MVVM.Model
{
    /// <summary>
    /// my savegroup is a data class used for getting what group i am touching.
    /// so i can parse it between classes.
    /// </summary>
    public  class SaveGroup : ObjectUpdater
    {
        private string _group;

        public string Group
        {
            get { return _group; }
            set { _group = value; OnPropertyChanged(); }
        }

    }
}
