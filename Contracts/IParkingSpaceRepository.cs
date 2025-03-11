using SmartParkingSystem.Entities.Models;

namespace SmartParkingSystem.Contracts
{
    public interface IParkingSpaceRepository
    {
        Task<List<ParkingSpace>> GetListParkingSpaces();
        Task<ParkingSpace> GetParkingSpace(int id);
        Task DeleteParkingSpace(ParkingSpace ParkingSpace);
        Task<ParkingSpace> AddParkingSpace(ParkingSpace ParkingSpace);
        Task UpdateParkingSpace(ParkingSpace ParkingSpace);
    }
}
