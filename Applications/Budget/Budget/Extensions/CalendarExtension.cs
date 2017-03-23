using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Budget.Extensions
{
    public class CalendarExtensions
    {
        public static bool GetTrackCalendarSelectedDates(DependencyObject obj)
        {
            return (bool)obj.GetValue(TrackCalendarSelectedDatesProperty);
        }

        public static void SetTrackCalendarSelectedDates(DependencyObject obj, bool value)
        {
            obj.SetValue(TrackCalendarSelectedDatesProperty, value);
        }

        public static readonly DependencyProperty TrackCalendarSelectedDatesProperty =
            DependencyProperty.RegisterAttached("TrackCalendarSelectedDates", typeof(bool), typeof(CalendarExtensions), new PropertyMetadata(false, OnTrackCalendarSelectedDatesPropertyChanged));

        public static IEnumerable<DateTime> GetSelectedDates(DependencyObject obj)
        {
            return (IEnumerable<DateTime>)obj.GetValue(SelectedDatesProperty);
        }

        public static void SetSelectedDates(DependencyObject obj, IEnumerable<DateTime> value)
        {
            obj.SetValue(SelectedDatesProperty, value);
        }

        public static readonly DependencyProperty SelectedDatesProperty =
            DependencyProperty.RegisterAttached("SelectedDates", typeof(IEnumerable<DateTime>), typeof(CalendarExtensions), new PropertyMetadata(Enumerable.Empty<DateTime>(), OnSelectedDatesPropertyChanged));

        private static CalendarExtensions GetExtensions(DependencyObject obj)
        {
            return (CalendarExtensions)obj.GetValue(ExtensionsProperty);
        }

        private static void SetExtensions(DependencyObject obj, CalendarExtensions value)
        {
            obj.SetValue(ExtensionsProperty, value);
        }

        private static readonly DependencyProperty ExtensionsProperty =
            DependencyProperty.RegisterAttached("Extensions", typeof(CalendarExtensions), typeof(CalendarExtensions), null);

        private static void OnSelectedDatesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var extensions = GetExtensions(d);
            if (extensions != null)
            {
                extensions.UpdateCalendarSelectedDates(GetSelectedDates(d));
            }
        }

        private static void OnTrackCalendarSelectedDatesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var calendar = d as Calendar;
            if (calendar != null)
            {
                if ((bool)e.NewValue)
                {
                    var extensions = new CalendarExtensions(calendar);
                    SetExtensions(calendar, extensions);
                    extensions.UpdateSelectedDates();
                }
                else
                {
                    GetExtensions(calendar).Detach();
                    calendar.ClearValue(ExtensionsProperty);
                }
            }
        }

        private Calendar _calendar;
        private bool updatingSelectedDates;
        private CalendarExtensions(Calendar calendar)
        {
            _calendar = calendar;
            _calendar.SelectedDatesChanged += OnCalendarSelectionChanged;
        }

        private void Detach()
        {
            _calendar.SelectedDatesChanged -= OnCalendarSelectionChanged;
        }

        private void OnCalendarSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSelectedDates();
        }

        private void UpdateSelectedDates()
        {
            if (!updatingSelectedDates)
            {
                updatingSelectedDates = true;
                SetSelectedDates(_calendar, _calendar.SelectedDates);
                Debug.WriteLine("Selected Dates are:");
                foreach (DateTime selectedDate in _calendar.SelectedDates)
                {
                    Debug.WriteLine(selectedDate.ToShortDateString());
                }
                updatingSelectedDates = false;
            }
        }

        private void UpdateCalendarSelectedDates(IEnumerable<DateTime> dates)
        {
            if (!updatingSelectedDates)
            {
                updatingSelectedDates = true;
                _calendar.SelectedDates.Clear();
                if (dates != null)
                {
                    foreach (var date in dates)
                    {
                        _calendar.SelectedDates.Add(date);
                    }
                }
                updatingSelectedDates = false;
            }
        }
    }
}
