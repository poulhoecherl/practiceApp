using Newtonsoft.Json;

namespace Practice.Data.Models
{
    public class Song
    {
        public int Id { get; set; }
        
        public int SongsId { get; set; }

        public string Name { get; set; } = string.Empty;
        
        public string? Artist { get; set; } = string.Empty;
        
        public string? Genre { get; set; } = "jazz";

    }
}
