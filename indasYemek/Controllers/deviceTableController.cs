using indasYemek.context;
using indasYemek.Entities;
using indasYemek.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace indasYemek.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class deviceTableController : ControllerBase
    {

        private DBContext _context;
        private readonly deviceTableRepository _deviceTableRepository;
        public deviceTableController(DBContext context, deviceTableRepository deviceTableRepository)
        {
            this._context = context;
            this._deviceTableRepository = deviceTableRepository;
        }

        [HttpGet("AddToDb")]
        public void addToDb(String? email,String? deviceToken)
        {
           _deviceTableRepository.addDToDb(email, deviceToken);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> getAll()
        {
            try
            {
                var result = await _deviceTableRepository.getAll();
                return Ok(new DefaultReturn { Status = true, Message = "Başarılı", Desc = result });
            }
            catch (Exception ex)
            {
                return Ok(new DefaultReturn { Status = false, Message = ex.Message, Desc = null });
            }
        }
    }

    public class DefaultReturn
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Desc { get; set; }
    }
}
