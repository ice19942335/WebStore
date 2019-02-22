using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebStore.Entities.Entities;

namespace WebStore.Areas.Admin.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(DayOfWeek? day, string name)
        {
            Dictionary<DateFilter, string> daysList = new Dictionary<DateFilter, string>();

            daysList.Add(new DateFilter(DayOfWeek.Monday), "Monday");
            daysList.Add(new DateFilter(DayOfWeek.Tuesday), "Tuesday");
            daysList.Add(new DateFilter(DayOfWeek.Wednesday), "Wednesday");
            daysList.Add(new DateFilter(DayOfWeek.Thursday), "Thursday");
            daysList.Add(new DateFilter(DayOfWeek.Friday), "Friday");
            daysList.Add(new DateFilter(DayOfWeek.Saturday), "Saturday");
            daysList.Add(new DateFilter(DayOfWeek.Sunday), "Sunday");

            DaysList = new SelectList(daysList.Values);
            SelectedDay = day;
            SelectedName = name;
        }

        public SelectList DaysList { get; private set; }
        public DayOfWeek? SelectedDay { get; private set; }
        public string SelectedName { get; private set; }
    }
}
