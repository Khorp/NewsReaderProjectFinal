using NewsReaderProject.Core;
using NewsReaderProject.MVVM.Model;
using NewsReaderProject.MVVM.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Unity;

namespace NewsReaderProject.MVVM.ViewModel
{
    /// <summary>
    /// This is the viewmodel used to read the article.
    /// we have a changeview command and a databinded textblock for the articletext.
    /// </summary>
    public class ArticleReadingVieModel : ObjectUpdater, IArticleReadingViewModel
    {

        public ICommand ChangePageCMD { get; set; }

        public ArticleText articleText = ArticleTextCreator.GetArticleText();
        
        public string ArticleText
        {
            get { return articleText.Text; }
            set { articleText.Text = value; }
        }

        public ArticleReadingVieModel()
        {
            ChangePageCMD = new RelayCommand(()=> 
            {
                ((App)App.Current).ChangeUserControl(App.container.Resolve<GroupView>());
            });
        }
    }
}
