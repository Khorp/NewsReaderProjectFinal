using NewsReaderProject.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsReaderProject.MVVM.Model
{
    /// <summary>
    /// my group data class used for the list in groupview.
    /// </summary>
    public class Groups : ObjectUpdater
    {
        private string _group;

        public string Group
        {
            get { return _group; }
            set { _group = value; }
        }
        //favorite never get used for anything so all the groups are false.
        private bool _favorite;

        public bool Favorite
        {
            get { return _favorite; }
            set { _favorite = value; }
        }



    }
}
