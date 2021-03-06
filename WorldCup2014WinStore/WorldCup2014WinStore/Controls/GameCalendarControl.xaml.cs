﻿using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Utility;

namespace WorldCup2014WinStore.Controls
{
    public sealed partial class GameCalendarControl : UserControl
    {
        public GameCalendarControl()
        {
            InitializeComponent();
            this.Loaded += GameCalendarControl_Loaded;
        }

        void GameCalendarControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCalendar();
        }

        #region Calendar

        ListDataLoader<CalendarItem> calendarLoader = new ListDataLoader<CalendarItem>();

        public void LoadCalendar()
        {
            if (calendarLoader.Busy)
            {
                return;
            }

            //snow1.IsBusy = true;

            calendarLoader.Load("getcalendar", string.Empty, true, Constants.CALENDAR_MODULE, Constants.CALENDAR_FILE_NAME,
                result =>
                {
                    PopulateCalendar(result);
                    //snow1.IsBusy = false;
                });
        }

        #endregion

        List<DateTime> months = new List<DateTime>();
        Dictionary<DateTime, GameCalendarMonthControl> monthAndControls = new Dictionary<DateTime, GameCalendarMonthControl>();
        private void PopulateCalendar(List<CalendarItem> items)
        {
            months.Clear();
            monthAndControls.Clear();

            foreach (var item in items)
            {
                DateTime month = new DateTime(item.Date.Year, item.Date.Month, 1);
                if (!months.Contains(month))
                {
                    months.Add(month);
                    GameCalendarMonthControl newMonthControl = new GameCalendarMonthControl();
                    newMonthControl.DataContext = month;
                    monthAndControls.Add(month, newMonthControl);
                    newMonthControl.DayTapped += monthControl_DayTapped;

                    //PivotItem pivotItem = new PivotItem() { Header = month.ToString("yyyy年MM月") };
                    //pivotItem.Content = newMonthControl;
                    //pivot.Items.Add(pivotItem);

                    flipView.Items.Add(newMonthControl);
                }

                //GameCalendarMonthControl monthControl = monthAndControls[month];
                //monthControl.AddDay(item);
            }

            foreach (var month in months)
            {
                PopulateMonth(month, monthAndControls[month], items);
            }
        }

        private void PopulateMonth(DateTime month, GameCalendarMonthControl monthControl, List<CalendarItem> items)
        {
            DateTime nextMonth = month.AddMonths(1);
            DateTime dt = month;
            while (dt < nextMonth)
            {
                CalendarItem item = new CalendarItem() { Date = dt, Football = "0" };
                var sameDay = items.FirstOrDefault(x => x.Date.Year == dt.Year && x.Date.Month == dt.Month && x.Date.Day == dt.Day);
                if (sameDay != null)
                {
                    item.Football = sameDay.Football;
                }
                monthControl.AddDay(item);
                dt = dt.AddDays(1);
            }
        }

        public event EventHandler<TappedRoutedEventArgs> DayTapped;
        void monthControl_DayTapped(object sender, TappedRoutedEventArgs e)
        {
            if (DayTapped != null)
            {
                DayTapped(sender, e);
            }
        }
    }
}
