using SmartParkingSystem.Entities.Models;

namespace SmartParkingSystem.Contracts
{
    public interface IDriverRepository
    {
        Task<List<Driver>> GetListDrivers();
        Task<Driver> GetDriver(int id);
        Task DeleteDriver(Driver Driver);
        Task<Driver> AddDriver(Driver Driver);
        Task UpdateDriver(Driver Driver);
    }
}
