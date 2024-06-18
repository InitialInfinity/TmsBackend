using Master.Entity;
using Master.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Tokens;

namespace Master.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _FeedbackRepo;
        public FeedbackController(IFeedbackRepository FeedbackRepo)
        {
            _FeedbackRepo = FeedbackRepo;

        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] FeedbackForm user)
        {


            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }

                if (user.fb_id != null)
                {
                    user.BaseModel.OperationType = "Update";
                }
                else
                {
                    user.BaseModel.OperationType = "Insert";
                }

             

        


        DataTable dataTable = new DataTable();

                dataTable.Columns.Add("f_trnNo", typeof(string));
                dataTable.Columns.Add("f_trnType", typeof(string));
                dataTable.Columns.Add("f_trnReqBy", typeof(string));
                dataTable.Columns.Add("f_dateTrnForm", typeof(string));
               dataTable.Columns.Add("f_TimeTrnForm", typeof(string));
                dataTable.Columns.Add("f_dateTrnTo", typeof(string));
                dataTable.Columns.Add("f_TimeTrnTo", typeof(string));
                dataTable.Columns.Add("f_empCode", typeof(string));
                dataTable.Columns.Add("f_empName", typeof(string));
                dataTable.Columns.Add("f_trnAttend", typeof(string));
                dataTable.Columns.Add("f_des", typeof(string));
                dataTable.Columns.Add("f_desId", typeof(string));
                dataTable.Columns.Add("f_feedback", typeof(string));
				dataTable.Columns.Add("f_Trainerfeedback", typeof(byte[]));
				dataTable.Columns.Add("f_Trainingfeedback", typeof(byte[]));

                dataTable.Columns.Add("f_fb_id", typeof(string));
                dataTable.Columns.Add("f_depid", typeof(string));
                dataTable.Columns.Add("f_dep", typeof(string));
                dataTable.Columns.Add("f_trainingRel", typeof(string));
                dataTable.Columns.Add("f_contentWell", typeof(string));
                dataTable.Columns.Add("f_example", typeof(string));
                dataTable.Columns.Add("f_duration", typeof(string));
                dataTable.Columns.Add("f_participation", typeof(string));
                dataTable.Columns.Add("f_question", typeof(string));
              






                foreach (var privilage in user.Feedback)
                {
                    dataTable.Rows.Add(
                        privilage.f_trnNo,
                        privilage.f_trnType,

                        privilage.f_trnReqBy,
                        privilage.f_dateTrnForm,


                        privilage.f_TimeTrnForm,
                        privilage.f_dateTrnTo,
                        privilage.f_TimeTrnTo,
                        privilage.f_empCode,
                        privilage.f_empName,
                        privilage.f_trnAttend,
                        privilage.f_des,
                        privilage.f_desId,
                        privilage.f_feedback,
                        privilage.f_Trainerfeedback,
                        privilage.f_Trainingfeedback,
                      
                        privilage.f_fb_id,
                        privilage.f_depid,
                        privilage.f_dep,
                        privilage.f_trainingRel,
                        privilage.f_contentWell,
                        privilage.f_example,
                        privilage.f_duration,
                        privilage.f_participation,
                        privilage.f_question


					);
                }

                // user.OrderDetails = null;
                user.DataTable = dataTable;
                dynamic createduser = await _FeedbackRepo.Get(user);
                var outcomeidvalue = createduser.Value.Outcome.OutcomeId;

                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("GetAllFeedbackForm")]
        public async Task<IActionResult> GetAllFeedbackForm(Guid user_id, string? fb_isactive)
        {
            try
            {
                FeedbackForm user = new FeedbackForm();
                user.UserId = user_id;
                user.fb_isactive = fb_isactive;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAllFeedbackForm";

                var createduser = await _FeedbackRepo.Feedback(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("GetByTrn")]
        public async Task<IActionResult> GetByTrn( string fb_trnNo)
        {
            try
            {
                FeedbackForm user = new FeedbackForm();
           
                user.fb_trnNo = fb_trnNo;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetByTrn";

                var createduser = await _FeedbackRepo.Feedback(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetAllFeedback")]
        public async Task<IActionResult> GetAllFeedback(string? fb_isactive, Guid fb_id)
        {
            try
            {
                FeedbackForm user = new FeedbackForm();
                
                user.fb_isactive = fb_isactive;
                user.fb_id = fb_id;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAllFeedback";

                var createduser = await _FeedbackRepo.Feedback(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("GetFeedbackForm")]
        public async Task<IActionResult> GetFeedbackForm(Guid? user_id, Guid? fb_id)
        {
            try
            {
                FeedbackForm user = new FeedbackForm();
                user.UserId = user_id;
                user.fb_id = fb_id;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetFeedbackForm";

                var createduser = await _FeedbackRepo.Get(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("GetByCodeName")]
        public async Task<IActionResult> GetByCodeName(string fb_empCode, string fb_trnNo)
        {
            FeedbackForm user = new FeedbackForm();
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }

            user.fb_empCode = fb_empCode;
            user.fb_trnNo = fb_trnNo;
            user.BaseModel.OperationType = "GetByCodeName";
            try
            {
                var parameter = await _FeedbackRepo.Get(user);
                return parameter;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] FeedbackForm user)
        {
            try
            {
                //TopicMaster user = new TopicMaster();
                //user.t_id = t_id;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "Delete";

                var createduser = await _FeedbackRepo.Feedback(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

		[HttpGet("GetOrder")]
		public async Task<IActionResult> GetOrder()
		{
			try
			{
				FeedbackForm user = new FeedbackForm();
				

				if (user.BaseModel == null)
				{
					user.BaseModel = new BaseModel();
				}

				user.BaseModel.OperationType = "GetOrder";

				var createduser = await _FeedbackRepo.Get(user);
				return createduser;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}

}
