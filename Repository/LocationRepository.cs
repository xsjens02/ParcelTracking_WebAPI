using ParcelTrackingAPI.Model;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using ParcelTrackingAPI.Configurations;

namespace ParcelTrackingAPI.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private IMongoCollection<Location> _locations;

        public LocationRepository(IOptions<ParcelMongoDB> parcelMongoDB)
        {
            var mc = new MongoClient(parcelMongoDB.Value.ConnectionString);
            var mongoDB = mc.GetDatabase(parcelMongoDB.Value.DatabaseName);
            _locations = mongoDB.GetCollection<Location>(parcelMongoDB.Value.CollectionName);
        }
        public async Task Add(Location location)
        {
            await _locations.InsertOneAsync(location);
        }

        public async Task<Location> Get(string id)
        {
            return await _locations.Find(l => l.ParcelID == id).FirstOrDefaultAsync();
        }

        public async Task Update(Location location)
        {
            var filter = Builders<Location>.Filter.Eq(l => l.ParcelID, location.ParcelID);

            var update = Builders<Location>.Update
                .Set(l => l.Latitude, location.Latitude)
                .Set(l => l.Longitude, location.Longitude)
                .Set(l => l.TimeStamp, location.TimeStamp);

            await _locations.UpdateOneAsync(filter, update);
        }
    }
}
