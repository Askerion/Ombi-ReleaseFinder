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
        private IOptions<AppSettingFtp> FtpConfiguration { get; set; }
        private MovieDatabaseContext _db;

        public FtpScheduleTask(IServiceScopeFactory serviceScopeFactory, IOptions<AppSettingFtp> ftpSettings) : base(serviceScopeFactory)
        {
            FtpConfiguration = ftpSettings;
            _db = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<MovieDatabaseContext>(); 
        }

        protected override string Schedule => "*/05 * * * *";


        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            var startFtpSearch = new FtpSearchProvider(FtpConfiguration, _db);
            startFtpSearch.GetFtpReleases();

            Console.WriteLine("Processing starts here");
            return Task.CompletedTask;
        }
    }
}
