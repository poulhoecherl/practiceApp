using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Data.Models
{
    public class Session_Song
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int SongId { get; set; }
    }
}
