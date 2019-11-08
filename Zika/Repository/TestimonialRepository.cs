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
    public class TestimonialRepository
    {
        private readonly ApplicationDbContext _ctx;
        private readonly ILogger<TestimonialRepository> _log;
        public TestimonialRepository(ApplicationDbContext context,
            ILogger<TestimonialRepository> logger)
        {
            _ctx = context;
            _log = logger;

        }

        public async Task<bool> Add(Testimonial testimonial)
        {
            try
            {
                _ctx.Testimonials.Add(testimonial);
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.LogInformation($"Cannot create Testimonial: {ex.Message}");
                return false;
            }
        }

        public async Task<Testimonial> Item(int id)
        {
            if (Exists(id))
            {
                return await _ctx.Testimonials.Where(x => x.TestimonialId == id).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<List<Testimonial>> Items() => await _ctx.Testimonials.ToListAsync() ?? new List<Testimonial>();

        public async Task<bool> Update(Testimonial testimonial)
        {
            if (Exists(testimonial.TestimonialId))
            {
                try
                {
                    _ctx.Testimonials.Update(testimonial);
                    await _ctx.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _log.LogInformation($"Cannot update Testimonial: {ex.Message}");

                }
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            if (Exists(id))
            {
                var interest = await _ctx.Testimonials.FindAsync(id);
                try
                {
                    _ctx.Testimonials.Remove(interest);
                    await _ctx.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _log.LogInformation($"Cannot delete Testimonial: {ex.Message}");

                }
            }
            return false;
        }

        private bool Exists(int id) => _ctx.Testimonials.Any(x => x.TestimonialId == id);
    }
}
