using Context;
using Dapper;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Data;
using System.Net.Http;


namespace Common
{
    public class SendNotification
    {
        //private readonly IConfiguration _configuration;
  //      private readonly EmailConfiguration _emailConfiguration;

		//public SendNotification(EmailConfiguration emailConfiguration)
		//{
		//	_emailConfiguration = emailConfiguration;
		//}

		//public async Task SendNote(string emailId, string from, string to, string trainerName, string topic, string title, string trnNo)
  //      {
  //          // Parse date and time
  //          DateTime fromDate, toDate;
  //          if (!DateTime.TryParse(from, out fromDate) || !DateTime.TryParse(to, out toDate))
  //          {
  //              // Handle parsing errors
  //              return;
  //          }

  //          // Email HTML content
  //          string htmlContent = $@"
  //      <div style='width: 100%; max-width: 100%;' class='container-fluid'>
  //          <div style='display: flex; justify-content: center;' class='row'>
  //              <div style='flex: 0 0 100%; max-width: 100%;' class='col-lg-12 col-md-12'></div>
  //              <div style='flex: 0 0 100%; max-width: 100%;' class='col-lg-12 col-md-12'>
  //                  <div style='padding: 1.25rem; margin: 1rem; box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);' class='card'>
  //                      <div style='font-family: Arial, sans-serif;'>
  //                          <h2 style='text-align: center;'>Training Schedule</h2>
  //                          <h5 style='margin-top: 1.25rem; font-weight: bold;'>Dear Employee,</h5>
  //                          <p style='font-weight: bold; margin-top: 1rem;'>Please note the following details for a training schedule</p>
  //                          <p><strong>Training No:</strong> {trnNo}</p>
  //                          <p><strong>Date:</strong> {fromDate:yyyy-MM-dd} To {toDate:yyyy-MM-dd}</p>
  //                          <p><strong>Time:</strong> {fromDate:HH:mm:ss} To {toDate:HH:mm:ss}</p>
  //                          <p><strong>Trainer Name:</strong> {trainerName}</p>
  //                          <p><strong>Topic:</strong> {topic}</p>
  //                      </div>
  //                  </div>
  //              </div>
  //              <div style='flex: 0 0 100%; max-width: 100%;' class='col-lg-12 col-md-12'></div>
  //          </div>
  //      </div>";

  //          // Split email addresses
  //          string[] emailAddresses = emailId.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

  //          // Retrieve email configuration

  //          EmailConfigure input = new EmailConfigure(); // Assuming EmailConfigure is the correct type for input
  //          var emailDetails = await _emailConfiguration.GetEmail();


  //          if (emailDetails != null)
  //          {
  //              // Use the retrieved email configuration details to send the email
  //              var message = new MimeMessage();
  //              message.From.Add(new MailboxAddress("Initial Infinity PrintSoft", "")); // set your email

  //              foreach (string emailAddress in emailAddresses)
  //              {
  //                  message.To.Add(new MailboxAddress(null, emailAddress.Trim()));
  //              }

  //              message.Subject = title;
  //              var bodyBuilder = new BodyBuilder();
  //              bodyBuilder.HtmlBody = htmlContent;
  //              message.Body = bodyBuilder.ToMessageBody();

  //              try
  //              {
  //                  using (var client = new SmtpClient())
  //                  {
  //                      // Connect to the SMTP server and send the email
  //                      client.Connect(emailDetails.smtp_server,465, true);
  //                      client.Authenticate(emailDetails.email, emailDetails.password);
  //                      client.Send(message);
  //                      client.Disconnect(true);
  //                  }
  //              }
  //              catch (Exception ex)
  //              {
  //                  // Handle SMTP client errors
  //                  Console.WriteLine($"Failed to send email: {ex.Message}");
  //              }
  //          }
  //          else
  //          {
  //              // Handle case where email configuration details are not found
  //              Console.WriteLine("Email configuration details not found.");
  //          }
  //      }
    }

  //  public void SendNo(string Password,string title)
  //      {
		//	string[] parts = Password.Split(';');
  //          string userpassword = "";
  //          string username = "";

  //          string email = "";
		//	if (parts.Length == 3)
		//	{
		//		 userpassword = parts[0]; // Extract the password part
		//		email = parts[1];    // Extract the email part
		//		username = parts[2];    // Extract the email part

		//		// Now you can use the password and email variables as needed
				
		//	}
		//	string htmlContent = "<div style=\"font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;\">" +
		//				"<div style=\"max-width: 600px; margin: 0 auto; background-color: #fff; padding: 20px; border-radius: 5px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\">" +
		//					"<div style=\"text-align: center;\">" +
		//						"<h1 style=\"margin: 0; font-size: 28px;\">LMS</h1>" +							
		//					"</div>" +
		//					"<div style=\"text-align: center; margin-top: 20px;\">" +
		//						"<h2 style=\"margin: 0;\">Get started</h2>" +
		//						"<p style=\"margin: 10px 0; font-size: 16px;\">Your account has been created on the LMS. Below are your system generated credentials.</p>" +
		//						"<p style=\"margin: 10px 0; font-size: 16px;\">Please use this credentials for login</p>" +
		//						"<div style=\"text-align: center; margin-top: 20px;\">" +
		//							"<p style=\"margin: 5px 0; font-size: 16px;\"><strong>Username:</strong> " + username + " </p>" +
		//							"<p style=\"margin: 5px 0; font-size: 16px;\"><strong>Password:</strong> " + userpassword + " </p>" +
		//						"</div>" +
								
		//					"</div>" +							
		//				"</div>" +
		//			"</div>";


		//	var message = new MimeMessage();
		//	message.From.Add(new MailboxAddress("Initial Infinity PrintSoft", "info@initialInfinity.com")); // set your email

		//   message.To.Add(new MailboxAddress(null, email.Trim()));
			

		//	message.Subject = title;
		//	var bodyBuilder = new BodyBuilder();
		//	bodyBuilder.HtmlBody = htmlContent;
		//	message.Body = bodyBuilder.ToMessageBody();

		//	using (var client = new SmtpClient())
		//	{
		//		client.Connect("smtpout.secureserver.net", 465, true); // SMTP server and port
		//		client.Authenticate("info@initialInfinity.com", "Feb#20244"); // Your email address and password
		//		client.Send(message);
		//		client.Disconnect(true);
		//	}
		//}

	}
