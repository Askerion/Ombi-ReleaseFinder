using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using Newtonsoft.Json;
using OmbiReleaseFinder.Classes;
using OmbiReleaseFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using OmbiReleaseFinder.Controllers;

namespace OmbiReleaseFinder.Providers
{
    internal class OmbiMovieListeProvider
    {
        private MovieDatabaseContext _db;
        private IOptions<AppSettingOmbi> _ombiSettings { get; set; }

        public OmbiMovieListeProvider(IOptions<AppSettingOmbi> ombiSettings, MovieDatabaseContext db)
        {
            _ombiSettings = ombiSettings;
            _db = db;
        }

        public void GetorUpgradeMovie()
        {

            WebClient request = new WebClient();
            request.Headers.Add("Content-Type", "appication/json");
            request.Headers.Add("Apikey", _ombiSettings.Value.OmbiApikey);
            var json = request.DownloadString(_ombiSettings.Value.OmbiUrl);

            
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            List<OmbiMovieListModel> objData = JsonConvert.DeserializeObject<List<OmbiMovieListModel>>(json, settings);

            
            SaveData(objData);

            //Coming soon
            //var output = "dir".Bash();        

        }

        // Generiert mögliche Releasenamen zu einen Film
        public List<Releasenames> GetText(string moviename)
        {
            List<Releasenames> releasename = new List<Releasenames>();

            moviename = moviename.Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue");
            moviename = moviename.Replace("-", "");
            moviename = moviename.Replace(" ", ".");
            moviename = moviename.Replace("..", ".");
            moviename = moviename.Replace(":", "");



            releasename.Add(new Releasenames { Name = moviename});

            //Movie Sprache
            var movielanguage = moviename + ".*German";
            releasename.Add(new Releasenames { Name = movielanguage });

            //Movie HD hinzufügen
            var moviequality = moviename + ".*1080p";
            releasename.Add(new Releasenames { Name = moviequality });

            var movieadvanced = moviename + ".*German" + ".*1080p";
            releasename.Add(new Releasenames { Name = movieadvanced });



            return releasename;

        }
       
        //Speichert die Filme in die Datenbank
        public async void SaveData(List<OmbiMovieListModel> ombiMovieListModels)
        {
            // prüfen ob der Film bereits in der DB ist..... Speichern des Movies
            foreach (OmbiMovieListModel m in ombiMovieListModels.Where(n => n.available == false))
            {

                CustomMovie _customMovie = _db.CustomMovie.Where(a => a.MovieDbId == m.theMovieDbId).FirstOrDefault();
                if (_customMovie == null)
                {
                    ApiQueryResponse<Movie> findMovie = await searchMoviefromTheMovieDB(m.theMovieDbId);
                    _db.Add(new CustomMovie { OriginalTitle = findMovie.Item.OriginalTitle, PosterPath = "http://image.tmdb.org/t/p/w185/" + findMovie.Item.PosterPath, Overview= findMovie.Item.Overview, Rating = findMovie.Item.Popularity, MovieDbId = findMovie.Item.Id, Releasenames = GetText(findMovie.Item.Title), Title = findMovie.Item.Title });
                    _db.SaveChanges();
                    
                }
            }
            // Filme die bereits vorhanden sind aus DB löschen
            foreach (OmbiMovieListModel m in ombiMovieListModels.Where(n => n.available == true))
            {                
                CustomMovie _customMovie = _db.CustomMovie.Where(a => a.MovieDbId == m.theMovieDbId).FirstOrDefault();
                if (_customMovie != null)
                {
                    //Nach den Releasenamensuchen und wenn vorhanden auch löschen
                    Releasenames[] _releasenames = _db.Releasenames.Where(r => r.MovieId == _customMovie.Id).ToArray();
                    if(_releasenames != null)
                        _db.Releasenames.RemoveRange(_releasenames);

                    _db.CustomMovie.Remove(_customMovie);
                    _db.SaveChanges();
                }
            }

        }

        //Liefert einen MovieObjekt von TheMovieDB
        public async Task<ApiQueryResponse<Movie>> searchMoviefromTheMovieDB(int MovieId)
        {
            MovieDbFactory.RegisterSettings("475a861fb3967339aa8dd5680cb64b5b", "http://api.themoviedb.org/3/");
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

            ApiQueryResponse<Movie> apiQueryResponse = await movieApi.FindByIdAsync(MovieId, "de");

            return apiQueryResponse;

        }     
        

    }


}

