using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Context.Models
{
    public class TrafficLog
    {
        public int ID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? PathAccessed { get; set; }
        public string? UserAgent { get; set; }
        public string? Referer { get; set; }
        public string? UserId { get; set; }
        public string IpAddress { get; set; }
    }
}
