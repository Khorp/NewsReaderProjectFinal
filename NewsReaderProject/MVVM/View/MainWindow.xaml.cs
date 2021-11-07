using NewsReaderProject.MVVM.View;
using System.Windows;
using Unity;
namespace NewsReaderProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ((App)App.Current).ContentControlRef = this.mainContent;

            //((App)App.Current).ChangeUserControl(container.Resolve<BlueView>());
            ((App)App.Current).ChangeUserControl(App.container.Resolve<HomeView>());
        }
    }
}
