using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zika.Services;

namespace Zika.Helper
{
    public class EmailHelper
    {
        private readonly IEmailSender _email;
        public EmailHelper(IEmailSender email)
        {
            _email = email;
        }
        public async Task<bool> Send(string email, string emailSubject, string emailMessage)
        {
            try
            {
                await _email.SendEmailAsync(email, emailSubject, emailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Send(string[] emails, string emailSubject, string emailMessage)
        {
            try
            {
                await _email.SendEmailToAllAsync(emails, emailSubject, emailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
