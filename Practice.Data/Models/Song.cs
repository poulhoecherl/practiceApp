using Newtonsoft.Json;

namespace Practice.Data.Models
{
    public class Song
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        // Foreign key for the relationship with Songs
        [JsonProperty("SongsId")]
        public int SongsId { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; } = string.Empty;

        // Navigation property - each Song belongs to one Songs collection

        public virtual Songs Songs { get; set; } = null!;

        [JsonProperty("Artist")]
        public string? Artist { get; set; } = string.Empty;

        [JsonProperty("Album")]
        public string? Genre { get; set; } = "jazz";

        [JsonProperty("Genre")]
        public TimeSpan Duration { get; set; }
    }
}
