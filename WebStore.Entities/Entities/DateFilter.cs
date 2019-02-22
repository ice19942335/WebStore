using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Entities.Entities
{
    public class DateFilter
    {
        public DateFilter(DayOfWeek dayOfWeek)
        {
            WeekDay = dayOfWeek;
        }
        public DayOfWeek WeekDay { get; set; }
        public string DayName { get; set; }
    }
}
