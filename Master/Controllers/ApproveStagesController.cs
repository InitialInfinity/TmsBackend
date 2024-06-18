using Master.Entity;
using Master.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tokens;

namespace Master.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApproveStagesController : ControllerBase
   {

        private readonly IApproveStagesRepositorycs _ApproveStagesRepo;
        public ApproveStagesController(IApproveStagesRepositorycs ApproveStagesRepo)
        {
            _ApproveStagesRepo = ApproveStagesRepo;

        }





        [HttpGet("Get")]
        public async Task<IActionResult> Get( string roleid)
        {
            ApproveStages user = new ApproveStages();
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }
          
            user.as_role_id = roleid;
            user.BaseModel.OperationType = "Get";
            try
            {
                var parameter = await _ApproveStagesRepo.ApproveStages(user);
                return parameter;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
