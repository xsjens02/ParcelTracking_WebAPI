using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParcelTrackingAPI.Model
{
    public class Location
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string? ParcelID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
