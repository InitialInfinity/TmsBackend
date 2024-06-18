using Master.Entity;
using Master.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Tokens;

namespace Master.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicMasterController : ControllerBase
    {
        private readonly ITopicMasterRepository _TopicMasterRepo;
        public TopicMasterController(ITopicMasterRepository TopicMasterRepo)
        {
            _TopicMasterRepo = TopicMasterRepo;

        }

        [HttpGet("GetAllTopics")]
        public async Task<IActionResult> GetAllTopics(Guid user_id, string? t_isactive)
        {
            try
            {
                TopicMaster user = new TopicMaster();
                user.UserId = user_id;
                user.t_isactive = t_isactive;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAllTopic";

                var createduser = await _TopicMasterRepo.TopicMaster(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }



      

        [HttpGet("GetTopics")]
        public async Task<IActionResult> GetTopics(Guid? user_id, Guid? t_id)
        {
            try
            {
                TopicMaster user = new TopicMaster();
                user.UserId = user_id;
                user.t_id = t_id;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetTopic";

                var createduser = await _TopicMasterRepo.Get(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertTopic([FromBody] TopicMaster user)
        {
           
            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }

                if (user.t_id != null)
                {
                    user.BaseModel.OperationType = "UpdateTopic";
                }
                else
                {
                    user.BaseModel.OperationType = "InsertTopic";
                }

              
        


        DataTable dataTable = new DataTable();

                dataTable.Columns.Add("s_subject", typeof(string));
                dataTable.Columns.Add("s_content", typeof(string));
                dataTable.Columns.Add("s_subcontent", typeof(string));

                dataTable.Columns.Add("s_t_id", typeof(string));

               




                foreach (var privilage in user.subject)
                {
                    dataTable.Rows.Add(
                        privilage.s_subject,

                        privilage.s_content,
                        privilage.s_subcontent,

                        privilage.s_t_id
                    );
                }

                // user.OrderDetails = null;
                user.DataTable = dataTable;
                dynamic createduser = await _TopicMasterRepo.Get(user);
                var outcomeidvalue = createduser.Value.Outcome.OutcomeId;

                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }
       

		[HttpPost("DeleteTopics")]
		public async Task<IActionResult> DeleteTopics([FromBody] TopicMaster user)
		{
			if (user.BaseModel == null)
			{
				user.BaseModel = new BaseModel();
			}
			user.BaseModel.OperationType = "DeleteTopic";
			var createduser = await _TopicMasterRepo.TopicMaster(user);
			return createduser;
		}
		[HttpGet("GetAllSubject")]
        public async Task<IActionResult> GetAllSubject(string? t_isactive, Guid? t_id, Guid? user_id)
        {
            try
            {
                TopicMaster user = new TopicMaster();
                user.t_isactive = t_isactive;
                user.UserId = user_id;
                user.t_id = t_id;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAllSubject";

                var createduser = await _TopicMasterRepo.TopicMaster(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpGet("GetSubject")]
        //public async Task<IActionResult> GetSubject(Guid? s_id,Guid? user_id)
        //{
        //    try
        //    {
        //        TopicMaster user = new TopicMaster();
        //        user.s_id = s_id;
        //        user.UserId = user_id;
        //        if (user.BaseModel == null)
        //        {
        //            user.BaseModel = new BaseModel();
        //        }
        //        user.BaseModel.OperationType = "GetSubject";

        //        var createduser = await _TopicMasterRepo.Get(user);
        //        return createduser;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost("InsertSubject")]
        //public async Task<IActionResult> InsertSubject([FromBody] TopicMaster user)
        //{
        //    if (user.BaseModel == null)
        //    {
        //        user.BaseModel = new BaseModel();
        //    }
        //    if (user.s_id != null)
        //    {
        //        user.BaseModel.OperationType = "UpdateSubject";
        //    }
        //    else
        //    {
        //        user.BaseModel.OperationType = "InsertSubject";
        //    }
        //    try
        //    {
        //        var subject = await _TopicMasterRepo.TopicMaster(user);
        //        return subject;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //[HttpPost("DeleteSubject")]
        //public async Task<IActionResult> DeleteSubject([FromBody] TopicMaster user)
        //{
        //    try
        //    {
        //        //TopicMaster user = new TopicMaster();
        //        //user.s_id = s_id;
        //        if (user.BaseModel == null)
        //        {
        //            user.BaseModel = new BaseModel();
        //        }
        //        user.BaseModel.OperationType = "DeleteSubject";

        //        var createduser = await _TopicMasterRepo.TopicMaster(user);
        //        return createduser;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
