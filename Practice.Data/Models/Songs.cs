using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Data.Models
{
    public class Songs
    {
        public int Id { get; set; }
        
        // Navigation property - one Songs can contain many Song objects
        public virtual ICollection<Song> SongCollection { get; set; } = new List<Song>();
    }
}
