using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Palmfit.Data.AppDbContext;
using Data.Entities;

namespace Core.Repositories.AuthRepository 
{
    public class AuthRepo : IAuthRepo
    {
        private readonly PalmfitDbContext _palmfitDbContext; 

        public AuthRepo(PalmfitDbContext palmfitDbContext) 
        {
            _palmfitDbContext = palmfitDbContext;
        }



            public string SendOTPByEmail(string email, string otp)
            {
                try
                {
                    // Replace these with your SMTP server settings and credentials
                    string smtpServer = "smtppro.zoho.com";
                    int smtpPort = 587;
                    string smtpUsername = "info@enema.ng";
                    string smtpPassword = "-8qijjUd";

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(smtpUsername);
                        mailMessage.To.Add(email);
                        mailMessage.Subject = "One-Time Password (OTP)";
                        mailMessage.Body = $"Your OTP: {otp}";

                        using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                        {
                            smtpClient.EnableSsl = true;
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                            smtpClient.Send(mailMessage);
                        }
                    }

                    return $"OTP sent to {email}";
                }
                catch (Exception ex)
                {
                    // Handle any exception that might occur during email sending
                    return $"Failed to send OTP to {email}. Error: {ex.Message}";
                }
            }



            public void SaveOTPInUserData(string email, string otp)
            {
                var userOTP = new UserOTP
                {
                    Email = email,
                    OTP = otp,
                    Expiration = DateTime.UtcNow.AddMinutes(5) // Set an expiration time for the OTP (e.g., 5 minutes in this example)
                };

                _palmfitDbContext.UserOTPs.Add(userOTP);
                _palmfitDbContext.SaveChanges();
            }


            public string GenerateOTP()
            {
                // Generate a 6-digit OTP (you can adjust the length as needed)
                Random random = new Random();
                int otpValue = random.Next(100000, 999999);
                return otpValue.ToString();
            }

    }
    }

