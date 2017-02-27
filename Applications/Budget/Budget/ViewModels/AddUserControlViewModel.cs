using Budget.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private List<Budget_Categories> _categories;
        public List<Budget_Categories> Categories
        {
            get { return _categories; }
            set { this.RaiseAndSetIfChanged(ref _categories, value); }
        }

        public AddUserControlViewModel()
        {
            Categories = 
            var canAdd = this.WhenAnyValue(x => x.DescriptionText, x => x.AmountText, (description, amount) => !string.IsNullOrEmpty(description) && !string.IsNullOrEmpty(amount));
            AddCommand = ReactiveCommand.Create(() => Add(Date, DescriptionText, AmountText, TimeText), canAdd);
        }

        private IObservable<Unit> Add(DateTime date, string descriptionText, string amountText, string timeText)
        {
            if (string.IsNullOrEmpty(timeText))
            {
                Debug.WriteLine("TimeText is null or empty");
            }
            return Observable.Start((Action)delegate() { Debug.WriteLine("Add Command executed!"); });
        }
    }
}