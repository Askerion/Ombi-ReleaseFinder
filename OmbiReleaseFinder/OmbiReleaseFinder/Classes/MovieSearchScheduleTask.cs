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

        public MovieSearchScheduleTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        protected override string Schedule => "*/10 * * * *";

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            OmbiMovieListeProvider ombiMovieListeProvider = new OmbiMovieListeProvider();
            ombiMovieListeProvider.GetorUpgradeMovie();

            //Console.WriteLine("Processing starts here");
            return Task.CompletedTask;
        }
    }
}
