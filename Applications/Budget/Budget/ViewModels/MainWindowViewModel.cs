using Budget.Enum;
using Budget.Helpers;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Windows.Controls;

namespace Budget.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private bool _isViewMode;
        private bool _isAddMode;
        private bool _isEditMode;
        private CalendarSelectionMode _selectionMode;
        private Nullable<DateTime> _selectedDate;
        private IEnumerable<DateTime> _selectedDates;
        private CalendarModeEnum _mode;
        private int _selectedTabIndex;
        private AddUserControlViewModel _addUserControlViewModel;
        private EditUserControlViewModel _editUserControlViewModel;
        private ViewUserControlViewModel _viewUserControlViewModel;
        public bool IsViewMode
        {
            get { return _isViewMode; }
            set { this.RaiseAndSetIfChanged(ref _isViewMode, value); }
        }
        public bool IsAddMode
        {
            get { return _isAddMode; }
            set { this.RaiseAndSetIfChanged(ref _isAddMode, value); }
        }
        public bool IsEditMode
        {
            get { return _isEditMode; }
            set { this.RaiseAndSetIfChanged(ref _isEditMode, value); }
        }
        public CalendarSelectionMode SelectionMode
        {
            get { return _selectionMode; }
            set { this.RaiseAndSetIfChanged(ref _selectionMode, value); }
        }
        public Nullable<DateTime> SelectedDate
        {
            get { return _selectedDate; }
            set { this.RaiseAndSetIfChanged(ref _selectedDate, value); }
        }
        public IEnumerable<DateTime> SelectedDates
        {
            get { return _selectedDates; }
            set { this.RaiseAndSetIfChanged(ref _selectedDates, value); }
        }
        public CalendarModeEnum Mode
        {
            get { return _mode; }
            set { this.RaiseAndSetIfChanged(ref _mode, value); }
        }
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { this.RaiseAndSetIfChanged(ref _selectedTabIndex, value); }
        }
        public AddUserControlViewModel AddUserControlViewModel
        {
            get { return _addUserControlViewModel; }
            set { this.RaiseAndSetIfChanged(ref _addUserControlViewModel, value); }
        }
        public EditUserControlViewModel EditUserControlViewModel
        {
            get { return _editUserControlViewModel; }
            set { this.RaiseAndSetIfChanged(ref _editUserControlViewModel, value); }
        }
        public ViewUserControlViewModel ViewUserControlViewModel
        {
            get { return _viewUserControlViewModel; }
            set { this.RaiseAndSetIfChanged(ref _viewUserControlViewModel, value); }
        }
        public IObservable<Nullable<DateTime>> SelectedDateObservable;
        public IObservable<IEnumerable<DateTime>> SelectedDatesObservable;


        public MainWindowViewModel()
        {
            AddUserControlViewModel = AddUserControlViewModel.GetInstance();
            AddUserControlViewModel.Date = DateTimeHelper.PstNow();
            EditUserControlViewModel = EditUserControlViewModel.GetInstance();
            EditUserControlViewModel.Date = DateTimeHelper.PstNow();
            ViewUserControlViewModel = ViewUserControlViewModel.GetInstance();
            ViewUserControlViewModel.Date = DateTimeHelper.PstNow();
            IsViewMode = true;
            SelectedTabIndex = 0;
            SelectionMode = CalendarSelectionMode.SingleRange;
            IObservable<bool> viewMode = this.WhenAnyValue(x => x.IsViewMode);
            IObservable<bool> editMode = this.WhenAnyValue(x => x.IsEditMode);
            IObservable<bool> addMode = this.WhenAnyValue(x => x.IsAddMode);
            IObservable<int> tabSelection = this.WhenAnyValue(x => x.SelectedTabIndex);
            SelectedDateObservable = this.WhenAnyValue(x => x.SelectedDate);
            SelectedDatesObservable = this.WhenAnyValue(x => x.SelectedDates);
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
            IObserver<int> updateRadioButton = Observer.Create<int>(
                tabIndex => UpdateRadioButtonSelection(tabIndex));
            IObserver<IEnumerable<DateTime>> selectedDatesObserver = Observer.Create<IEnumerable<DateTime>>(
                dates => UpdateSelectedDates(dates),
                ex => Debug.WriteLine("selectedDateObserver OnError {0}", ex.Message),
                () => Debug.WriteLine("selectedDateObserver OnCompleted"));
            viewMode.Subscribe(viewModeObserver);
            editMode.Subscribe(editModeObserver);
            addMode.Subscribe(addModeObserver);
            SelectedDatesObservable.Subscribe(selectedDatesObserver);
            SelectedDateObservable.Subscribe(AddUserControlViewModel.DateTimeObserver);
            SelectedDateObservable.Subscribe(EditUserControlViewModel.DateTimeObserver);
            SelectedDateObservable.Subscribe(ViewUserControlViewModel.DateTimeObserver);
            tabSelection.Subscribe(updateRadioButton);
            SelectedDate = DateTimeHelper.PstNow();
        }

        private void UpdateRadioButtonSelection(int tabIdx)
        {
            switch(tabIdx)
            {                
                case 1:
                    IsAddMode = true;
                    SelectionMode = CalendarSelectionMode.SingleDate;
                    break;
                case 2:
                    IsEditMode = true;
                    SelectionMode = CalendarSelectionMode.SingleDate;
                    break;
                case 0:
                default:
                    IsViewMode = true;
                    SelectionMode = CalendarSelectionMode.MultipleRange;
                    break;
            }
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
        /// <summary>
        /// Used for debugging purposes
        /// </summary>
        /// <param name="selectedDate"></param>
        private void UpdateSelectedDates(IEnumerable<DateTime> selectedDates)
        {
            if (selectedDates != null)
            {
                
            }
        }
        public static void UpdateDataGrids(DateTime date)
        {
            EditUserControlViewModel.GetInstance().UpdateTransactions(date);
            ViewUserControlViewModel.GetInstance().UpdateTransactions(date);
        }
    }
}
