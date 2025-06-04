using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCConfigurator.Data
{
    public class Build
    {
        public int BuildId { get; set; }
        public int UserId { get; set; }
        public string BuildSummary { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual User User { get; set; }
    }
}
