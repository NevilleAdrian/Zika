using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Zika.Data;
using Zika.Models;
using Zika.Services;

namespace Zika.Repository
{
    public class UsersRepository
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IEmailSender _email;
        private readonly ILogger<UsersRepository> _log;
        public UsersRepository(ApplicationDbContext context,
            ILogger<UsersRepository> logger,
            IEmailSender email)
        {
            _ctx = context;
            _log = logger;
            _email = email;
        }

        public async Task<bool> Add(Email email)
        {
            try
            {
                _ctx.Emails.Add(email);
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.LogInformation($"Cannot create emails: {ex.Message}");
                return false;
            }
        }

        public async Task<Email> Item(int id)
        {
            if (Exists(id))
            {
                return await _ctx.Emails.Where(x => x.EmailId == id).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<List<ApplicationUser>> Items() => await _ctx.Users.ToListAsync() ?? new List<ApplicationUser>();
        public async Task<Email> Itemsx() => await _ctx.Emails.FirstOrDefaultAsync() ?? null;

        public async Task<bool> Update(Email email)
        {
            if (Exists(email.EmailId))
            {
                try
                {
                    _ctx.Emails.Update(email);
                    await _ctx.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _log.LogInformation($"Cannot update faq: {ex.Message}");

                }
            }
            return false;
        }

        public async Task<bool> SendEmailsToList(string message, string subject)
        {
            var item = await _ctx.Emails.Select(x => x.UserEmails).FirstOrDefaultAsync();
            var emails = item.Split(',');

            try
            {
                await _email.SendEmailToAllAsync(emails, subject, message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendEmailsToAll(string message, string subject)
        {
            var item = await _ctx.Emails.Select(x => x.UserEmails).FirstOrDefaultAsync();
            var users = await _ctx.Users.Select(x => x.Email).ToArrayAsync();
            var emails = item.Split(',');
            var finalArray = users.Concat(emails).ToArray();

            try
            {
                await _email.SendEmailToAllAsync(finalArray, subject, message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            if (Exists(id))
            {
                var interest = await _ctx.Emails.FindAsync(id);
                try
                {
                    _ctx.Emails.Remove(interest);
                    await _ctx.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _log.LogInformation($"Cannot delete faq: {ex.Message}");

                }
            }
            return false;
        }

        private bool Exists(int id) => _ctx.Emails.Any(x => x.EmailId == id);
    }
}
