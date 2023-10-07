using indasYemek.context;
using indasYemek.Entities;
using Microsoft.EntityFrameworkCore;

namespace indasYemek.Repositories
{
    public class istekListesiRepository
    {
        private readonly DBContext _context;
        public istekListesiRepository(DBContext context)
        {
            _context = context;
        }
        public void addToDb(String? email, String? corba, String? yemek1, String? yemek2, String? meze, String? tatli) 
        {  
            foreach (var entity in _context.istekListesi)
            {
                if (entity.email == email)
                {
                    _context.istekListesi.Remove(entity);
                }   
            }
            _context.SaveChanges();         
            istekListesi istekListesi = new istekListesi();
            istekListesi.email = email;
            istekListesi.corba = corba;
            istekListesi.yemek1 = yemek1;
            istekListesi.yemek2 = yemek2;
            istekListesi.meze = meze;
            istekListesi.tatli = tatli;
            _context.istekListesi.Add(istekListesi);
            _context.SaveChanges();
        }

        public int checkDbSize()
        {
            int size = _context.istekListesi.Count();
            return size;           
        }

        public async Task <List<istekListesi>> getAll()
        {
            try
            {
                return await _context.istekListesi.ToListAsync();
            }
            catch (Exception e)
            {
                return new List<istekListesi>();
            }
        }
    }
}
