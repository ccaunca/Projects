using Budget.Helpers;
using Budget.Models;
using ReactiveUI;
using RoyT.TimePicker;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;

namespace Budget.ViewModels
{
    public class AddUserControlViewModel : ReactiveObject
    {
        public ReactiveCommand AddCommand { get; private set; }
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
        private List<string> _categories;
        public List<string> Categories
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
            Categories = new List<string>();
            using (var context = new CarloniusEntities())
            {
                ObjectResult<Budget_GetAllCategories_Result> resultSet = context.Budget_GetAllCategories();
                List<Budget_GetAllCategories_Result> resultList = (from cat in resultSet select cat).ToList();
                foreach(Budget_GetAllCategories_Result result in resultList)
                {
                    Categories.Add(result.Category);
                }
            }
                
            var canAdd = this.WhenAnyValue(x => x.DescriptionText, x => x.TransactionAmount, (description, amount) => !string.IsNullOrEmpty(description) && amount > 0);
            AddCommand = ReactiveCommand.Create(() => AddTransaction(), canAdd);
            AddCommand.IsExecuting.ToProperty(this, x => x.IsAdding, out _isAdding);
            IObservable<bool> addObservable = this.WhenAnyValue(x => x.IsAdding);
            IObserver<bool> addObserver = Observer.Create<bool>(x => UpdateUI());
            IObservable<int> selectedCategory = this.WhenAnyValue(x => x.SelectedCategory);
            IObserver<int> updateSelectedCategory = Observer.Create<int>(x => Debug.WriteLine("Selected Category = {0}", x));
            addObservable.Subscribe(addObserver);
            selectedCategory.Subscribe(updateSelectedCategory);
        }
        private void UpdateUI()
        {
            
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
                try
                {
                    using (var context = new CarloniusEntities())
                    {

                        Budget_Transactions trans = new Budget_Transactions { Amount = TransactionAmount, DateTime = SetDateTime(Date, Time), Description = DescriptionText, CategoryID = SelectedCategory, CreatedDate = DateTimeHelper.PstNow() };
                        context.Budget_Transactions.Add(trans);
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        private DateTime SetDateTime(DateTime date, DigitalTime time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
        }
    }
}