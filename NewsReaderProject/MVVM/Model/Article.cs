using System;
using System.Collections.Generic;
using System.Text;

namespace NewsReaderProject.MVVM.Model
{
    /// <summary>
    /// my article class is used for the list in the groupview. it just gets the headline and the id.
    /// </summary>
    public class Article
    {
        private string _headline;

        public string Headline
        {
            get { return _headline; }
            set { _headline = value; }
        }
        private string _articleID;
        public string ArticleID 
        {
            get { return _articleID; }
            set { _articleID = value; }
        }

    }
}
