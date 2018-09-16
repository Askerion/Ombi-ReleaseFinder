using Microsoft.Extensions.Options;
using OmbiReleaseFinder.Models;
using System.Linq;
using OmbiReleaseFinder.Classes;

namespace OmbiReleaseFinder.Providers
{
    internal class FtpSearchProvider
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(FtpSearchProvider));
        private MovieDatabaseContext _db;
        private IOptions<AppSettingFtp> FtpConfiguration { get; set; }

        public FtpSearchProvider(IOptions<AppSettingFtp> ftpSettings, MovieDatabaseContext db)
        {
            FtpConfiguration = ftpSettings;
            _db = db;
        }


        public void GetFtpReleases()
        {
            //Konfig FTP Folder parsen
            //var ftpConfig = FtpConfiguration.Value;
            string[] sArray = FtpConfiguration.Value.Folders.Split(';');


            log.Info("Task: Lade FTP Konfig");
            Rebex.Licensing.Key = "==AOptXWwcjeC/mE+S0K60UcnUhOW7heBDBmgjFhBdpuvU==";
            try
            {
                using (var client = new Rebex.Net.Ftp())
                {
                    client.Settings.SslAcceptAllCertificates = true;
                    // connect and log in
                    client.Connect(FtpConfiguration.Value.Host, FtpConfiguration.Value.Port);
                    //if ssl true
                    if (FtpConfiguration.Value.Ssl == true)
                        client.Secure();
                    //FTP Login
                    log.Info("Task: FTP Login");
                    client.Login(FtpConfiguration.Value.Username, FtpConfiguration.Value.Passwort);
                    //FTP Root Login
                    client.ChangeDirectory("/");
                                                         
                    //FTP Ordner durchsuchen
                    log.Info("Task: FTP Ordner durchsuchen");
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
            catch (System.Exception ex)
            {

                log.Info("Task: Fehler" + ex);
            }
            
        }
    }
}
