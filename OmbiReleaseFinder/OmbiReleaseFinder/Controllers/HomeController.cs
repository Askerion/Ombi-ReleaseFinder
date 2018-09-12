﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OmbiReleaseFinder.Models;
using OmbiReleaseFinder.Providers;

namespace OmbiReleaseFinder.Controllers
{
    public class HomeController : Controller
    {
        public static IOptions<AppSettingFtp> _ftpSettings { get; set; }
        public static IOptions<AppSettingOmbi> _ombiSettings { get; set; }


        MovieDatabaseContext _releases = new MovieDatabaseContext();
        
        public HomeController(IOptions<AppSettingFtp> ftpSettings, IOptions<AppSettingOmbi> ombiSettings)
        {
            _ftpSettings = ftpSettings;
            _ombiSettings = ombiSettings;
        }
        public IActionResult Index()
        {
            //OmbiMovieListeProvider ombiMovieListeProvider = new OmbiMovieListeProvider();
            //var getmovie = await ombiMovieListeProvider.GetMovieListAsync();
            //return View(getmovie);         
            Movie2FtpProvider movie2FtpProvider = new Movie2FtpProvider();
            movie2FtpProvider.SearchMoviewithRegex();

      
            var customMovies = new MovieDatabaseContext();

            var test = customMovies.CustomMovie.Include(x => x.Releasenames).Include(y => y.FtpRelease);
            return View(test);


        }

        [HttpGet]
        public IActionResult Releases(string searchString)
        {
            //var _releases = new MovieDatabaseContext();
            var getrel = _releases.FtpRelease.ToList();
                        
            if (!String.IsNullOrEmpty(searchString))
            {
                FtpRelease[] _searchrel = _releases.FtpRelease.Where(s => s.FtpReleasename.Contains(searchString)).ToArray();
                return View(_searchrel);                
            }        

            return View(getrel);
        }



        
        public IActionResult MovieModal(string moviedbid)
        {
            var customMovies = new MovieDatabaseContext();
            var moviewithrel = customMovies.CustomMovie.Include(x => x.Releasenames).Include(y => y.FtpRelease);


            if (!String.IsNullOrEmpty(moviedbid))
            {

                var aaa = moviewithrel.Where(r => r.MovieDbId == Convert.ToInt32(moviedbid)).FirstOrDefault();
                return View(aaa);
            }

            return View();
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
