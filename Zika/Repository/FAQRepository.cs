using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zika.Data;
using Zika.Models;

namespace Zika.Repository
{
    public class FAQRepository
    {
        private readonly ApplicationDbContext _ctx;
        private readonly ILogger<FAQRepository> _log;
        public FAQRepository(ApplicationDbContext context,
            ILogger<FAQRepository> logger)
        {
            _ctx = context;
            _log = logger;

        }

        public async Task<bool> Add(FAQ faq)
        {
            try
            {
                _ctx.FAQs.Add(faq);
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.LogInformation($"Cannot create faq: {ex.Message}");
                return false;
            }
        }

        public async Task<FAQ> Item(int id)
        {
            if (Exists(id))
            {
                return await _ctx.FAQs.Where(x => x.FAQId == id).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<List<FAQ>> Items() => await _ctx.FAQs.ToListAsync() ?? new List<FAQ>();

        public async Task<bool> Update(FAQ faq)
        {
            if (Exists(faq.FAQId))
            {
                try
                {
                    _ctx.FAQs.Update(faq);
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

        public async Task<bool> Delete(int id)
        {
            if (Exists(id))
            {
                var interest = await _ctx.FAQs.FindAsync(id);
                try
                {
                    _ctx.FAQs.Remove(interest);
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

        private bool Exists(int id) => _ctx.FAQs.Any(x => x.FAQId == id);
    }
}
