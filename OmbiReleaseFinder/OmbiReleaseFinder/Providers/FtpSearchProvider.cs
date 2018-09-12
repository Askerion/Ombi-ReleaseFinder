using Microsoft.Extensions.Options;
using OmbiReleaseFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmbiReleaseFinder.Providers
{
    internal class FtpSearchProvider
    {
        private MovieDatabaseContext _db = new MovieDatabaseContext();


        public void GetFtpReleases(IOptions<AppSettingFtp> appSettingFtp)
        {
            //Konfig FTP Folder parsen
            string[] sArray = appSettingFtp.Value.Folders.Split(';');


            Rebex.Licensing.Key = "==AOptXWwcjeC/mE+S0K60UcnUhOW7heBDBmgjFhBdpuvU==";
            using (var client = new Rebex.Net.Ftp())
            {
                client.Settings.SslAcceptAllCertificates = true;
                // connect and log in
                client.Connect(appSettingFtp.Value.Host, appSettingFtp.Value.Port);
                //if ssl true
                if (appSettingFtp.Value.Ssl == true)
                    client.Secure();
                //FTP Login
                client.Login(appSettingFtp.Value.Username, appSettingFtp.Value.Passwort);
                //FTP Root Login
                client.ChangeDirectory("/");


                //FTP Ordner durchsuchen
                foreach (string s in sArray)
                {
                    string[] dataItems = client.GetNameList(s);                  
                    
                    foreach (string a in dataItems)
                    {
                        string relname = a.Substring(a.LastIndexOf("/") + 1);
                        string relgroup = a.Substring(a.LastIndexOf("-") + 1);

                        FtpRelease _ftpRelease = _db.FtpRelease.Where(r => r.FtpReleasename == relname).FirstOrDefault();

                        if (_ftpRelease == null)
                        {
                            _db.Add(new FtpRelease { FtpReleasename = relname, FtpReleaseGroup = relgroup, FtpFolder = a });
                            _db.SaveChanges();
                        }
                    }
                }

            }
        }
    }
}
