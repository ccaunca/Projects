using Budget.Helpers;
using Budget.Models;
using Budget.Views;
using ReactiveUI;
using RoyT.TimePicker;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;

namespace Budget.ViewModels
{
    public class AddUserControlViewModel : ReactiveObject
    {
        public ReactiveCommand AddCommand { get; private set; }
        public ReactiveCommand AddCategoryCommand { get; private set; }
        private readonly ObservableAsPropertyHelper<bool> _isAdding;
        public bool IsAdding { get { return _isAdding.Value; } }
        private string _descriptionText;
        public string DescriptionText
        {
            get { return _descriptionText; }
            set { this.RaiseAndSetIfChanged(ref _descriptionText, value); }
        }
        private decimal _transactionAmount;
        public decimal TransactionAmount
        {
            get { return _transactionAmount; }
            set { this.RaiseAndSetIfChanged(ref _transactionAmount, value); }
        }
        private DigitalTime _minTime;
        public DigitalTime MinTime
        {
            get { return _minTime; }
            set { this.RaiseAndSetIfChanged(ref _minTime, value); }
        }
        private DigitalTime _time;
        public DigitalTime Time
        {
            get { return _time; }
            set { this.RaiseAndSetIfChanged(ref _time, value); }
        }
        private DigitalTime _maxTime;
        public DigitalTime MaxTime
        {
            get { return _maxTime; }
            set { this.RaiseAndSetIfChanged(ref _maxTime, value); }
        }
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { this.RaiseAndSetIfChanged(ref _date, value); }
        }
        private List<Budget_GetAllCategories_Result> _categories;
        public List<Budget_GetAllCategories_Result> Categories
        {
            get { return _categories; }
            set { this.RaiseAndSetIfChanged(ref _categories, value); }
        }
        private int _selectedCategory;
        public int SelectedCategory
        {
            get { return _selectedCategory; }
            set { this.RaiseAndSetIfChanged(ref _selectedCategory, value); }
        }
        private static AddUserControlViewModel _instance;
        public static AddUserControlViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AddUserControlViewModel();
            }
            return _instance;
        }
        public IObserver<Nullable<DateTime>> DateTimeObserver;
        public void GetAllCategories()
        {
            Categories = CarloniusRepository.GetAllCategories();
        }
        private AddUserControlViewModel()
        {
            Time = new DigitalTime(0, 0);
            MinTime = new DigitalTime(0, 0);
            MaxTime = new DigitalTime(23, 59);

            DateTimeObserver = Observer.Create<Nullable<DateTime>>(
                date => UpdateDate(date),
                ex => HandleException(ex),
                () => Debug.WriteLine("AddUserControlViewModel dateTimeObserver OnCompleted.")
            );
            GetAllCategories();
            var canAdd = this.WhenAnyValue(x => x.DescriptionText, x => x.TransactionAmount,
                (description, amount) => !string.IsNullOrEmpty(description) && amount > 0);
            AddCommand = ReactiveCommand.Create(() => AddTransaction(), canAdd);
            AddCommand.IsExecuting.ToProperty(this, x => x.IsAdding, out _isAdding);
            AddCategoryCommand = ReactiveCommand.Create(() => AddCategory());
            IObservable<bool> addObservable = this.WhenAnyValue(x => x.IsAdding);
            IObserver<bool> addObserver = Observer.Create<bool>(x => UpdateUI());
            addObservable.Subscribe(addObserver);
        }

        private void AddCategory()
        {
            AddCategoryWindow window = new AddCategoryWindow();
            window.Show();
        }

        private void UpdateUI()
        {   // Completion upon adding transaction
            if (!IsAdding && TransactionAmount != 0)
            {   // TODO: This will display even if there's an exception in AddTransaction, need to fix this
                MessageBox.Show("Add Successful!", "Transaction Add", MessageBoxButton.OK, MessageBoxImage.None);
            }
        }
        private void UpdateDate(DateTime? date)
        {
            if (date.HasValue)
                Date = date.Value;
        }
        private void HandleException(Exception ex)
        {
            Debug.WriteLine("Exception occurred {0}", ex.Message);
            throw ex;
        }

        private IObservable<Unit> AddTransaction()
        {
            return Observable.Start(() =>
            {
                CarloniusRepository.AddTransaction(new Budget_Transactions { Amount = TransactionAmount, DateTime = DateTimeHelper.SetDateTime(Date, Time), Description = DescriptionText, CategoryID = SelectedCategory, CreatedDate = DateTimeHelper.PstNow() });
            });
        }
    }
}