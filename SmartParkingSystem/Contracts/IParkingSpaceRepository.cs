using SmartParkingSystem.Entities.Models;

namespace SmartParkingSystem.Contracts
{
    public interface IParkingSpaceRepository
    {
<<<<<<< HEAD
        Task<List<ParkingSpace>> GetListParkingSpaces(string role, string email);
=======
        Task<List<ParkingSpace>> GetListParkingSpaces();
>>>>>>> origin/master
        Task<ParkingSpace> GetParkingSpace(int id);
        Task DeleteParkingSpace(ParkingSpace ParkingSpace);
        Task<ParkingSpace> AddParkingSpace(ParkingSpace ParkingSpace);
        Task UpdateParkingSpace(ParkingSpace ParkingSpace);
    }
}
