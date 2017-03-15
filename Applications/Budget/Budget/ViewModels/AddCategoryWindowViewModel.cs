using Budget.Helpers;
using Budget.Models;
using ReactiveUI;
using System.Windows;

namespace Budget.ViewModels
{
    public class AddCategoryWindowViewModel : ReactiveObject
    {
        private string _category;
        public string NewCategoryText
        {
            get { return _category; }
            set { this.RaiseAndSetIfChanged(ref _category, value); }
        }
        public ReactiveCommand AddCategoryCommand { get; private set; }
        public AddCategoryWindowViewModel()
        {
            var canAddCategory = this.WhenAnyValue(x => x.NewCategoryText, (category) => !string.IsNullOrEmpty(category) && !DoesAlreadyExist(category));
            AddCategoryCommand = ReactiveCommand.Create(() => AddCategory(), canAddCategory);
        }
        private bool DoesAlreadyExist(string category)
        {
            var alreadyExists = CarloniusRepository.GetCategory(category);
            return alreadyExists != null;
        }
        private void AddCategory()
        {
            CarloniusRepository.InsertCategory(new Budget_Categories { Category = NewCategoryText, DateTime = DateTimeHelper.PstNow() });
            MessageBox.Show("Add Successful!", "Category Add", MessageBoxButton.OK, MessageBoxImage.None);
            AddUserControlViewModel addVM = AddUserControlViewModel.GetInstance();
            addVM.GetAllCategories();
        }
    }
}
