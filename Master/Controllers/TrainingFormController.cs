using Context;
using MailKit.Net.Smtp;
using Master.Entity;
using Master.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Data;
using Tokens;

namespace Master.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingFormController : ControllerBase
    {

        private readonly ITrainingFormRepository _TrainingFormRepo;
        public TrainingFormController(ITrainingFormRepository TrainingFormRepo)
        {
            _TrainingFormRepo = TrainingFormRepo;

        }


        [HttpGet("GetAllTrainingNeed")]
        public async Task<IActionResult> GetAllTrainingNeed(Guid user_id, string? tr_isactive)
        {
            try
            {
                TrainingForm user = new TrainingForm();
                user.UserId = user_id;
                user.tr_isactive = tr_isactive;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAllTrainingNeed";

                var createduser = await _TrainingFormRepo.TrainingForm(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("GetAllByDepart")]
        public async Task<IActionResult> GetAllByDepart(Guid user_id, string? td_Dept)
        {
            try
            {
                TrainingForm user = new TrainingForm();
                user.td_dept = td_Dept;
                user.UserId = user_id;

                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAllByDepart";

                var createduser = await _TrainingFormRepo.TrainingForm(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetAllApprove")]
        public async Task<IActionResult> GetAllApprove(string roleid)
        {
            try
            {
                TrainingForm user = new TrainingForm();
                user.roleid = roleid;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAllApprove";

                var createduser = await _TrainingFormRepo.TrainingForm(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromBody] TrainingForm user)
        {
            dynamic createduser;
            try
            {
                //TopicMaster user = new TopicMaster();
                //user.t_id = t_id;
                user.tr_updateddate = DateTime.Now;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "UpdateStatus";

                //var createduser = await _TrainingFormRepo.TrainingForm(user);

                if (user.tr_action == "2")//apoved by hr 4 apoved by hod
                {

                    createduser = await _TrainingFormRepo.TrainingForm(user);


                    var outcomeidvalue = createduser.Value.Outcome.OutcomeId;
                    if (outcomeidvalue == 1)
                    {

                        var datavalue = createduser.Value.Outcome.OutcomeDetail;

                        Guid tr_id;
                        if (Guid.TryParse(datavalue, out tr_id))
                        {

                            user.tr_id = tr_id;

                            user.BaseModel.OperationType = "selectTrainingDta";


                            dynamic createduser2 = await _TrainingFormRepo.Get(user);
                            var datavalue2 = createduser2.Value.Data;

                            string ValueTo = "HOD";
                            await SendNotification(datavalue2, ValueTo);

                        }




                    }
                }

                else
                {

                    createduser = await _TrainingFormRepo.TrainingForm(user);
                }
                return createduser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed : {ex.Message}");
                throw;
            }
        }


      



        [HttpGet("GetTrainingNeed")]
        public async Task<IActionResult> GetTrainingNeed(Guid? user_id, Guid? tr_id)
        {
            try
            {
                TrainingForm user = new TrainingForm();
                user.UserId = user_id;
                user.tr_id = tr_id;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetTrainingNeed";

                var createduser = await _TrainingFormRepo.Get(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertTrainingNeed([FromBody] TrainingForm user)
        {


            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }

                if (user.tr_id != null)
                {
                    user.BaseModel.OperationType = "UpdateTrainingNeed";
                }
                else
                {
                    user.BaseModel.OperationType = "InsertTrainingNeed";
                }





                DataTable dataTable = new DataTable();

                dataTable.Columns.Add("td_dept", typeof(string));
                dataTable.Columns.Add("td_des", typeof(string));
                dataTable.Columns.Add("td_emp_code", typeof(string));
                dataTable.Columns.Add("td_emp_name", typeof(string));


                dataTable.Columns.Add("td_date_training", typeof(string));
                dataTable.Columns.Add("td_req_dept", typeof(string));
                dataTable.Columns.Add("td_topic_training", typeof(string));
                dataTable.Columns.Add("td_topic_training_name", typeof(string));
                dataTable.Columns.Add("td_emp_des", typeof(string));
                dataTable.Columns.Add("tr_td_id", typeof(string));


                foreach (var privilage in user.training)
                {
                    dataTable.Rows.Add(
                        privilage.td_dept,
                        privilage.td_des,

                        privilage.td_emp_code,
                        privilage.td_emp_name,


                        privilage.td_date_training,
                        privilage.td_req_dept,
                        privilage.td_topic_training,
                        privilage.td_topic_training_name,
                        privilage.td_emp_des,
                        privilage.tr_td_id
                    );
                }

                // user.OrderDetails = null;
                user.DataTable = dataTable;
                dynamic createduser = await _TrainingFormRepo.Get(user);
                var outcomeidvalue = createduser.Value.Outcome.OutcomeId;

                var datavalue = createduser.Value.Outcome.OutcomeDetail;

                Guid tr_id;
                if (Guid.TryParse(datavalue, out tr_id))
                {

                    user.tr_id = tr_id;

                    user.BaseModel.OperationType = "selectTrainingDtaForHR";


                    dynamic createduser2 = await _TrainingFormRepo.Get(user);
                    var datavalue2 = createduser2.Value.Data;
                    string ValueTo = "HR";
                    await SendNotification(datavalue2, ValueTo);






                }
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }




//        private async Task SendNotificationToHr(dynamic datavalue2)
//        {
//            string emailId = datavalue2.tr_remark;
//            string reqdate = datavalue2.tr_req_date;

//            string trnNo = datavalue2.tr_req_no;
//            string title = "Training Approval Request";
//			string htmlContent = @"
//<div style='width: 100%; background-color: #f8f9fa; padding: 20px 0;'>
//    <div style='max-width: 600px; margin: 0 auto;'>
//        <div style='box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15); border-radius: 0.5rem; overflow: hidden;'>
//            <div style='background-color: #ffffff; padding: 20px;'>
//                <h1 style='text-align: center; margin-top: 0;'>Training Approval Request</h1>
//                <h2 style='font-weight: bold; margin-top: 2rem; text-align: start;'>Dear HR,</h2>
//                <div style='margin-top: 1rem; text-align: start;'>
//                    <p style='font-weight: bold;'>A new training has been added and requires approval:</p>
//                    <p><strong>Training No:</strong> " + trnNo + @"</p>
//                    <p><strong>Date:</strong> " + reqdate + @"</p>
//                    <p style='font-weight: bold;'>Please take necessary action to approve this training.</p>
//                </div>
//            </div>
//        </div>
//    </div>
//</div>";


//			string[] emailAddresses = emailId.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);


//            EmailConfigure user = new EmailConfigure();
//            dynamic emailDetails = await _TrainingFormRepo.EmailTo(user);


//			if (emailDetails != null)
//			{
//				try
//				{
//					// Use the retrieved email configuration details to send individual emails
//					for (int i = 0; i < emailAddresses.Length; i++)
//					{
//						var message = new MimeMessage();
//						message.From.Add(new MailboxAddress("Rensa Tubes", emailDetails.Value.Data.email));
//						message.To.Add(new MailboxAddress(null, emailAddresses[i].Trim()));
//						message.Subject = title;

//						var bodyBuilder = new BodyBuilder();
//						bodyBuilder.HtmlBody = htmlContent;
//						message.Body = bodyBuilder.ToMessageBody();

//						using (var client = new SmtpClient())
//						{
//							client.Connect(emailDetails.Value.Data.smtp_server, emailDetails.Value.Data.smtp_port, true);
//							client.Authenticate(emailDetails.Value.Data.email, emailDetails.Value.Data.password);
//							client.Send(message);
//							client.Disconnect(true);
//						}
//					}
//				}
//				catch (Exception ex)
//				{
//					Console.WriteLine($"Failed to send email: {ex.Message}");
//				}
//			}
//			else
//            {
//                // Handle case where email configuration details are not found
//                Console.WriteLine("Email configuration details not found.");
//            }

//        }

        private async Task SendNotification(dynamic datavalue2,string ValueTo)
        {
            string emailId = datavalue2.tr_remark;
            string reqdate = datavalue2.tr_req_date;

            string trnNo = datavalue2.tr_req_no;
            string title = "Training Approval Request";
            string htmlContent = @"
<div style='width: 100%; background-color: #f8f9fa; padding: 20px 0;'>
    <div style='max-width: 600px; margin: 0 auto;'>
        <div style='box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15); border-radius: 0.5rem; overflow: hidden;'>
            <div style='background-color: #ffffff; padding: 20px;'>
                <h1 style='text-align: center; margin-top: 0;'>Training Approval Request</h1>
                <h2 style='font-weight: bold; margin-top: 2rem; text-align: start;'>Dear "+ ValueTo + @",</h2>
                <div style='margin-top: 1rem; text-align: start;'>
                    <p style='font-weight: bold;'>A new training has been added and requires approval:</p>
                    <p><strong>Training No:</strong> " + trnNo + @"</p>
                    <p><strong>Date:</strong> " + reqdate + @"</p>
                    <p style='font-weight: bold;'>Please take necessary action to approve this training.</p>
                </div>
            </div>
        </div>
    </div>
</div>";

            // Split email addresses
            string[] emailAddresses = emailId.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            // Retrieve email configuration

            // Assuming EmailConfigure is the correct type for input
            //var emailDetails = await _emailConfiguration.GetEmail();
            EmailConfigure user = new EmailConfigure();
            dynamic emailDetails = await _TrainingFormRepo.EmailTo(user);


            if (emailDetails != null)
            {
                try
                {
                    // Use the retrieved email configuration details to send individual emails
                    for (int i = 0; i < emailAddresses.Length; i++)
                    {
                        var message = new MimeMessage();
                        message.From.Add(new MailboxAddress("Rensa Tubes", emailDetails.Value.Data.email));
                        message.To.Add(new MailboxAddress(null, emailAddresses[i].Trim()));
                        message.Subject = title;

                        var bodyBuilder = new BodyBuilder();
                        bodyBuilder.HtmlBody = htmlContent;
                        message.Body = bodyBuilder.ToMessageBody();

                        using (var client = new SmtpClient())
                        {
                            client.Connect(emailDetails.Value.Data.smtp_server, emailDetails.Value.Data.smtp_port, false);
                            client.Authenticate(emailDetails.Value.Data.email, emailDetails.Value.Data.password);
                            client.Send(message);
                            client.Disconnect(true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                }
            }
            else
            {
                // Handle case where email configuration details are not found
                Console.WriteLine("Email configuration details not found.");
            }

        }

        [HttpPost("DeleteTrainingNeed")]
        public async Task<IActionResult> DeleteTrainingNeed([FromBody] TrainingForm user)
        {
            try
            {
                //TopicMaster user = new TopicMaster();
                //user.t_id = t_id;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "DeleteTrainingNeed";

                var createduser = await _TrainingFormRepo.TrainingForm(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetAllTraining")]
        public async Task<IActionResult> GetAllTraining(string? tr_isactive, Guid tr_id, Guid user_id)
        {
            try
            {
                TrainingForm user = new TrainingForm();
                user.UserId = user_id;
                user.tr_isactive = tr_isactive;
                user.tr_id = tr_id;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAllTraining";

                var createduser = await _TrainingFormRepo.TrainingForm(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }



        //[HttpGet("GetByCode")]
        //public async Task<IActionResult> GetByCode(Guid? UserId, string td_emp_code)
        //{
        //    TrainingForm user = new TrainingForm();
        //    if (user.BaseModel == null)
        //    {
        //        user.BaseModel = new BaseModel();
        //    }
        //    user.UserId = UserId;
        //    user.td_emp_code = td_emp_code;
        //    user.BaseModel.OperationType = "GetByCode";
        //    try
        //    {
        //        var parameter = await _TrainingFormRepo.Get(user);
        //        return parameter;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        [HttpGet("GetByCode")]
        public async Task<IActionResult> GetByCode(Guid? UserId, string td_emp_code)
        {
            try
            {
                TrainingForm user = new TrainingForm();
                user.UserId = UserId;
                user.td_emp_code = td_emp_code;
               
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetByCode";

                var createduser = await _TrainingFormRepo.TrainingForm(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpGet("GetByTopic")]
        public async Task<IActionResult> GetByTopic(string tr_topic_training)
        {
            TrainingForm user = new TrainingForm();
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }
            user.tr_topic_training = tr_topic_training;
            user.BaseModel.OperationType = "GetByTopic";
            try
            {
                var parameter = await _TrainingFormRepo.Get(user);
                return parameter;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(Guid? UserId, string td_emp_name)
        {
            TrainingForm user = new TrainingForm();
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }
            user.UserId = UserId;
            user.td_emp_name = td_emp_name;
            user.BaseModel.OperationType = "GetByName";
            try
            {
                var parameter = await _TrainingFormRepo.Get(user);
                return parameter;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetOrder")]
        public async Task<IActionResult> GetOrder(Guid? UserId)
        {
            try
            {
                TrainingForm user = new TrainingForm();
                user.UserId = UserId;

                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }

                user.BaseModel.OperationType = "GetOrder";

                var createduser = await _TrainingFormRepo.Get(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
