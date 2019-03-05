using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebStore.Entities.ViewModels.Admin
{
    public class FilterViewModel
    {
        public FilterViewModel(DayOfWeek? day, string name)
        {
            List<string> daysList = new List<string>() { "All", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            DaysList = new SelectList(daysList);
            SelectedDay = day;
            SelectedName = name;
        }

        public SelectList DaysList { get; private set; }
        public DayOfWeek? SelectedDay { get; private set; }
        public string SelectedName { get; private set; }
    }
}
