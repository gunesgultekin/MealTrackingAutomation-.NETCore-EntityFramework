using indasYemek.context;
using indasYemek.Entities;
using Microsoft.EntityFrameworkCore;

namespace indasYemek.yemekListesiRepository1
{
    public class yemekListesiRepository
    {
        private readonly DBContext _context;

        public yemekListesiRepository(DBContext context)
        {
            _context = context;
        }
       
        public void addToDb(String? corba, String? yemek1, String? yemek2, String? meze, String? tatli)
        {
            foreach (var entity in _context.yemekListesi)
                _context.yemekListesi.Remove(entity);
            _context.SaveChanges();

            foreach (var entity in _context.istekListesi)
                _context.istekListesi.Remove(entity);
            _context.SaveChanges();
            yemekListesi yemeklistesi = new yemekListesi();
            yemeklistesi.corba = corba;
            yemeklistesi.yemek1 = yemek1;
            yemeklistesi.yemek2 = yemek2;
            yemeklistesi.meze = meze;
            yemeklistesi.tatli = tatli;
            _context.yemekListesi.Add(yemeklistesi);
            _context.SaveChanges();
        }

        public bool checkDb()
        {
            int check = _context.yemekListesi.Count();
            if (check > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<yemekListesi> getAll()
        {
            try
            {
                return await _context.yemekListesi.FirstAsync();
            }
            catch (Exception e)
            {
                return new yemekListesi();
            }
        }

        public void deleteAll()
        {
            foreach (var entity in _context.yemekListesi)
                _context.yemekListesi.Remove(entity);        
            _context.SaveChanges() ;
            foreach(var entity in _context.istekListesi)
                _context.istekListesi.Remove(entity);
            _context.SaveChanges() ;
        }
    }
}
