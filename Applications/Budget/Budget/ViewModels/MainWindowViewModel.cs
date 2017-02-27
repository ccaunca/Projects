using Budget.Base;
using Budget.Enum;
using Budget.Helpers;
using Budget.UserControls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        private bool _isAddMode = false;
        public bool IsAddMode
        {
            get { return _isAddMode; }
            set { this.RaiseAndSetIfChanged(ref _isAddMode, value); }
        }
        private bool _isEditMode = false;
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
        private ObservableCollection<TabItem> _tabs;
        public ObservableCollection<TabItem> Tabs
        {
            get { return _tabs; }
            set { this.RaiseAndSetIfChanged(ref _tabs, value); }
        }

        public MainWindowViewModel()
        {
            IsViewMode = true;
            SelectionMode = CalendarSelectionMode.SingleRange;
            Tabs = new ObservableCollection<TabItem>();
            Tabs.Add(new TabItem { Header = CalendarModeEnum.View, Content = new ViewUserControl(), IsSelected = IsViewMode, IsEnabled = false });
            Tabs.Add(new TabItem { Header = CalendarModeEnum.Add, Content = new AddUserControl(), IsSelected = IsAddMode, IsEnabled = false });
            Tabs.Add(new TabItem { Header = CalendarModeEnum.Edit, Content = new EditUserControl(), IsSelected = IsEditMode, IsEnabled = false });
            IObservable<bool> viewMode = this.WhenAnyValue(x => x.IsViewMode);
            IObservable<bool> editMode = this.WhenAnyValue(x => x.IsEditMode);
            IObservable<bool> addMode = this.WhenAnyValue(x => x.IsAddMode);
            IObservable<Nullable<DateTime>> selectedDate = this.WhenAnyValue(x => x.SelectedDate);
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
            selectedDate.Subscribe(selectedDateObserver);
            
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
                    Tabs[0].IsSelected = false;
                    Tabs[1].IsSelected = true;
                    Tabs[2].IsSelected = false;
                    break;
                case CalendarModeEnum.Edit:
                    Tabs[0].IsSelected = false;
                    Tabs[1].IsSelected = false;
                    Tabs[2].IsSelected = true;
                    break;
                case CalendarModeEnum.View:
                default:
                    Tabs[0].IsSelected = true;
                    Tabs[1].IsSelected = false;
                    Tabs[2].IsSelected = false;
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
