using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Data.Models
{
    public class Drills
    {
        public int Id { get; set; }
        
        // Navigation property - one Drills can contain many Drill objects
        public virtual ICollection<Drill> DrillCollection { get; set; } = new List<Drill>();
    }
}
