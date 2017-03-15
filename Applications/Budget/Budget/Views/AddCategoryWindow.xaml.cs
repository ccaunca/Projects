using Budget.ViewModels;
using System.Windows;

namespace Budget.Views
{
    /// <summary>
    /// Interaction logic for AddCategoryWindow1.xaml
    /// </summary>
    public partial class AddCategoryWindow : Window
    {
        public AddCategoryWindow()
        {
            InitializeComponent();
            DataContext = new AddCategoryWindowViewModel();
        }
    }
}
