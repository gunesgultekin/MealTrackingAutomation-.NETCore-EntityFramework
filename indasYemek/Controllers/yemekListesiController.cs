using indasYemek.context;
using indasYemek.Entities;
using indasYemek.yemekListesiRepository1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace indasYemek.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class yemekListesiController : ControllerBase
    {
        private DBContext _context;
        private readonly yemekListesiRepository _yemekListesiRepository;
        public yemekListesiController(DBContext context, yemekListesiRepository yemekListesiRepository)
        {
            this._context = context;
            this._yemekListesiRepository = yemekListesiRepository;
        }

        [HttpPost("addToDb")]
        public void addToDb(String? corba, String? yemek1, String? yemek2, String? meze, String? tatli)
        {
            _yemekListesiRepository.addToDb(corba, yemek1, yemek2, meze, tatli);

        }

        [HttpGet("checkDb")]
        public bool checkDb()
        {
            return _yemekListesiRepository.checkDb();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> getAll()
        {
            try
            {
                var result = await _yemekListesiRepository.getAll();

                return Ok(new DefaultReturn { Status = true, Message = "Baþarýlý", Desc = result });

            }
            catch (Exception ex)
            {
                return Ok(new DefaultReturn { Status = false, Message = ex.Message, Desc = null });
            }

        }

        [HttpGet("deleteAll")]
        public void deleteAll()
        {
            _yemekListesiRepository.deleteAll();
        }

        public class DefaultReturn
        {
            public bool Status { get; set; }
            public string Message { get; set; }
            public object Desc { get; set; }

        }
    }
}