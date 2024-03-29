namespace InstaCrafter.Classes.Models
{
    public class InstagramLocation
    {
        public long FacebookPlacesId { get; set; }

        public string? City { get; set; }

        public long Pk { get; set; }

        public string? ShortName { get; set; }

        public string? ExternalSource { get; set; }

        public string? ExternalId { get; set; }

        public string? Address { get; set; }

        public double Lng { get; set; }

        public double Lat { get; set; }

        public string? Name { get; set; }
    }
}