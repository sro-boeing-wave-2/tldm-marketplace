using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using MarketPlaceBackend.Models;


namespace MarketPlaceBackend.Contracts
{
    public interface IApplicationService
    {
        Task<IEnumerable<Application>> getAllApplications();
        Task<IEnumerable<Object>> getAllApplicationsWithoutId();
        Task<Application> getApplicationById(string id);
        Task addApplication(Application app);
        Task<Application> deleteApplicationById(string id);
        Task<string> updateApplication(Application app);
    }
}
