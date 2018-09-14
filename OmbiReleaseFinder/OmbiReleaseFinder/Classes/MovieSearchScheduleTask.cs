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
    public class MovieSearchScheduleTask : ScheduledProcessor
    {
        private IOptions<AppSettingOmbi> _ombiSettings;
        private MovieDatabaseContext _db;


        public MovieSearchScheduleTask(IServiceScopeFactory serviceScopeFactory, IOptions<AppSettingOmbi> ombiSettings) : base(serviceScopeFactory)
        {
            _ombiSettings = ombiSettings;
            _db = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<MovieDatabaseContext>();
        }

        protected override string Schedule => "*/1 * * * *";

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            //Todo: wieder anschalten
            OmbiMovieListeProvider ombiMovieListeProvider = new OmbiMovieListeProvider(_ombiSettings, _db);
            ombiMovieListeProvider.GetorUpgradeMovie();

            //Console.WriteLine("Processing starts here");
            return Task.CompletedTask;
        }
    }
}
