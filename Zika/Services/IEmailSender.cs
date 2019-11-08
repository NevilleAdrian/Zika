using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zika.Services
{
	public interface IEmailSender
	{
		Task SendEmailAsync(string email, string subject, string message);
		Task SendEmailToAllAsync(string[] emails, string subject, string message);
	}
}
