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
    /// My Posting views model, it has the commands for changing view and posting. and the data for the 
    /// views textblocks i also sure my socket class to post here.
    /// </summary>
    public class PostingViewModel : ObjectUpdater,IPostingViewModel
    {
        public ICommand ChangePageCMD { get; set; }
        public ICommand Post { get; set; }
        public SocketHelper socketHelper = SocketCreator.getInstance();

        public SaveGroup saveGroup = SaveGroupCreator.GetSaveGroup();

        private string _from;
        public string From
        {
            get { return _from; }
            set { _from = value; OnPropertyChanged(); }
        }
        private string _subject;
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; OnPropertyChanged(); }
        }
        public string NewsGroup
        {
            get { return saveGroup.Group; }
        }
        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }


        /// <summary>
        /// my constructor, here i can change view back to the groupview, and i can post an article and after i have posted
        /// an article i make all expect for the group empty and then i change the view back to the group
        /// </summary>
        public PostingViewModel()
        {
            ChangePageCMD = new RelayCommand(()=> 
            {
                ((App)App.Current).ChangeUserControl(App.container.Resolve<GroupView>());
            });
            Post = new RelayCommand(()=> 
            {
                string group1 = saveGroup.Group.Split(' ')[0].Replace(" ", string.Empty);
                //post Command
                socketHelper.PostArticle(From,group1,Subject,Message);

                From = "";Subject = "";Message = "";

                ((App)App.Current).ChangeUserControl(App.container.Resolve<GroupView>());
            });
        }
    }
}
