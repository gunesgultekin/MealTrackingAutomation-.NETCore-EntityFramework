using indasYemek.context;
using indasYemek.Repositories;
using indasYemek.yemekListesiRepository1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace indasYemek.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class userTableController : ControllerBase
    {
        private DBContext _context;
        private readonly userTableRepository _userTableRepository;
        public userTableController(DBContext context, userTableRepository userTableRepository)
        {
            this._context = context;
            this._userTableRepository = userTableRepository;

        }

        [HttpGet("loginAuth")]
        public String LoginAuth(string username, string password)
        {
            return _userTableRepository.LoginAuth(username, password);
        }
    }
}
