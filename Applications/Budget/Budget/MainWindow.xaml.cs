using Budget.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Budget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = MainWindowViewModel.GetInstance();
        }
    }
}