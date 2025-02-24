using SmartParkingSystem.Entities.Models;
using System.Data;

namespace SmartParkingSystem.Contracts
{
    public interface IDriverRepository
    {
        Task<List<Driver>> GetListDrivers();
        Task<List<Driver>> GetDriversBasedOnUser(string role, string email);
        Task<Driver> GetDriver(int id);
        Task DeleteDriver(Driver Driver);
        Task<Driver> AddDriver(Driver Driver);
        Task UpdateDriver(Driver Driver);
    }
}
