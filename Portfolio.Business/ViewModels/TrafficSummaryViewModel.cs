using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Business.ViewModels
{
    public class TrafficSummaryViewModel
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }

        // Computed label for the chart
        public string Label
        {
            get
            {
                if (Date.Hour > 0)
                    return Date.ToString("MM/dd HH:00");
                return Date.ToString("MM/dd");
            }
        }
    }
}
