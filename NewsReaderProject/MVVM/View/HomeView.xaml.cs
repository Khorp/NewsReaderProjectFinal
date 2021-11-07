using NewsReaderProject.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewsReaderProject.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private IHomeViewModel viewModel = null;
        public HomeView(IHomeViewModel iHomeViewModel)
        {
            viewModel = iHomeViewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
