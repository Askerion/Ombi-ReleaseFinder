using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OmbiReleaseFinder.Classes;
using OmbiReleaseFinder.Controllers;
using OmbiReleaseFinder.Models;
using OmbiReleaseFinder.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmbiReleaseFinder.BackgroundServiceClasses
{
    public class FtpScheduleTask : ScheduledProcessor
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(FtpScheduleTask));
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
            //Suche nach FTP Releasen
            log.Info("Task: Suche nach FTP Releasen");
            var startFtpSearch = new FtpSearchProvider(FtpConfiguration, _db);
            startFtpSearch.GetFtpReleases();

            log.Info("Task: Mappe Release zu Movie");
            //Mappe Release zu Movies
            var mapMovies = new Movie2FtpProvider(_db);
            mapMovies.SearchMoviewithRegex();



            log.Info("Task: Fertig");
            return Task.CompletedTask;
        }
    }
}
