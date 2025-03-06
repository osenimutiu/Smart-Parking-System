using Microsoft.EntityFrameworkCore;
using SmartParkingSystem.Contracts;
using SmartParkingSystem.Entities.Models;

namespace SmartParkingSystem.Repository
{
    public class ParkingSpaceRepository : IParkingSpaceRepository
    {
        private readonly RepositoryContext _context;

        public ParkingSpaceRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<ParkingSpace> AddParkingSpace(ParkingSpace ParkingSpace)
        {
            _context.Add(ParkingSpace);
            await _context.SaveChangesAsync();
            return ParkingSpace;
        }

        public async Task DeleteParkingSpace(ParkingSpace ParkingSpace)
        {
            _context.ParkingSpaces.Remove(ParkingSpace);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ParkingSpace>> GetListParkingSpaces(string role, string email)
        {
            if (role.ToLower() == "owner")
                return await _context.ParkingSpaces.Where(x=>x.Owner.Email == email).Include(y => y.Owner).DefaultIfEmpty().ToListAsync();
            return await _context.ParkingSpaces.Include(x => x.Owner).DefaultIfEmpty().ToListAsync();
        }

        public async Task<ParkingSpace> GetParkingSpace(int id)
        {
            return await _context.ParkingSpaces.FindAsync(id);
        }

        public async Task UpdateParkingSpace(ParkingSpace ParkingSpace)
        {
            var ParkingSpaceItem = await _context.ParkingSpaces.FirstOrDefaultAsync(x => x.SpaceId == ParkingSpace.SpaceId);

            if (ParkingSpaceItem != null)
            {
                ParkingSpaceItem.AvailableSlots = ParkingSpace.AvailableSlots;
                ParkingSpaceItem.TotalSlots = ParkingSpace.TotalSlots;
                ParkingSpaceItem.OwnerId = ParkingSpace.OwnerId;
                ParkingSpaceItem.Location = ParkingSpace.Location;
                ParkingSpaceItem.VehicleType = ParkingSpace.VehicleType;
                ParkingSpaceItem.IsAvailable = ParkingSpace.IsAvailable;
                ParkingSpaceItem.Amount = ParkingSpace.Amount;
                await _context.SaveChangesAsync();
            }

        }
    }
}
