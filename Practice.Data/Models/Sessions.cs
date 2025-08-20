using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Data.Models
{
    internal class Sessions
    {

        public int Id { get; set; }

        // Navigation property - one Session can contain many Session objects
        public virtual ICollection<Session> SessionCollection { get; set; } = new List<Session>();
    }
}
