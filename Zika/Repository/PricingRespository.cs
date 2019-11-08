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
    public class PricingRespository
    {
        private readonly ApplicationDbContext _ctx;
        private readonly ILogger<PricingRespository> _log;
        public PricingRespository(ApplicationDbContext context,
            ILogger<PricingRespository> logger)
        {
            _ctx = context;
            _log = logger;

        }

        public async Task<bool> Add(Pricing pricing)
        {
            try
            {
                _ctx.Pricings.Add(pricing);
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.LogInformation($"Cannot create Pricing: {ex.Message}");
                return false;
            }
        }

        public async Task<Pricing> Item(int id)
        {
            if (Exists(id))
            {
                return await _ctx.Pricings.Where(x => x.PricingId == id).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<List<Pricing>> Items() => await _ctx.Pricings.ToListAsync() ?? new List<Pricing>();

        public async Task<bool> Update(Pricing pricing)
        {
            if (Exists(pricing.PricingId))
            {
                try
                {
                    _ctx.Pricings.Update(pricing);
                    await _ctx.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _log.LogInformation($"Cannot update Pricing: {ex.Message}");

                }
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            if (Exists(id))
            {
                var interest = await _ctx.Pricings.FindAsync(id);
                try
                {
                    _ctx.Pricings.Remove(interest);
                    await _ctx.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _log.LogInformation($"Cannot delete Pricing: {ex.Message}");

                }
            }
            return false;
        }

        private bool Exists(int id) => _ctx.Pricings.Any(x => x.PricingId == id);
    }
}
