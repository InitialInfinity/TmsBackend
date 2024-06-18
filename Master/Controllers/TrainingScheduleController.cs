using Common;
using Context;
using MailKit.Net.Smtp;
using Master.Entity;
using Master.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Tokens;


namespace Master.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingScheduleController : ControllerBase
    {


        private readonly ITrainingScheduleRepository _TrainingSchedule;
        // private readonly EmailConfiguration _emailConfiguration;
        public TrainingScheduleController(ITrainingScheduleRepository TrainingScheduleRepo)
        {
            _TrainingSchedule = TrainingScheduleRepo;
            //_emailConfiguration = emailConfiguration;
        }





        [HttpGet("GetAllTrainingSchedule")]
        public async Task<IActionResult> GetAllTrainingSchedule(Guid user_id, string? ts_isactive)
        {
            try
            {
                TrainingSchedule user = new TrainingSchedule();
                user.UserId = user_id;
                user.ts_isactive = ts_isactive;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAllTrainingSchedule";


                var createduser = await _TrainingSchedule.TrainingSchedule(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("GetAllTraining")]
        public async Task<IActionResult> GetAllTraining(string? ts_isactive, Guid ts_id, Guid user_id)
        {
            try
            {
                TrainingSchedule user = new TrainingSchedule();
                user.UserId = user_id;
                user.ts_isactive = ts_isactive;
                user.ts_id = ts_id;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAllTraining";


                var createduser = await _TrainingSchedule.TrainingSchedule(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("GetTrainingSchedule")]
        public async Task<IActionResult> GetTrainingSchedule(Guid? user_id, Guid? ts_id)
        {
            try
            {
                TrainingSchedule user = new TrainingSchedule();
                user.UserId = user_id;
                user.ts_id = ts_id;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetTrainingSchedule";


                var createduser = await _TrainingSchedule.Get(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> InsertTrainingNeed([FromBody] TrainingSchedule user)
        {

            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }


                if (user.ts_id != null)
                {
                    user.BaseModel.OperationType = "UpdateTrainingSchedule";
                }
                else
                {
                    user.BaseModel.OperationType = "InsertTrainingSchedule";
                }






                DataTable dataTable = new DataTable();


                dataTable.Columns.Add("tss_emp_code", typeof(string));
                dataTable.Columns.Add("tss_emp_name", typeof(string));
                dataTable.Columns.Add("tss_traning_attend", typeof(string));
                dataTable.Columns.Add("tss_traning_des", typeof(string));


                dataTable.Columns.Add("tss_sch_hour", typeof(string));
                dataTable.Columns.Add("tss_actual_attend", typeof(string));
                dataTable.Columns.Add("tss_com_status", typeof(string));
                dataTable.Columns.Add("tss_to_marks", typeof(string));
                dataTable.Columns.Add("tss_marks_obt", typeof(string));
                dataTable.Columns.Add("tss_traning_status", typeof(string));
                dataTable.Columns.Add("tss_re_traning_req", typeof(string));
                dataTable.Columns.Add("tss_traning_cert", typeof(byte[]));
                dataTable.Columns.Add("tss_status", typeof(string));
                dataTable.Columns.Add("tss_remark", typeof(string));
                dataTable.Columns.Add("ts_tss_id", typeof(string));
                dataTable.Columns.Add("tss_topic", typeof(string));
                dataTable.Columns.Add("tss_tag", typeof(string));





                foreach (var privilage in user.trainingsubschedule)
                {
                    dataTable.Rows.Add(
                        privilage.tss_emp_code,
                        privilage.tss_emp_name,


                       privilage.tss_traning_attend,
                        privilage.tss_traning_des,
                    privilage.tss_sch_hour,
                        privilage.tss_actual_attend,
                        privilage.tss_com_status,
                        privilage.tss_to_marks,
                        privilage.tss_marks_obt,
                        privilage.tss_traning_status,
                        privilage.tss_re_traning_req,
                        privilage.tss_traning_cert,
                        privilage.tss_status,
                        privilage.tss_remark,
                        privilage.ts_tss_id,
                        privilage.tss_topic,
                        privilage.tss_tag
                    );
                }



                user.DataTable = dataTable;
                dynamic createduser = await _TrainingSchedule.Get(user);
                var outcomeidvalue = createduser.Value.Outcome.OutcomeId;

                var datavalue = createduser.Value.Outcome.OutcomeDetail;

                Guid ts_id;
                if (Guid.TryParse(datavalue, out ts_id))
                {

                    user.ts_id = ts_id;

                    user.BaseModel.OperationType = "selectTrainingDtaForMG";


                    dynamic createduser2 = await _TrainingSchedule.Get(user);
                    var datavalue2 = createduser2.Value.Data;
                    await SendNotificationToMG(datavalue2);






                }
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }


       

        private async Task SendNotificationToMG(dynamic datavalue2)
        {
            string emailId = datavalue2.ts_req_by;
            string from = datavalue2.ts_dt_tm_fromtraining;
            string to = datavalue2.ts_dt_tm_totraining;
            string trainerName = datavalue2.ts_trainer_name;
            string topic = datavalue2.ts_topic_name;
            string trnNo = datavalue2.ts_training_no;
            string title = "Training Approval Request";
            string htmlContent = @"
<div style='width: 100%; background-color: #f8f9fa; padding: 20px 0;'>
    <div style='max-width: 600px; margin: 0 auto;'>
        <div style='box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15); border-radius: 0.5rem; overflow: hidden;'>
            <div style='background-color: #ffffff; padding: 20px;'>
                <h1 style='text-align: center; margin-top: 0;'>Training Approval Request</h1>
                <h2 style='font-weight: bold; margin-top: 2rem; text-align: start;'>Dear Manager,</h2>
                <div style='margin-top: 1rem; text-align: start;'>
                    <p style='font-weight: bold;'>A new training has been added and requires approval:</p>
                    <p><strong>Training No:</strong> " + trnNo + @"</p>
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
            dynamic emailDetails = await _TrainingSchedule.GetEmail(user);


            //if (emailDetails != null)
            //{
            //	// Use the retrieved email configuration details to send the email
            //	var message = new MimeMessage();
            //	message.From.Add(new MailboxAddress("Rensa Tubes", emailDetails.Value.Data.email)); // set your email

            //	foreach (string emailAddress in emailAddresses)
            //	{
            //		message.To.Add(new MailboxAddress(null, emailAddress.Trim()));
            //	}

            //	message.Subject = title;
            //	var bodyBuilder = new BodyBuilder();
            //	bodyBuilder.HtmlBody = htmlContent;
            //	message.Body = bodyBuilder.ToMessageBody();

            //	try
            //	{
            //		using (var client = new SmtpClient())
            //		{
            //			// Connect to the SMTP server and send the email
            //			client.Connect(emailDetails.Value.Data.smtp_server, emailDetails.Value.Data.smtp_port, true);//false
            //			client.Authenticate(emailDetails.Value.Data.email, emailDetails.Value.Data.password);
            //			client.Send(message);
            //			client.Disconnect(true);
            //		}
            //	}
            //	catch (Exception ex)
            //	{
            //		// Handle SMTP client errors
            //		Console.WriteLine($"Failed to send email: {ex.Message}");
            //	}
            //}

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



        [HttpPost("DeleteTrainingSchedule")]
        public async Task<IActionResult> DeleteTrainingSchedule([FromBody] TrainingSchedule user)
        {
            try
            {
                //TopicMaster user = new TopicMaster();
                //user.t_id = t_id;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "DeleteTrainingSchedule";


                var createduser = await _TrainingSchedule.TrainingSchedule(user);
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
                TrainingSchedule user = new TrainingSchedule();
                user.roleid = roleid;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAllApprove";


                var createduser = await _TrainingSchedule.TrainingSchedule(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromBody] TrainingSchedule user)
        {
            dynamic createduser;

            try
            {

                user.ts_updateddate = DateTime.Now;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "UpdateStatus";

                if (user.ts_action == "6")
                {

                    createduser = await _TrainingSchedule.TrainingSchedule(user);


                    var outcomeidvalue = createduser.Value.Outcome.OutcomeId;
                    if (outcomeidvalue == 1)
                    {

                        var datavalue = createduser.Value.Outcome.OutcomeDetail;

                        Guid ts_id;
                        if (Guid.TryParse(datavalue, out ts_id))
                        {

                            user.ts_id = ts_id;


                            user.BaseModel.OperationType = "selectEmployeedata";


                            dynamic createduser2 = await _TrainingSchedule.Get(user);
                            var datavalue2 = createduser2.Value.Data;
                            await SendNotification(datavalue2);

                        }




                    }
                }

                else
                {

                    createduser = await _TrainingSchedule.TrainingSchedule(user);
                }


                return createduser;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //private async Task SendNotification(dynamic datavalue2)
        //{
        //          string emailId = datavalue2.ts_req_by;
        //          string from = datavalue2.ts_dt_tm_fromtraining;
        //          string to = datavalue2.ts_dt_tm_totraining;
        //          string trainerName = datavalue2.ts_trainer_name;
        //          string topic = datavalue2.ts_topic_name;
        //          string trnNo = datavalue2.ts_training_no;
        //          string title = "Training Schedule Form";

        //	//public async Task SendNote(string emailId, string from, string to, string trainerName, string topic, string title, string trnNo)
        //	//{
        //	// Parse date and time
        //	DateTime dateTime;
        //	string fromdate = "";
        //	string fromtime = "";
        //	string todate = "";
        //	string totime = "";
        //	if (DateTime.TryParse(to, out dateTime))
        //	{
        //		// Parsing successful
        //		// Extract the date and time components
        //		todate = dateTime.ToString("yyyy-MM-dd");
        //		totime = dateTime.ToString("HH:mm:ss");
        //	}
        //          if (DateTime.TryParse(from, out dateTime))
        //          {
        //              // Parsing successful
        //              // Extract the date and time components
        //              fromdate = dateTime.ToString("yyyy-MM-dd");
        //              fromtime = dateTime.ToString("HH:mm:ss");
        //          }
        //	// Email HTML content
        //	//		string htmlContent = "<div style=\"width: 100%; max-width: 100%;\" class=\"container-fluid\">" +
        //	//"<div style=\"display: flex; justify-content: center;\" class=\"row\">" +
        //	//"<div style=\"flex: 0 0 100%; max-width: 100%;\" class=\"col-lg-12 col-md-12\"></div>" +
        //	//"<div style=\"flex: 0 0 100%; max-width: 100%;\" class=\"col-lg-12 col-md-12\">" +
        //	//"<div style=\"padding: 1.25rem; margin: 1rem; box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);\" class=\"card\">" +
        //	//"<div style=\"font-family: Arial, sans-serif;\">" +
        //	//"<h2 style=\"text-align: center;\">Training Schedule</h2>" +
        //	//"<h5 style=\"margin-top: 1.25rem; font-weight: bold;\">Dear Employee,</h5>" +
        //	//"<p style=\"font-weight: bold; margin-top: 1rem;\">Please note the following details for a training schedule</p>" +
        //	//"<p><strong>Training No:</strong> " + trnNo + "</p>" +
        //	//"<p><strong>Date:</strong> " + fromdate + " To " + todate + "</p>" +
        //	//"<p><strong>Time:</strong> " + fromtime + " To " + totime + "</p>" +
        //	//"<p><strong>Trainer Name:</strong> " + trainerName + "</p>" +
        //	//"<p><strong>Topic:</strong> " + topic + "</p>" +
        //	//"</div>" +
        //	//"</div>" +
        //	//"</div>" +
        //	//"<div style=\"flex: 0 0 100%; max-width: 100%;\" class=\"col-lg-12 col-md-12\"></div>" +
        //	//"</div>" +
        //	//"</div>";
        //	// Assuming you have a backend API endpoint to handle the link click
        //	string backendBaseUrl = "http://localhost:3000";
        //	string uniqueIdentifier = "YOUR_UNIQUE_IDENTIFIER"; // Generate or fetch the unique identifier

        //	// Constructing the dynamic link with the unique identifier as a query parameter
        //	//string dynamicLink = $"{backendBaseUrl}/training/view?trainingId={uniqueIdentifier}";
        //	string dynamicLink = $"{backendBaseUrl}/approvalEmail";
        //	// Constructing the HTML content with the dynamic link
        //	string htmlContent = $@"
        //  <div style='width: 100%; max-width: 100%;' class='container-fluid'>
        //      <div style='display: flex; justify-content: center;' class='row'>
        //          <div style='flex: 0 0 100%; max-width: 100%;' class='col-lg-12 col-md-12'></div>
        //          <div style='flex: 0 0 100%; max-width: 100%;' class='col-lg-12 col-md-12'>
        //              <div style='padding: 1.25rem; margin: 1rem; box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);' class='card'>
        //                  <div style='font-family: Arial, sans-serif;'>
        //                      <h2 style='text-align: center;'>Training Schedule</h2>
        //                      <h5 style='margin-top: 1.25rem; font-weight: bold;'>Dear Employee,</h5>
        //                      <p style='font-weight: bold; margin-top: 1rem;'>Please note the following details for a training schedule</p>
        //                      <p><strong>Training No:</strong> {trnNo}</p>
        //                      <p><strong>Date:</strong> {fromdate} To {todate}</p>
        //                      <p><strong>Time:</strong> {fromtime} To {totime}</p>
        //                      <p><strong>Trainer Name:</strong> {trainerName}</p>
        //                      <p><strong>Topic:</strong> {topic}</p>
        //                      <p><a href='{dynamicLink}'>Click here to view and respond</a></p>
        //                  </div>
        //              </div>
        //          </div>
        //          <div style='flex: 0 0 100%; max-width: 100%;' class='col-lg-12 col-md-12'></div>
        //      </div>
        //  </div>";


        //	// Split email addresses
        //	string[] emailAddresses = emailId.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        //              // Retrieve email configuration

        //              // Assuming EmailConfigure is the correct type for input
        //              //var emailDetails = await _emailConfiguration.GetEmail();
        //              EmailConfigure user = new EmailConfigure();
        //		dynamic emailDetails = await _TrainingSchedule.GetEmail(user);


        //		if (emailDetails != null)
        //		{
        //			// Use the retrieved email configuration details to send the email
        //			var message = new MimeMessage();
        //			message.From.Add(new MailboxAddress("Rensa Tubes", emailDetails.Value.Data.email)); // set your email

        //			foreach (string emailAddress in emailAddresses)
        //			{
        //				message.To.Add(new MailboxAddress(null, emailAddress.Trim()));
        //			}

        //			message.Subject = title;
        //			var bodyBuilder = new BodyBuilder();
        //			bodyBuilder.HtmlBody = htmlContent;
        //			message.Body = bodyBuilder.ToMessageBody();

        //			try
        //			{
        //				using (var client = new SmtpClient())
        //				{
        //					// Connect to the SMTP server and send the email
        //					client.Connect(emailDetails.Value.Data.smtp_server, emailDetails.Value.Data.smtp_port,true );//false
        //					client.Authenticate(emailDetails.Value.Data.email, emailDetails.Value.Data.password);
        //					client.Send(message);
        //					client.Disconnect(true);
        //				}
        //			}
        //			catch (Exception ex)
        //			{
        //				// Handle SMTP client errors
        //				Console.WriteLine($"Failed to send email: {ex.Message}");
        //			}
        //		}
        //		else
        //		{
        //			// Handle case where email configuration details are not found
        //			Console.WriteLine("Email configuration details not found.");
        //		}

        //}

        private async Task SendNotification(dynamic datavalue2)
        {
            // Extracting data from datavalue2 object
            string emailId = datavalue2.ts_req_by;
            string from = datavalue2.ts_dt_tm_fromtraining;
            string to = datavalue2.ts_dt_tm_totraining;
            string trainerName = datavalue2.ts_trainer_name;
            string topic = datavalue2.ts_topic_name;
            string trnNo = datavalue2.ts_training_no;
            string title = "Training Schedule Form";
            string name = datavalue2.ts_training_agency; // Assuming emp_name contains names
            string id = datavalue2.ts_empid; // Assuming emp_id contains IDs
            Guid trid = datavalue2.ts_id; // Assuming emp_id contains IDs

            // Parsing date and time
            DateTime fromDateTime, toDateTime;
            string fromdate = "", fromtime = "", todate = "", totime = "";
            if (DateTime.TryParse(from, out fromDateTime))
            {
                fromdate = fromDateTime.ToString("yyyy-MM-dd");
                fromtime = fromDateTime.ToString("HH:mm:ss");
            }
            if (DateTime.TryParse(to, out toDateTime))
            {
                todate = toDateTime.ToString("yyyy-MM-dd");
                totime = toDateTime.ToString("HH:mm:ss");
            }

            // Split names and IDs
            string[] names = name.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string[] ids = id.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            // Split email addresses
            string[] emailAddresses = emailId.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            // Retrieve email configuration
            EmailConfigure user = new EmailConfigure();
            dynamic emailDetails = await _TrainingSchedule.GetEmail(user);

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
                        bodyBuilder.HtmlBody = GenerateHtmlContent(names[i], ids[i], trnNo, fromdate, todate, fromtime, totime, trainerName, topic, trid);
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
                Console.WriteLine("Email configuration details not found.");
            }
        }
        private string GenerateHtmlContent(string name, string id, string trnNo, string fromdate, string todate, string fromtime, string totime, string trainerName, string topic, Guid trid)
        {
            string backendBaseUrl = "http://lms.rensatubes.com:443/";
            string dynamicLink = $"{backendBaseUrl}/approvalEmail?trainingId={trid}&employeeId={id}";


            //string htmlContent = $@"
            // <div style='width: 100%; max-width: 100%;' class='container-fluid'>
            //     <div style='display: flex; justify-content: center;' class='row'>
            //         <div style='flex: 0 0 100%; max-width: 100%;' class='col-lg-12 col-md-12'></div>
            //         <div style='flex: 0 0 100%; max-width: 100%;' class='col-lg-12 col-md-12'>
            //             <div style='padding: 1.25rem; margin: 1rem; box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);' class='card'>
            //                 <div style='font-family: Arial, sans-serif;'>
            //                     <h2 style='text-align: center;'>Training Schedule</h2>
            //                     <h5 style='margin-top: 1.25rem; font-weight: bold;'>Dear {name.Trim()},</h5>
            //                     <p><a href='{dynamicLink}'>Click here to view and respond</a></p>
            //                     <p style='font-weight: bold; margin-top: 1rem;'>Please note the following details for a training schedule</p>
            //                     <p><strong>Training No:</strong> {trnNo}</p>
            //                     <p><strong>Date:</strong> {fromdate} To {todate}</p>
            //                     <p><strong>Time:</strong> {fromtime} To {totime}</p>
            //                     <p><strong>Trainer Name:</strong> {trainerName}</p>
            //                     <p><strong>Topic:</strong> {topic}</p>
            //                 </div>
            //             </div>
            //         </div>
            //         <div style='flex: 0 0 100%; max-width: 100%;' class='col-lg-12 col-md-12'></div>
            //     </div>
            // </div>";
            string htmlContent = $@"
<div style='width: 100%; background-color: #f8f9fa; padding: 20px 0;'>
    <div style='max-width: 600px; margin: 0 auto;'>
        <div style='box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15); border-radius: 0.5rem; overflow: hidden;'>
            <div style='background-color: #ffffff; padding: 20px;'>
                <h1 style='text-align: center; margin-top: 0;'>Training Schedule</h1>
                <h3 style='font-weight: bold; margin-top: 2rem;'>Dear {name.Trim()},</h3>
                <p style='font-weight: bold; margin-top: 1rem;'>{name.Trim()}, your training is scheduled.</p>
                <p style='margin-top: 1.5rem;'><a href='{dynamicLink}' style='text-decoration: none; color: #007bff;'>Click here to view and respond</a></p>
                <p style='font-weight: bold; margin-top: 1.5rem;'>Please note the following details for the training schedule:</p>
                <p><strong>Training No:</strong> {trnNo}</p>
                <p><strong>Date:</strong> {fromdate} to {todate}</p>
                <p><strong>Time:</strong> {fromtime} to {totime}</p>
                <p><strong>Trainer Name:</strong> {trainerName}</p>
                <p><strong>Topic:</strong> {topic}</p>
            </div>
        </div>
    </div>
</div>";


            return htmlContent;
        }

        [HttpGet("GetAllCalenderPlot")]
        public async Task<IActionResult> GetAllCalenderPlot(Guid user_id)
        {
            try
            {
                TrainingSchedule user = new TrainingSchedule();
                user.UserId = user_id;


                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAllCalenderPlot";


                var createduser = await _TrainingSchedule.TrainingSchedule(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpPost("UpdateTag")]
        public async Task<IActionResult> UpdateTag([FromBody] TrainingSchedule user)
        {
            dynamic createduser;
            try
            {
                //TopicMaster user = new TopicMaster();
                //user.t_id = t_id;
                user.ts_updateddate = DateTime.Now;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "UpdateTag";

                if (user.ts_tag == "Accepted")
                {
                    createduser = await _TrainingSchedule.TrainingSchedule(user);
                    var outcomeidvalue = createduser.Value.Outcome.OutcomeId;
                    if (outcomeidvalue == 1)
                    {

                        var datavalue = createduser.Value.Outcome.OutcomeDetail;

                        Guid ts_id;
                        if (Guid.TryParse(datavalue, out ts_id))
                        {

                            user.ts_id = ts_id;


                            user.BaseModel.OperationType = "selectEmployeedataToHr";


                            dynamic createduser2 = await _TrainingSchedule.Get(user);
                            var datavalue2 = createduser2.Value.Data;
                            await SendNotificationHR(datavalue2);

                        }




                    }

                }
                else
                {
                    createduser = await _TrainingSchedule.TrainingSchedule(user);
                    var outcomeidvalue = createduser.Value.Outcome.OutcomeId;
                    if (outcomeidvalue == 1)
                    {

                        var datavalue = createduser.Value.Outcome.OutcomeDetail;

                        Guid ts_id;
                        if (Guid.TryParse(datavalue, out ts_id))
                        {

                            user.ts_id = ts_id;


                            user.BaseModel.OperationType = "selectEmployeedataToHod";


                            dynamic createduser2 = await _TrainingSchedule.Get(user);
                            var datavalue2 = createduser2.Value.Data;
                            await SendNotificationHOD(datavalue2);

                        }




                    }
                }


                return createduser;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private async Task SendNotificationHR(dynamic datavalue2)
        {
            string emailId = datavalue2.ts_req_by;
            string topic = datavalue2.ts_topic_name;
            string empname = datavalue2.ts_empid;
            string to = datavalue2.ts_dt_tm_totraining;
            string from = datavalue2.ts_dt_tm_fromtraining;


            string trnNo = datavalue2.ts_training_no;
            string title = "Employee Training Acceptance Confirmation";

            DateTime fromDateTime, toDateTime;
            string fromdate = "", fromtime = "", todate = "", totime = "";
            if (DateTime.TryParse(from, out fromDateTime))
            {
                fromdate = fromDateTime.ToString("yyyy-MM-dd");
                fromtime = fromDateTime.ToString("HH:mm:ss");
            }
            if (DateTime.TryParse(to, out toDateTime))
            {
                todate = toDateTime.ToString("yyyy-MM-dd");
                totime = toDateTime.ToString("HH:mm:ss");
            }
            string htmlContent = @"
<div style='width: 100%; background-color: #f5f5f5; padding: 20px 0;'>
    <div style='max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 10px; box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);'>
        <div style='padding: 20px;'>
            <h2 style='text-align: center; margin-top: 0;'>Employee Training Acceptance Confirmation</h2>
            <h4 style='font-weight: bold; margin-top: 23px;'>Dear HR,</h4>
            <p style='margin-top: 20px;'><strong>Training No:</strong> " + trnNo + @"</p>
            <p><strong>" + empname + @"</strong> has accepted the training!</p>
        </div>
    </div>
</div>";



            // Split email addresses
            string[] emailAddresses = emailId.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            // Retrieve email configuration

            // Assuming EmailConfigure is the correct type for input
            //var emailDetails = await _emailConfiguration.GetEmail();
            EmailConfigure user = new EmailConfigure();
            dynamic emailDetails = await _TrainingSchedule.GetEmail(user);


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
        private async Task SendNotificationHOD(dynamic datavalue2)
        {
            string emailToId = datavalue2.ts_req_by;
            string emailIdCC = datavalue2.ts_remark;
            string topic = datavalue2.ts_topic_name;
            string empname = datavalue2.ts_empid;
            string to = datavalue2.ts_dt_tm_totraining;
            string from = datavalue2.ts_dt_tm_fromtraining;


            string trnNo = datavalue2.ts_training_no;
            string title = "Employee Training Rejection Confirmation";

            DateTime fromDateTime, toDateTime;
            string fromdate = "", fromtime = "", todate = "", totime = "";
            if (DateTime.TryParse(from, out fromDateTime))
            {
                fromdate = fromDateTime.ToString("yyyy-MM-dd");
                fromtime = fromDateTime.ToString("HH:mm:ss");
            }
            if (DateTime.TryParse(to, out toDateTime))
            {
                todate = toDateTime.ToString("yyyy-MM-dd");
                totime = toDateTime.ToString("HH:mm:ss");
            }

            string htmlContent = @"
<div style='width: 100%; background-color: #f5f5f5; padding: 20px 0;'>
    <div style='max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 10px; box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);'>
        <div style='padding: 20px;'>
            <h2 style='text-align: center; margin-top: 0;'>Employee Training Rejection Confirmation</h2>
            <h4 style='font-weight: bold; margin-top: 23px;'>Dear HR,</h4>
            <p style='margin-top: 20px;'><strong>Training No:</strong> " + trnNo + @"</p>
            <p><strong>" + empname + @"</strong> has rejected the training!</p>
        </div>
    </div>
</div>";



            // Split email addresses
            string[] emailAddresses = emailToId.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string[] ccAddresses = emailIdCC.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            // Retrieve email configuration

            // Assuming EmailConfigure is the correct type for input
            //var emailDetails = await _emailConfiguration.GetEmail();
            EmailConfigure user = new EmailConfigure();
            dynamic emailDetails = await _TrainingSchedule.GetEmail(user);


            if (emailDetails != null)
            {
                try
                {
                    using (var client = new SmtpClient())
                    {
                        client.Connect(emailDetails.Value.Data.smtp_server, emailDetails.Value.Data.smtp_port, false);

                        foreach (string emailAddress in emailAddresses)
                        {
                            var message = new MimeMessage();
                            message.From.Add(new MailboxAddress("Rensa Tubes", emailDetails.Value.Data.email));
                            message.To.Add(new MailboxAddress(null, emailAddress.Trim()));

                            // Add CC recipients
                            foreach (string ccAddress in ccAddresses)
                            {
                                message.Cc.Add(new MailboxAddress(null, ccAddress.Trim()));
                            }

                            message.Subject = title;
                            var bodyBuilder = new BodyBuilder();
                            bodyBuilder.HtmlBody = htmlContent;
                            message.Body = bodyBuilder.ToMessageBody();

                            // Send the individual email
                            client.Authenticate(emailDetails.Value.Data.email, emailDetails.Value.Data.password);
                            client.Send(message);
                        }

                        client.Disconnect(true);
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



        [HttpGet("GetByCode")]
        public async Task<IActionResult> GetByCode(Guid? UserId, string emp_code)
        {
            TrainingSchedule user = new TrainingSchedule();
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }
            user.UserId = UserId;
            user.ts_empid = emp_code;
            user.BaseModel.OperationType = "GetByCode";
            try
            {
                var parameter = await _TrainingSchedule.Get(user);
                return parameter;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}