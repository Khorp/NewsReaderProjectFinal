using NewsReaderProject.Core;
using System;
using System.Collections.Generic;
using System.Text;
using NewsReaderProject.MVVM.Model;
using System.Windows.Input;
using NewsReaderProject.MVVM.View;
using Unity;

namespace NewsReaderProject.MVVM.ViewModel
{
    /// <summary>
    /// My homeviewmodel, it technicly is a loginview.
    /// it has the data for the login and is binded to the model:userdata.
    /// and i have a button for changing the view to the groupview and using the socket class
    /// for logining to the server.
    /// </summary>
    public class HomeViewModel : ObjectUpdater, IHomeViewModel
    {
        SocketHelper socketHelper = SocketCreator.getInstance();

        public UserData user = new UserData();
        public string Username
        {
            get { return user.Username; }
            set { user.Username = value; OnPropertyChanged(); }
        }
        public string Password
        {
            get { return user.Password; }
            set { user.Password = value; OnPropertyChanged(); }
        }
        public string NewsServer
        {
            get { return user.NewsServer; }
            set { user.NewsServer = value; OnPropertyChanged(); }
        }
        /// <summary>
        /// I only use the contructor for setting up the command and in the commmand login for the server.
        /// </summary>
        public HomeViewModel()
        {
            ChangePageCMD = new RelayCommand(() => {
                //Resolve will make sure to provide the need parameters
                ((App)App.Current).ChangeUserControl(App.container.Resolve<GroupView>());

                socketHelper.Connect(Username, Password, NewsServer);
            });
        }

        public ICommand ChangePageCMD { get; set; }
    }
}
