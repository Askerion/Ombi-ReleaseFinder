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
using OmbiReleaseFinder.Classes;

namespace OmbiReleaseFinder.BackgroundServiceClasses
{
    public class MovieSearchScheduleTask : ScheduledProcessor
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(MovieSearchScheduleTask));
        private IOptions<AppSettingOmbi> _ombiSettings;
        private MovieDatabaseContext _db;


        public MovieSearchScheduleTask(IServiceScopeFactory serviceScopeFactory, IOptions<AppSettingOmbi> ombiSettings) : base(serviceScopeFactory)
        {
            _ombiSettings = ombiSettings;
            _db = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<MovieDatabaseContext>();
        }

        protected override string Schedule => "*/3 * * * *";

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            //Suche nach neuen Movies
            log.Info("Task: Suche nach neuen Movies");
            OmbiMovieListeProvider ombiMovieListeProvider = new OmbiMovieListeProvider(_ombiSettings, _db);
            ombiMovieListeProvider.GetorUpgradeMovie();
            log.Info("Task: Fertig");

            //Console.WriteLine("Processing starts here");
            return Task.CompletedTask;
        }
    }
}
