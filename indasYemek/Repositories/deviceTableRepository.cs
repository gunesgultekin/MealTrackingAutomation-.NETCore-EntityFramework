using indasYemek.context;
using indasYemek.Entities;
using Microsoft.EntityFrameworkCore;

namespace indasYemek.Repositories
{
    public class deviceTableRepository
    {
        private readonly DBContext _context;
        public deviceTableRepository(DBContext context)
        {
            _context = context;
        }

        public void addDToDb(String? email,String? deviceToken)
        {
            foreach (var entity in _context.deviceTable)
            {
                if (entity.email == email)
                {
                    _context.deviceTable.Remove(entity);
                }

                if (entity.deviceToken == deviceToken)
                {

                    _context.deviceTable.Remove(entity);
                }
            }
            _context.SaveChanges();
            deviceTable deviceRecord = new deviceTable();
            deviceRecord.email = email;
            deviceRecord.deviceToken = deviceToken;
            _context.deviceTable.Add(deviceRecord);
            _context.SaveChanges();
        }

        public async Task<List<deviceTable>> getAll()
        {
            try
            {
                return await _context.deviceTable.ToListAsync();
            }
            catch (Exception e)
            {
                return new List<deviceTable>();
            }
        }
    }
}
