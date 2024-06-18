using common;
using Common;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Tokens;
using Master.Entity;
using Master.Repository.Interface;
using MimeKit;
using MailKit.Net.Smtp;

namespace Master.Controllers 
{

    [Route("api/[controller]")]
    [ApiController]
    
    public class UserMasterController : Controller
    {
        string Server_Value = "";
        public readonly IUserMasterRepository _userMasterRepo;
        public UserMasterController(IUserMasterRepository userMasterRepo)
        {
            _userMasterRepo = userMasterRepo;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(Guid UserId, string? Status)
        {
            try
            {
                if(Status== null)
                {
                    Status = "1";
                }
                UserMaster user = new UserMaster();
                string comp = UserId.ToString();
                user.UserId = UserId;
               
                user.um_isactive = Status;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.Server_Value = Server_Value;
                user.BaseModel.OperationType = "GetAll";
                var createduser = await _userMasterRepo.UserMaster(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetStaff")]
        public async Task<IActionResult> GetStaff(Guid?UserId)
        {
            UserMaster user= new UserMaster();
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }
            user.UserId = UserId;
       
            user.BaseModel.Server_Value = Server_Value;
            user.BaseModel.OperationType = "GetStaff";
            try
            {
                var parameter = await _userMasterRepo.StaffGet(user);
                return parameter;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid? UserId, Guid? um_id)
        {
            UserMaster user = new UserMaster();
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }
            user.UserId = UserId;
            user.um_id = um_id;
         
            user.BaseModel.Server_Value = Server_Value;
            user.BaseModel.OperationType = "Get";
            try
            {
                var parameter = await _userMasterRepo.Get(user);
                return parameter;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] UserMaster user)
        {
            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
             
                if (user.um_id == null)
                {
                    user.BaseModel.OperationType = "Insert";
                }
                else
                {
                    user.um_updateddate = DateTime.Now;
                    user.BaseModel.OperationType = "Update";
                }
				dynamic createduser = await _userMasterRepo.UserMaster(user);
                var outcomeidvalue = createduser.Value.Outcome.OutcomeId;

				if (outcomeidvalue == 1)
                {
                    //var datavalue = createduser.Value.Data;
                    //SendNotification SN = new SendNotification();
                    //SN.SendNo(datavalue.OutcomeDetail, "Training Shedule Form", "training is sheduled");
                    var datavalue = createduser.Value.Outcome.OutcomeDetail;
					//SendNotification SN = new SendNotification();

					//SN.SendNo(datavalue ,"Login Credentials");
					await SendNo(datavalue);
				}
                    return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

		private async Task SendNo(dynamic datavalue2)
		{
			
			string[] parts = datavalue2.Split(';');
			string userpassword = "";
			string username = "";
            string title = "Login Credentials";
			string email = "";
			if (parts.Length == 3)
			{
				userpassword = parts[0]; // Extract the password part
				email = parts[1];    // Extract the email part
				username = parts[2];    // Extract the email part

				// Now you can use the password and email variables as needed

			}
			string htmlContent = "<div style=\"font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;\">" +
						"<div style=\"max-width: 600px; margin: 0 auto; background-color: #fff; padding: 20px; border-radius: 5px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\">" +
							"<div style=\"text-align: center;\">" +
								"<h1 style=\"margin: 0; font-size: 28px;\">LMS</h1>" +
							"</div>" +
							"<div style=\"text-align: center; margin-top: 20px;\">" +
								"<h2 style=\"margin: 0;\">Get started</h2>" +
								"<p style=\"margin: 10px 0; font-size: 16px;\">Your account has been created on the LMS. Below are your system generated credentials.</p>" +
								"<p style=\"margin: 10px 0; font-size: 16px;\">Please use this credentials for login</p>" +
								"<div style=\"text-align: center; margin-top: 20px;\">" +
									"<p style=\"margin: 5px 0; font-size: 16px;\"><strong>Username:</strong> " + username + " </p>" +
									"<p style=\"margin: 5px 0; font-size: 16px;\"><strong>Password:</strong> " + userpassword + " </p>" +
								"</div>" +

							"</div>" +
						"</div>" +
					"</div>";

			// Split email addresses
			EmailConfigure user = new EmailConfigure();
			dynamic emailDetails = await _userMasterRepo.GetEmailId(user);
			
			
			


				if (emailDetails != null)
				{
					// Use the retrieved email configuration details to send the email
					var message = new MimeMessage();
					message.From.Add(new MailboxAddress("Rensa Tubes", emailDetails.Value.Data.email)); // set your email
				message.To.Add(new MailboxAddress(null, email.Trim()));

				message.Subject = title;
					var bodyBuilder = new BodyBuilder();
					bodyBuilder.HtmlBody = htmlContent;
					message.Body = bodyBuilder.ToMessageBody();

					try
					{
						using (var client = new SmtpClient())
						{
							// Connect to the SMTP server and send the email
							client.Connect(emailDetails.Value.Data.smtp_server, emailDetails.Value.Data.smtp_port, false);
							client.Authenticate(emailDetails.Value.Data.email, emailDetails.Value.Data.password);
							client.Send(message);
							client.Disconnect(true);
						}
					}
					catch (Exception ex)
					{
						// Handle SMTP client errors
						Console.WriteLine($"Failed to send email: {ex.Message}");
					}
				}
				else
				{
					// Handle case where email configuration details are not found
					Console.WriteLine("Email configuration details not found.");
				}
			
		}




		[HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] UserMaster user)
        {
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }
          
            user.BaseModel.OperationType = "Delete";
            var usertDetails = await _userMasterRepo.UserMaster(user);
            return usertDetails;
        }

        [HttpGet("GetExcel")]
        public async Task<IActionResult> GetExcel(Guid UserId, string status, string Server_Value)
        {
            try
            {
                UserMaster user = new UserMaster();
                user.UserId = UserId;
                
                user.um_isactive = status;
                
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                    user.BaseModel.Server_Value = Server_Value;
                }
                user.BaseModel.OperationType = "GetAll";
                dynamic createduser = await _userMasterRepo.UserMaster(user);
                dynamic data1 = ((Tokens.Result)((Microsoft.AspNetCore.Mvc.ObjectResult)createduser).Value).Data;
                DataTable data = new DataTable();
                if (data1 is List<object> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        var firstItem = dataList[0] as IDictionary<string, object>;
                        if (firstItem != null)
                        {
                            foreach (var kvp in firstItem)
                            {
                                data.Columns.Add(kvp.Key);
                            }
                        }
                        foreach (var item in dataList)
                        {
                            var values = item as IDictionary<string, object>;
                            if (values != null)
                            {
                                var row = data.NewRow();
                                foreach (var kvp in values)
                                {
                                    row[kvp.Key] = kvp.Value;
                                }
                                data.Rows.Add(row);
                            }
                        }
                    }
                }
                ExportRepository export = new ExportRepository();
                var result = new Result
                {
                    Data = export.DataTableToJsonObj(data)
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetPdf")]
        public async Task<IActionResult> GetPdf(Guid? UserId, string status)
        {
            try
            {
                UserMaster user = new UserMaster();
                user.UserId = UserId;
          
                user.um_isactive = status;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                    user.BaseModel.Server_Value = Server_Value;
                }
                user.BaseModel.OperationType = "GetAll";
                dynamic createdUser = await _userMasterRepo.UserMaster(user);
                dynamic data12 = ((Tokens.Result)((Microsoft.AspNetCore.Mvc.ObjectResult)createdUser).Value).Data;
                DataTable data = new DataTable();
                if (data12 is List<object> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        var firstItem = dataList[0] as IDictionary<string, object>;
                        if (firstItem != null)
                        {
                            foreach (var kvp in firstItem)
                            {
                                data.Columns.Add(kvp.Key);
                            }
                        }
                        foreach (var item in dataList)
                        {
                            var values = item as IDictionary<string, object>;
                            if (values != null)
                            {
                                var row = data.NewRow();
                                foreach (var kvp in values)
                                {
                                    row[kvp.Key] = kvp.Value;
                                }
                                data.Rows.Add(row);
                            }
                        }
                    }
                }
                string htmlContent = "<div style='margin-top: 5rem; padding-left: 3rem; padding-right: 3rem; margin-bottom: 5rem; border: double;'>";
                htmlContent += "    <div style='text-align: center; line-height: 1; margin-bottom: 2rem;'>";
                htmlContent += "        <h3 style='font-weight: bold;'>User Master</h3>";
                htmlContent += "    </div>";
                htmlContent += "    <table style='width:100%; border-collapse: collapse; margin-top: 10px'>";
                htmlContent += "        <thead>";
                htmlContent += "            <tr>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Sr.No</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Staff Name</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>User Name</th>";
                //htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Role</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Status</th>";
                htmlContent += "            </tr>";
                htmlContent += "        </thead>";
                htmlContent += "        <tbody>";
                int a=0;
                foreach (DataRow row in data.Rows)
                {
                    a++;
                    htmlContent += "<tr style='border: 1px solid black;'>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + a + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + row["um_staffname"].ToString() + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + row["um_user_name"].ToString() + "</td>";
                    //htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + row["um_rolename"].ToString() + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + row["um_isactive"].ToString() + "</td>";
                    htmlContent += "</tr>";
                }
                htmlContent += "        </tbody>";
                htmlContent += "    </table>";
                htmlContent += "</div>";
                string date = DateTime.Now.ToString("dd-MM-yyyy--HH-mm");
                return Ok(htmlContent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
