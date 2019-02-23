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
            List<string> daysList = new List<string>() {"All", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            DaysList = new SelectList(daysList);
            SelectedDay = day;
            SelectedName = name;
        }

        public SelectList DaysList { get; private set; }
        public DayOfWeek? SelectedDay { get; private set; }
        public string SelectedName { get; private set; }
    }
}
