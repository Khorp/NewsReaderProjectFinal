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
    /// Interaction logic for PostingView.xaml
    /// </summary>
    public partial class PostingView : UserControl
    {
        private IPostingViewModel viewmodel;
        public PostingView(IPostingViewModel ipostingViewModel)
        {
            viewmodel = ipostingViewModel;
            this.DataContext = viewmodel;
            InitializeComponent();
        }
    }
}
