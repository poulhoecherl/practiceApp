using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Data.Models
{
    public class Session
    {
        private DateTime defaultDateTime = new DateTime(1901, 1, 1, 0, 0, 0);

        public Session()
        {
            StartDate = DateTime.Now;
        }

        public int Id { get; set; }

        public DateTime StartDate { get; set; } = new DateTime(1901, 1, 1, 0, 0, 0);

        public DateTime EndDate { get; set; } = new DateTime(1901, 1, 1, 0, 0, 0);

        public string Duration 
        { 
            get 
            {
                if (EndDate == defaultDateTime || StartDate == defaultDateTime)
                {
                    return "00:00:00";
                }

                TimeSpan duration = EndDate - StartDate; 
                return duration.ToString(@"hh\:mm\:ss"); 
            }
        }
    }
}
