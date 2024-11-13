using ParcelTrackingAPI.Model;

namespace ParcelTrackingAPI.Repository
{
    public interface ILocationRepository
    {
        Task Add(Location location);
        Task <Location> Get(string id);
        Task Update(Location location);
    }
}
