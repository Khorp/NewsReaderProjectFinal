﻿using NewsReaderProject.MVVM.ViewModel;
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
using Unity;

namespace NewsReaderProject.MVVM.View
{
    /// <summary>
    /// Interaction logic for ArticleReadingView.xaml
    /// </summary>
    public partial class ArticleReadingView : UserControl
    {
        private IArticleReadingViewModel viewmodel = null;
        public ArticleReadingView(IArticleReadingViewModel iarticleReadingViewModel)
        {
            viewmodel = iarticleReadingViewModel;
            this.DataContext = viewmodel;
            InitializeComponent();
        }
    }
}
