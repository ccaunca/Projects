using Budget.Helpers;
using Budget.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace Budget.ViewModels
{
    public class AddUserControlViewModel : ReactiveObject
    {
        public ReactiveCommand AddCommand { get; private set; }
        private string _descriptionText;
        public string DescriptionText
        {
            get { return _descriptionText; }
            set { this.RaiseAndSetIfChanged(ref _descriptionText, value); }
        }
        private string _amountText;
        public string AmountText
        {
            get { return _amountText; }
            set { this.RaiseAndSetIfChanged(ref _amountText, value); }
        }
        private string _timeText;
        public string TimeText
        {
            get { return _timeText; }
            set { this.RaiseAndSetIfChanged(ref _timeText, value); }
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
                
            var canAdd = this.WhenAnyValue(x => x.DescriptionText, x => x.AmountText, (description, amount) => !string.IsNullOrEmpty(description) && !string.IsNullOrEmpty(amount));
            AddCommand = ReactiveCommand.Create(() => Add(Date, DescriptionText, AmountText, TimeText), canAdd);
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

        private IObservable<Unit> Add(DateTime date, string descriptionText, string amountText, string timeText)
        {
            return Observable.Start(() =>
            {
                try
                {
                    using (var context = new CarloniusEntities())
                    {
                        Budget_Transactions trans = new Budget_Transactions { Amount = Convert.ToDecimal(amountText), DateTime = Date, Description = descriptionText, CategoryID = 1, CreatedDate = DateTimeHelper.PstNow() };
                        context.Budget_Transactions.Add(trans);
                        context.SaveChanges();
                    }
                    if (string.IsNullOrEmpty(timeText))
                    {
                        Debug.WriteLine("TimeText is null or empty");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
    }
}