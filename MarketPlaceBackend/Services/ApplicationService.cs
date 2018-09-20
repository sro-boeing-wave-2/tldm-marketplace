using MarketPlaceBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using MarketPlaceBackend.Contracts;

namespace MarketPlaceBackend.Services
{
    public class ApplicationService: IApplicationService
    {
        private readonly MarketPlaceBackendContext _context;

        public ApplicationService(MarketPlaceBackendContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Application>> getAllApplications()
        {
            var allApplications = await _context.Application.Select(app => app).ToListAsync();
            return allApplications;
        }

        public async Task<IEnumerable<Object>> getAllApplicationsWithoutId()
        {
            var applications = await (from app in _context.Application
                                      select new
                                      {
                                          firstName = app.Name,
                                          lastName = "Bot",
                                          emailId = app.EmailId
                                      }).ToListAsync();
            return applications;
        }

        public async Task<Application> getApplicationById(string id)
        {
            var application = await _context.Application.SingleOrDefaultAsync(app => app.Id == id);
            return application;
        }

        public async Task addApplication(Application app)
        {
            _context.Application.Add(app);
            await _context.SaveChangesAsync();
        }

        public async Task<Application> deleteApplicationById(string id)
        {
            var application = await _context.Application.SingleOrDefaultAsync(app => app.Id == id);
            _context.Application.Remove(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<string> updateApplication(Application app)
        {
            //_context.Entry(app).State = EntityState.Modified;
            //_context.Application.Update(app);
            _context.Entry(app).Property(x => x.Name).IsModified = isSet(app.Name);
            _context.Entry(app).Property(x => x.Developer).IsModified = isSet(app.Developer);
            _context.Entry(app).Property(x => x.Info).IsModified = isSet(app.Info);
            _context.Entry(app).Property(x => x.AppUrl).IsModified = isSet(app.AppUrl);
            _context.Entry(app).Property(x => x.LogoUrl).IsModified = isSet(app.LogoUrl);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(app.Id))
                {
                    return "Not Found";
                }
                else
                {
                    throw;
                }
            }
            return "No Content";
        }

        private bool isSet(Object x)
        {
            if(x == null)
            {
                return false;
            } else
            {
                return true;
            }
        }

        private bool ApplicationExists(string id)
        {
            return _context.Application.Any(e => e.Id == id);
        }
    }
}
