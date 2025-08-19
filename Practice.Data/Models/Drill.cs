using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Data.Models
{
    public class Drill
    {
        public int Id { get; set; }
        
        // Foreign key for the relationship with Drills
        public int DrillsId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        // Navigation property - each Drill belongs to one Drills collection
        public virtual Drills Drills { get; set; } = null!;
    }
}
