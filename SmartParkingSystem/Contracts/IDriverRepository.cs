using SmartParkingSystem.Entities.Models;
<<<<<<< HEAD
using System.Data;
=======
>>>>>>> origin/master

namespace SmartParkingSystem.Contracts
{
    public interface IDriverRepository
    {
        Task<List<Driver>> GetListDrivers();
<<<<<<< HEAD
        Task<List<Driver>> GetDriversBasedOnUser(string role, string email);
        Task<Driver> GetDriver(int id);
        Task<Driver> GetDriverByEmail(string emailAddress);
=======
        Task<Driver> GetDriver(int id);
>>>>>>> origin/master
        Task DeleteDriver(Driver Driver);
        Task<Driver> AddDriver(Driver Driver);
        Task UpdateDriver(Driver Driver);
    }
}
