using System;
using System.Collections.Generic;
using System.Text;

namespace NewsReaderProject.MVVM.Model
{
    /// <summary>
    /// my singolton of the articletext.
    /// </summary>
   public static class ArticleTextCreator
    {
        private static ArticleText instance;
        public static ArticleText GetArticleText()
        {
            if (instance == null)
            {
                instance = new ArticleText();
            }
            return instance;
        }
    }
}
