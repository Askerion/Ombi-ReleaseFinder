using Microsoft.EntityFrameworkCore;
using OmbiReleaseFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OmbiReleaseFinder.Providers
{
    internal class Movie2FtpProvider
    {
        private MovieDatabaseContext _db;

        public Movie2FtpProvider(MovieDatabaseContext db)
        {            
            _db = db;
        }


        public void SearchMoviewithRegex()
        {           

            CustomMovie[] ombiMovie = _db.CustomMovie.Include(x => x.Releasenames).ToArray();

            foreach (CustomMovie item in ombiMovie)
            {

                //** Sebi wars

                var regExList = item.Releasenames.Select(rn => rn.Name).ToArray();

                var releaseMatches = _db.FtpRelease
                    .Where(r => 
                        r.MovieId == null
                        && RexExTest(r.FtpReleasename, regExList))
                        .ToArray();


                foreach (var releaseMatch in releaseMatches)
                    releaseMatch.MovieId = item.Id;

                _db.SaveChanges();
            }

        }


        private static bool RexExTest(string releaseName, string[] regEx)
        {
            return regEx.Any(reg => new Regex(reg).IsMatch(releaseName));
        }
    }
}
