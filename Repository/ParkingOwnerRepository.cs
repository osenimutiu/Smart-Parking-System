using Microsoft.EntityFrameworkCore;
using SmartParkingSystem.Contracts;
using SmartParkingSystem.Entities.Models;

namespace SmartParkingSystem.Repository
{
    public class ParkingOwnerRepository : IParkingOwnerRepository
    {
        private readonly RepositoryContext _context;

        public ParkingOwnerRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<ParkingOwner> AddParkingOwner(ParkingOwner ParkingOwner)
        {
            _context.Add(ParkingOwner);
            await _context.SaveChangesAsync();
            return ParkingOwner;
        }

        public async Task DeleteParkingOwner(ParkingOwner ParkingOwner)
        {
            _context.ParkingOwners.Remove(ParkingOwner);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ParkingOwner>> GetListParkingOwners()
        {
            return await _context.ParkingOwners.ToListAsync();
        }

        public async Task<ParkingOwner> GetParkingOwner(int id)
        {
            return await _context.ParkingOwners.FindAsync(id);
        }

        public async Task UpdateParkingOwner(ParkingOwner ParkingOwner)
        {
            var ParkingOwnerItem = await _context.ParkingOwners.FirstOrDefaultAsync(x => x.OwnerId == ParkingOwner.OwnerId);

            if (ParkingOwnerItem != null)
            {
                ParkingOwnerItem.Name = ParkingOwner.Name;
                ParkingOwnerItem.PhoneNumber = ParkingOwner.PhoneNumber;
                ParkingOwnerItem.Email = ParkingOwner.Email;
                await _context.SaveChangesAsync();
            }

        }
    }
}
