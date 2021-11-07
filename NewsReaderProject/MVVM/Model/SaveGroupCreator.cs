using System;
using System.Collections.Generic;
using System.Text;

namespace NewsReaderProject.MVVM.Model
{
    /// <summary>
    /// My singolton for getting only 1 instance of savegroup.
    /// </summary>
    public static class SaveGroupCreator
    {
        private static SaveGroup instance;
        public static SaveGroup GetSaveGroup()
        {
            if (instance == null)
            {
                instance = new SaveGroup();
            }
            return instance;
        }
    }
}
