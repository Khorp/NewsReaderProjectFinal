using NewsReaderProject.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsReaderProject.MVVM.Model
{
    /// <summary>
    /// a data class for the articles text.
    /// </summary>
    public class ArticleText : ObjectUpdater
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged(); }
        }

    }
}
