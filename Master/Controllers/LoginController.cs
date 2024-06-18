using Master.Entity;
using Master.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tokens;

namespace Master.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        public readonly ILoginRepository _login;
        public LoginController(ILoginRepository loginRepo)
        {
            _login = loginRepo;
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Get(string username,string password)
        {
            Login user = new Login();
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }
            user.username = username;
            user.password = password;

          
            user.BaseModel.OperationType = "ValidateLogin";
            try
            {
                var parameter = await _login.Get(user);
                return parameter;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
