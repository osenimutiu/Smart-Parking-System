using SmartParkingSystem.Contracts;
using System.Net.Mail;
using System.Net;

namespace SmartParkingSystem.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly RepositoryContext _context;
        private readonly IConfiguration _configuration;

        public NotificationRepository(RepositoryContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        //public void SendEmail(string toEmail, string Location, DateTime StartDate, DateTime EndDate)
        //{
        //    string subject = "Smart Parking System - Booking Confirmation";
        //    var smtpClient = new SmtpClient(_configuration["EmailConfiguration:SmtpServer"])
        //    {
        //        Port = 465,
        //        Credentials = new NetworkCredential(_configuration["EmailConfiguration:From"], _configuration["EmailConfiguration:Password"]),
        //        EnableSsl = true,
        //    };
            
        //    var mailMessage = new MailMessage
        //    {
        //        From = new MailAddress(_configuration["EmailConfiguration:From"]),
        //        Subject = subject,
        //        Body = MailBody(Location,StartDate,EndDate),
        //        IsBodyHtml = true,
        //    };
        //    try
        //    {
        //        mailMessage.To.Add(toEmail);
        //        smtpClient.Send(mailMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
            
        //}

        public bool SendEmail(string toEmail, string Location, DateTime StartDate, DateTime EndDate, bool IsBodyHtml = false)
        {

            bool status = false;
            string subject = "Smart Parking System - Booking Confirmation";
            try
            {
                string HostAddress = _configuration["EmailConfiguration:SmtpServer"];
                string FormEmailId = _configuration["EmailConfiguration:From"];
                string Password = _configuration["EmailConfiguration:Password"];
                string Port = _configuration["EmailConfiguration:Port"];
                string body = MailBody(Location, StartDate, EndDate);
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(FormEmailId);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = IsBodyHtml;
                mailMessage.To.Add(new MailAddress(toEmail));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = HostAddress;
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential();
                networkCredential.UserName = mailMessage.From.Address;
                networkCredential.Password = Password;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCredential;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                smtp.Port = Convert.ToInt32(Port);
                try
                {
                    smtp.Send(mailMessage);
                }
                catch (Exception ex)
                {

                    throw;
                }

                status = true;
                return status;
            }
            catch (Exception e)
            {
                return status;
            }
        }


        private string MailBody(string Location,DateTime StartDate, DateTime EndDate)
        {
            string body = $@"
        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    margin: 0;
                    padding: 0;
                }}
                .container {{
                    max-width: 600px;
                    margin: 20px auto;
                    background: #ffffff;
                    padding: 20px;
                    border-radius: 8px;
                    box-shadow: 0px 0px 10px 0px #cccccc;
                }}
                h2 {{
                    color: #333366;
                }}
                .content {{
                    padding: 10px;
                    font-size: 16px;
                    line-height: 1.6;
                }}
                .footer {{
                    margin-top: 20px;
                    text-align: center;
                    font-size: 12px;
                    color: #777777;
                }}
                .button {{
                    display: inline-block;
                    padding: 10px 20px;
                    font-size: 16px;
                    color: #ffffff;
                    background-color: #28a745;
                    text-decoration: none;
                    border-radius: 5px;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <h2>Booking Confirmation</h2>
                <div class='content'>
                    <p>Dear Customer,</p>
                    <p>Your parking space booking has been confirmed. Please find the details below:</p>
                    <ul>
                        <li><strong>Location:</strong>{Location}</li>
                        <li><strong>From:</strong>{StartDate}</li>
                        <li><strong>To:</strong>{EndDate}</li>
                    </ul>
                </div>
                <div class='footer'>
                    <p>Smart Parking System © 2025. All rights reserved.</p>
                </div>
            </div>
        </body>
        </html>";
            return body;
        }

    }
}
