using NewsReaderProject.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using NewsReaderProject.MVVM.Model;
using NewsReaderProject.MVVM.View;
using Unity;
using System.Collections.ObjectModel;
using System.Windows;
using System.Threading.Tasks;
using System.Threading;

namespace NewsReaderProject.MVVM.ViewModel
{
    /// <summary>
    /// My groupviewmodel used for setting up some commands, and having the lists for the
    /// groups, and articles. aslo have some binded itemSelect that is used for knowing what group or article
    /// so i know what to open with the socket class
    /// </summary>
    public class GroupViewModel : ObjectUpdater, IGroupViewModel
    {
        public ICommand ChangePageCMD { get; set; }
        public bool PopulateListbool { get; set; } = true;

        public SocketHelper socketHelper = SocketCreator.getInstance();

        private Groups _selectedItem;
        public Groups SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        private Article _selectedArticle;
        public Article SelectedArticle
        {
            get { return _selectedArticle; }
            set { _selectedArticle = value; OnPropertyChanged(); }
        }

        public ICommand PopulateList { get; set; }

        private ObservableCollection<Groups> _grouplist = new ObservableCollection<Groups>();
        
        public ObservableCollection<Groups> Grouplist
        {
            get { return _grouplist; }
            set { _grouplist = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Article> _articles = new ObservableCollection<Article>();
        public ObservableCollection<Article> Articles
        {
            get { return _articles; }
            set { _articles = value; OnPropertyChanged(); }
        }
        //article text is the class i use to save the text from an article so when i open a view for reading that
        //i can databind it in.
        public ArticleText articleText = ArticleTextCreator.GetArticleText();
        /// <summary>
        /// Used for the posting, so the postingview model knows what group i have clicked.
        /// </summary>
        public SaveGroup saveGroup = SaveGroupCreator.GetSaveGroup();
        public ICommand OpenArticleList { get; set; }
        public ICommand ReadArticle { get; set; }
        public ICommand GotoPosting { get; set; }
        /// <summary>
        /// my constructor used for changing view to posting and logout so if you want to login again
        /// </summary>
        public GroupViewModel()
        {
            GotoPosting = new RelayCommand(() =>
            {
                saveGroup.Group = SelectedItem.Group;
                ((App)App.Current).ChangeUserControl(App.container.Resolve<PostingView>());
            });
            // used to make the grouplist filled, it starts a thread so my ui dosent freeze when i activate the commmands.
            PopulateList = new RelayCommand(() =>
            {
                //i clear the list so the ui should show that is gets populated.
                Grouplist.Clear();
                Thread thread = new Thread(() =>
                {
                    PopulateListbool = false;
                    Grouplist = populateList();
                });

                thread.Start();
                //this is so i can do some code when the thread is done, but dosent really work.
                //most likely a databind error.
                Task.Run(() => 
                {
                    thread.Join();
                    PopulateListbool = true;
                });

            });
            //used to logout. and go back to the login view
            ChangePageCMD = new RelayCommand(() =>
            {
                ((App)App.Current).ChangeUserControl(App.container.Resolve<HomeView>());

                socketHelper.Quit();

            });
            //used for getting all the headlines from a group
            OpenArticleList = new RelayCommand(() =>
            {
                // i remove the 0000232 0000032 thing from the group
                string SimplifiedGroup = SelectedItem.Group.Split(' ')[0].Replace(" ", string.Empty);
                //start a thead so the view dosent freeze
                Thread thread = new Thread(() =>
                {
                    Articles = socketHelper.GiveArticles(SimplifiedGroup);
                });

                thread.Start();

            });
            ReadArticle = new RelayCommand(() =>
            {
                //socket read article
                string ArticleText = socketHelper.ReadArticle(SelectedArticle);
                //i set the article text as the text from the article.
                articleText.Text = ArticleText;
                //then i change view.
                ((App)App.Current).ChangeUserControl(App.container.Resolve<ArticleReadingView>());

            });
        }
        /// <summary>
        /// This is the populate command it runs the socket command for getting the groups and then it
        /// converts into a group list instead.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Groups> populateList()
        {
            ObservableCollection<string> groups = socketHelper.GiveGroups();

            //List<string> groups = socketHelper.GiveGroups();

            ObservableCollection<Groups> tempG = new ObservableCollection<Groups>();
            foreach (var item in groups)
            {
                if (item != null || !item.Contains("281")
                    || !item.Contains("215") || item != "281")
                {
                    tempG.Add(new Groups { Group = item, Favorite = false });
                }
            }
            return tempG;
        }
    }
}
