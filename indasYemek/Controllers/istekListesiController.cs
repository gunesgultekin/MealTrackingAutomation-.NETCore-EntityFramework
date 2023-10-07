using indasYemek.context;
using indasYemek.Entities;
using indasYemek.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static indasYemek.Controllers.yemekListesiController;

namespace indasYemek.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class istekListesiController : ControllerBase
    {
        private DBContext _context;
        private readonly istekListesiRepository _istekListesiRepository;

        public istekListesiController(DBContext context, istekListesiRepository istekListesiRepository)
        {
            this._context = context;
            this._istekListesiRepository = istekListesiRepository;
        }

        [HttpGet("addToDb")]
        public void addToDb(String email, String corba, String yemek1, String yemek2, String meze, String tatli)
        {
            _istekListesiRepository.addToDb(email, corba, yemek1, yemek2, meze, tatli);
        }

        [HttpGet("checkDbSize")]
        public int checkDbSize()
        {
            return _istekListesiRepository.checkDbSize();

        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> getAll()
        {
            try
            {
                var result = await _istekListesiRepository.getAll();

                return Ok(new DefaultReturn { Status = true, Message = "Başarılı", Desc = result });

            }
            catch (Exception ex)
            {
                return Ok(new DefaultReturn { Status = false, Message = ex.Message, Desc = null });
            }
        }

        public class DefaultReturn
        {
            public bool Status { get; set; }
            public string Message { get; set; }
            public object Desc { get; set; }

        }
    }
}