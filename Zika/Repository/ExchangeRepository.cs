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
    public class ExchangeRepository
    {
        private readonly ApplicationDbContext _ctx;
        private readonly ILogger<ExchangeRepository> _log;
        public ExchangeRepository(ApplicationDbContext context,
            ILogger<ExchangeRepository> logger)
        {
            _ctx = context;
            _log = logger;
            
        }

        public async Task<bool> Add(Exchange exchange)
        {
            try
            {
                _ctx.Exchanges.Add(exchange);
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                _log.LogInformation($"Cannot create exchange: {ex.Message}");
                return false;
            }
        }

        public async Task<Exchange> Item(int id)
        {
            if(Exists(id))
            {
                return await _ctx.Exchanges.Where(x => x.ExchangeId == id).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<List<Exchange>> Items() => await _ctx.Exchanges.ToListAsync() ?? new List<Exchange>();

        public async Task<bool> Update(Exchange exchange)
        {
            if (Exists(exchange.ExchangeId))
            {
                try
                {
                    _ctx.Exchanges.Update(exchange);
                    await _ctx.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _log.LogInformation($"Cannot update exchange: {ex.Message}");
                    
                }
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            if (Exists(id))
            {
                var interest = await _ctx.Exchanges.FindAsync(id);
                try
                {
                    _ctx.Exchanges.Remove(interest);
                    await _ctx.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _log.LogInformation($"Cannot delete exchange: {ex.Message}");

                }
            }
            return false;
        }

        private bool Exists(int id) => _ctx.Exchanges.Any(x => x.ExchangeId == id);
    }
}
