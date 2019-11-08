using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zika.Data;
using Zika.Extensions;
using Zika.Helper;
using Zika.Models;
using Zika.Services;
using Zika.ViewModels;

namespace Zika.Repository
{
    public class FreightRepository
    {
        private readonly ApplicationDbContext _ctx;
        private readonly ILogger<FreightRepository> _log;
        private readonly IEmailSender _email;
        private readonly UserManager<ApplicationUser> _user;
        public FreightRepository(ApplicationDbContext context,
            ILogger<FreightRepository> logger,
            IEmailSender email,
            UserManager<ApplicationUser> user)
        {
            _ctx = context;
            _log = logger;
            _email = email;
            _user = user;
        }

        public async Task<bool> Add(Freight freight)
        {
            try
            {
                _ctx.Freights.Add(freight);
                await _ctx.SaveChangesAsync();

                EmailHelper emailHelper = new EmailHelper(_email);
                ApplicationUser user = await _user.Users.Where(x => x.Id == freight.UserId).FirstOrDefaultAsync();
                var subject = "FREIGHT RECEIVED";
                StringBuilder message = new StringBuilder($"Dear {user.LastName},<br/>", 200);
                message.Append($"We recieved your freight order.<br/><br/>Please keep your tracking Id safe. <br/><br/>This is your tracking ID: '{freight.FreightId}'<br/><br/>");
                message.Append($"Your '{freight.FreightFrom}' address is: <br/><br/>");
                message.Append(freight.FreightFrom == "USA" ? @"<address>
                    Airport Industrial Office Park, Building B6A, <br/>
                    145 Hook Creek Blvd, Valley Stream,<br/>
                    New York <br/>
                    New York 11581 <br/>
                    United States <br/>
                    <abbr title = 'Phone'> P:</abbr>
                         6465132670
                     </address>" : @"<address>
                    Unit 1, Loughborough Centre<br/>
                    105 Angel Road Brixton<br/>
                    London United Kingdom<br/>
                    Sw9 7PD
                </address>");
                message.Append("<br/>Thank you.");
                bool result = await emailHelper.Send(user.Email, subject, message.ToString());

                return true;
            }
            catch (Exception ex)
            {
                _log.LogInformation($"Cannot create Freight: {ex.Message}");
                return false;
            }
        }

        public async Task<Freight> Item(int id)
        {
            if (Exists(id))
            {
                return await _ctx.Freights.Where(x => x.FreightId == id).Include(x => x.User).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<List<Freight>> Items() => await _ctx.Freights.Include(x => x.User).ToListAsync() ?? new List<Freight>();

        public async Task<bool> Update(Freight freight)
        {
            if (Exists(freight.FreightId))
            {
                Freight freightInit = await _ctx.Freights.Where(x => x.FreightId == freight.FreightId).Include(x => x.User).AsNoTracking().FirstOrDefaultAsync();
                

                try
                {
                    _ctx.Freights.Update(freight);

                    if (freight.Status != freightInit.Status)
                    {
                        var subject = "FREIGHT STATUS NOTIFICATION";
                        StringBuilder message = new StringBuilder($"Dear {freightInit.User.LastName},<br/>", 100);
                        message.Append($"The status of your freight has changed from '{freightInit.Status.StatusReadable()}' to '{freight.Status.StatusReadable()}'<br/><br/>");
                        message.Append("Thank you.");

                        var userEmail = freightInit.User.Email;
                        EmailHelper emailHelper = new EmailHelper(_email);
                        bool result = await emailHelper.Send(userEmail, subject, message.ToString());
                        if (!result)
                        {
                            _log.LogInformation("Could not send email");
                        }

                    }

                    if (freight.Mass > 0 && freightInit.Mass == 0)
                    {
                        var subject = "CONFIRMATION OF FREIGHT MASS";
                        StringBuilder message = new StringBuilder($"Dear {freightInit.User.LastName},<br/>", 100);
                        message.Append($"The freight with freight number {freight.FreightId} has been weighed. It weighs {freight.Mass}.<br/><br/>");
                        message.Append("Warm wishes.");

                        var userEmail = freightInit.User.Email;
                        EmailHelper emailHelper = new EmailHelper(_email);
                        bool result = await emailHelper.Send(userEmail, subject, message.ToString());
                        if (!result)
                        {
                            _log.LogInformation("Could not send email");
                        }

                        
                    }

                    if (freight.Amount > 0 && freightInit.Amount == 0)
                    {
                        var subject = "FREIGHT PAYMENT NOTIFICATION";
                        StringBuilder message = new StringBuilder($"Dear {freightInit.User.LastName},<br/>", 100);
                        message.Append($"Your freight details has been billed. An invoice will be sent to you shortly.<br/><br/>");
                        message.Append("Thank you.");

                        var userEmail = freightInit.User.Email;
                        EmailHelper emailHelper = new EmailHelper(_email);
                        bool result = await emailHelper.Send(userEmail, subject, message.ToString());
                        if (!result)
                        {
                            _log.LogInformation("Could not send email");
                        }
                    }

                    if (freight.HasPaid && !freightInit.HasPaid)
                    {
                        var subject = "PAYMENT CONFIRMATION";
                        StringBuilder message = new StringBuilder($"Dear {freightInit.User.LastName},<br/>", 100);
                        message.Append($"We just received and confirmed your payment. <br/>Many thanks for your patronage.<br/><br/>");
                        message.Append("Warm wishes.");

                        var userEmail = freightInit.User.Email;
                        EmailHelper emailHelper = new EmailHelper(_email);
                        bool result = await emailHelper.Send(userEmail, subject, message.ToString());
                        if (!result)
                        {
                            _log.LogInformation("Could not send email");
                        }
                    }
                    await _ctx.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _log.LogInformation($"Cannot update Freight: {ex.Message}");

                }
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            if (Exists(id))
            {
                var interest = await _ctx.Freights.FindAsync(id);
                try
                {
                    _ctx.Freights.Remove(interest);
                    await _ctx.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _log.LogInformation($"Cannot delete Freight: {ex.Message}");

                }
            }
            return false;
        }

        public async Task<string> TrackingInfo(RequestFreightViewModel rqv)
        {
            if(Exists(rqv.FreightId))
            {
                var model = await _ctx.Freights.FindAsync(rqv.FreightId);
                return model.Status.StatusReadable();
            }
            return null;
        }
        
        private bool Exists(int id) => _ctx.Freights.Any(x => x.FreightId == id);
    }
}
