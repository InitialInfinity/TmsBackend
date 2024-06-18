using Master.API.Entity;
using Master.Entity;
using Master.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tokens;

namespace Master.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompentencyMasterController : ControllerBase
    {

        private readonly ICompentencyMasterRepository _compRepo;

        public CompentencyMasterController(ICompentencyMasterRepository compRepo)
        {
            _compRepo = compRepo;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(Guid user_id, string status)
        {
            try
            {
                CompentencyMaster user = new CompentencyMaster();
                user.UserId = user_id;
                user.cp_isactive = status;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAll";
                var createduser = await _compRepo.Compentency(user);
                var data = ((Microsoft.AspNetCore.Mvc.ObjectResult)createduser).Value;
                return Ok(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid? user_id, Guid cp_id)
        {
            CompentencyMaster user = new CompentencyMaster();
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }
            user.UserId = user_id;
            user.cp_id = cp_id;
            user.BaseModel.OperationType = "Get";
            try
            {
                var parameter = await _compRepo.Get(user);
                return parameter;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("GetAllTopicsDes")]
        public async Task<IActionResult> GetAllTopicsDes(Guid user_id, string designation)
        {
            try
            {
                CompentencyMaster user = new CompentencyMaster();
                user.UserId = user_id;
                user.cp_designation = designation;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAllTopicsDes";

                var createduser = await _compRepo.Compentency(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CompentencyMaster user)
        {
            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                if (user.cp_id == null)
                {
                    user.BaseModel.OperationType = "Insert";
                }
                else
                {
                    user.cp_updateddate = DateTime.Now;
                    user.BaseModel.OperationType = "Update";
                }
                var createduser = await _compRepo.Get(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteDepartment([FromBody] CompentencyMaster user)
        {
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }
            user.BaseModel.OperationType = "Delete";
            var productDetails = await _compRepo.Get(user);
            return productDetails;
        }
    }
}
