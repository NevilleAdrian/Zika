using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zika.Data;
using Zika.Models;
using Zika.Services;
using Zika.ViewModels;

namespace Zika.Repository
{
    public class TeamRepository
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IImageService _img;
        private readonly ILogger<TeamRepository> _log;
        public TeamRepository(ApplicationDbContext context,
            ILogger<TeamRepository> logger,
            IImageService img)
        {
            _ctx = context;
            _log = logger;
            _img = img;
        }

        public async Task<bool> Add(TeamViewModel team)
        {
            string path = null;
            if(team.File != null)
            {
                try
                {
                    path = _img.CreateImage(team.File);
                }
                catch(Exception ex)
                {
                    _log.LogInformation($"Cannot add image: {ex.Message}");
                }
                
            }
            try
            {
                team.ImageUrl = path;
                Team teamToAdd = new Team {
                    Description = team.Description,
                    Facebook = team.Facebook,
                    ImageUrl = team.ImageUrl,
                    Instagram = team.Instagram,
                    Name = team.Name,
                    Position = team.Position,
                    Skype = team.Skype,
                    Twitter = team.Twitter
                };
                _ctx.Teams.Add(teamToAdd);
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.LogInformation($"Cannot create Team: {ex.Message}");
                return false;
            }
        }

        public async Task<Team> Item(int id)
        {
            if (Exists(id))
            {
                return await _ctx.Teams.Where(x => x.TeamId == id).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<List<Team>> Items() => await _ctx.Teams.ToListAsync() ?? new List<Team>();

        public async Task<bool> Update(TeamViewModel team)
        {
            if (Exists(team.TeamId))
            {
                string path = null;
                if (team.File != null)
                {
                    try
                    {
                        path = _img.EditImage(team.File, team.ImageUrl);
                    }
                    catch (Exception ex)
                    {
                        _log.LogInformation($"Cannot add image: {ex.Message}");
                    }

                }
                try
                {
                    team.ImageUrl = path ?? team.ImageUrl;
                    Team teamToAdd = new Team
                    {
                        Description = team.Description,
                        Facebook = team.Facebook,
                        ImageUrl = team.ImageUrl,
                        Instagram = team.Instagram,
                        Name = team.Name,
                        Position = team.Position,
                        Skype = team.Skype,
                        Twitter = team.Twitter,
                        TeamId = team.TeamId
                    };
                    _ctx.Teams.Update(teamToAdd);
                    await _ctx.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _log.LogInformation($"Cannot update Team: {ex.Message}");

                }
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            if (Exists(id))
            {
                var interest = await _ctx.Teams.FindAsync(id);
                try
                {
                    _ctx.Teams.Remove(interest);
                    await _ctx.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _log.LogInformation($"Cannot delete Team: {ex.Message}");

                }
            }
            return false;
        }

        private bool Exists(int id) => _ctx.Teams.Any(x => x.TeamId == id);
    }
}
