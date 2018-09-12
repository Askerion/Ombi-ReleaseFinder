using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OmbiReleaseFinder.Controllers;
using OmbiReleaseFinder.Models;
using OmbiReleaseFinder.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmbiReleaseFinder.Classes
{
    public class FtpScheduleTask : ScheduledProcessor
    {     

        public FtpScheduleTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        protected override string Schedule => "*/05 * * * *";

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {           
            var startFtpSearch = new FtpSearchProvider();
            startFtpSearch.GetFtpReleases(HomeController._ftpSettings);

            Console.WriteLine("Processing starts here");
            return Task.CompletedTask;
        }
    }
}
