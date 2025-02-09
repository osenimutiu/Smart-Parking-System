using Microsoft.EntityFrameworkCore;
using SmartParkingSystem.Contracts;
using SmartParkingSystem.Entities.Models;

namespace SmartParkingSystem.Repository
{
    public class DriverRepository : IDriverRepository
    {
        private readonly RepositoryContext _context;

        public DriverRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<Driver> AddDriver(Driver Driver)
        {
            _context.Add(Driver);
            await _context.SaveChangesAsync();
            return Driver;
        }

        public async Task DeleteDriver(Driver Driver)
        {
            _context.Drivers.Remove(Driver);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Driver>> GetListDrivers()
        {
            return await _context.Drivers.ToListAsync();
        }

        public async Task<Driver> GetDriver(int id)
        {
            return await _context.Drivers.FindAsync(id);
        }

        public async Task UpdateDriver(Driver Driver)
        {
            var DriverItem = await _context.Drivers.FirstOrDefaultAsync(x => x.DriverId == Driver.DriverId);

            if (DriverItem != null)
            {
                DriverItem.Name = Driver.Name;
                DriverItem.Email = Driver.Email;
                DriverItem.PhoneNumber = Driver.PhoneNumber;
                await _context.SaveChangesAsync();
            }

        }
    }
}
