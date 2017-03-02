using Budget.Enum;
using Budget.Helpers;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive;
using System.Windows.Controls;

namespace Budget.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private bool _isViewMode;
        public bool IsViewMode
        {
            get { return _isViewMode; }
            set { this.RaiseAndSetIfChanged(ref _isViewMode, value); }
        }
        private bool _isAddMode;
        public bool IsAddMode
        {
            get { return _isAddMode; }
            set { this.RaiseAndSetIfChanged(ref _isAddMode, value); }
        }
        private bool _isEditMode;
        public bool IsEditMode
        {
            get { return _isEditMode; }
            set { this.RaiseAndSetIfChanged(ref _isEditMode, value); }
        }
        private CalendarSelectionMode _selectionMode;
        public CalendarSelectionMode SelectionMode
        {
            get { return _selectionMode; }
            set { this.RaiseAndSetIfChanged(ref _selectionMode, value); }
        }
        private Nullable<DateTime> _selectedDate;
        public Nullable<DateTime> SelectedDate
        {
            get { return _selectedDate; }
            set { this.RaiseAndSetIfChanged(ref _selectedDate, value); }
        }
        private CalendarModeEnum _mode;
        public CalendarModeEnum Mode
        {
            get { return _mode; }
            set { this.RaiseAndSetIfChanged(ref _mode, value); }
        }
        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { this.RaiseAndSetIfChanged(ref _selectedTabIndex, value); }
        }
        private AddUserControlViewModel _addUserControlViewModel;
        public AddUserControlViewModel AddUserControlViewModel
        {
            get { return _addUserControlViewModel; }
            set { this.RaiseAndSetIfChanged(ref _addUserControlViewModel, value); }
        }
        public IObservable<Nullable<DateTime>> SelectedDateObservable;
        public MainWindowViewModel()
        {
            AddUserControlViewModel = AddUserControlViewModel.GetInstance();
            AddUserControlViewModel.Date = DateTimeHelper.PstNow();
            IsViewMode = true;
            SelectedTabIndex = 0;
            SelectionMode = CalendarSelectionMode.SingleRange;
            IObservable<bool> viewMode = this.WhenAnyValue(x => x.IsViewMode);
            IObservable<bool> editMode = this.WhenAnyValue(x => x.IsEditMode);
            IObservable<bool> addMode = this.WhenAnyValue(x => x.IsAddMode);
            SelectedDateObservable = this.WhenAnyValue(x => x.SelectedDate);
            IObserver<bool> viewModeObserver = Observer.Create<bool>(
                isChecked => ModeUpdate(CalendarModeEnum.View, isChecked),
                ex => HandleError(CalendarModeEnum.View, ex.Message),
                () => OnCompleted(CalendarModeEnum.View));
            IObserver<bool> editModeObserver = Observer.Create<bool>(
                isChecked => ModeUpdate(CalendarModeEnum.Edit, isChecked),
                ex => HandleError(CalendarModeEnum.Edit, ex.Message),
                () => OnCompleted(CalendarModeEnum.Edit));
            IObserver<bool> addModeObserver = Observer.Create<bool>(
                isChecked => ModeUpdate(CalendarModeEnum.Add, isChecked),
                ex => HandleError(CalendarModeEnum.Add, ex.Message),
                () => OnCompleted(CalendarModeEnum.Add));
            IObserver<Nullable<DateTime>> selectedDateObserver = Observer.Create<Nullable<DateTime>>(
                date => DisplaySelectedDate(date),
                ex => Debug.WriteLine("selectedDateObserver OnError {0}", ex.Message),
                () => Debug.WriteLine("selectedDateObserver OnCompleted"));
            viewMode.Subscribe(viewModeObserver);
            editMode.Subscribe(editModeObserver);
            addMode.Subscribe(addModeObserver);
            SelectedDateObservable.Subscribe(selectedDateObserver);
            SelectedDateObservable.Subscribe(AddUserControlViewModel.DateTimeObserver);
        }
        private void ModeUpdate(CalendarModeEnum mode, bool isChecked)
        {
            Debug.WriteLine("{0}Mode is {1} at {2}", mode, isChecked, DateTimeHelper.PstNow());
            if (isChecked)
            {
                Mode = mode;
                SetCalendarSelectionMode(mode);
                SetTab(mode);
            }
            Debug.WriteLine("CalendarSelectionMode is {0}", SelectionMode);
        }
        private void SetCalendarSelectionMode(CalendarModeEnum mode)
        {
            switch(mode)
            {
                case CalendarModeEnum.Add:
                case CalendarModeEnum.Edit:
                    SelectionMode = CalendarSelectionMode.SingleDate;
                    break;
                case CalendarModeEnum.View:
                default:
                    SelectionMode = CalendarSelectionMode.SingleRange;
                    break;
            }
        }
        private void SetTab(CalendarModeEnum mode)
        {
            switch(mode)
            {
                case CalendarModeEnum.Add:
                    SelectedTabIndex = 1;
                    break;
                case CalendarModeEnum.Edit:
                    SelectedTabIndex = 2;
                    break;
                case CalendarModeEnum.View:
                default:
                    SelectedTabIndex = 0;
                    break;
            }
            
        }
        private void HandleError(CalendarModeEnum mode, string exceptionMessage)
        {
            Debug.WriteLine("{0} OnError: {1}", mode, exceptionMessage);
        }
        private void OnCompleted(CalendarModeEnum mode)
        {
            Debug.WriteLine("{0}Mode completed", mode);
        }
        private void DisplaySelectedDate(Nullable<DateTime> selectedDate)
        {
            if (selectedDate.HasValue)
                Debug.WriteLine(String.Format("Selected Date is {0}", selectedDate.Value.ToShortDateString()));
            else
                Debug.WriteLine("Selected Date is null");
        }
    }
}
