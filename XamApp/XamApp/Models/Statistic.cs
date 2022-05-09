using System;
using System.Collections.Generic;
using System.Text;

namespace XamApp.Models
{
    public class Statistic
    {
        public DateTime SDate { get; set; }
        public DateTime EDate { get; set; }
        public IEnumerable<StatisticItem> Parts { get; set; }
    }

    public class StatisticItem
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double Amount { get; set; }
    }
}
