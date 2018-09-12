using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmbiReleaseFinder.Models
{
    public class OmbiMovieListModel
    {

        public int theMovieDbId { get; set; }
        public int issueId { get; set; }
        public Issue[] issues { get; set; }
        public bool subscribed { get; set; }
        public bool showSubscribe { get; set; }
        public int rootPathOverride { get; set; }
        public int qualityOverride { get; set; }
        public string imdbId { get; set; }
        public string overview { get; set; }
        public string posterPath { get; set; }
        public DateTime releaseDate { get; set; }
        public DateTime digitalReleaseDate { get; set; }
        public string status { get; set; }
        public string background { get; set; }
        public bool released { get; set; }
        public bool digitalRelease { get; set; }
        public string title { get; set; }
        public bool approved { get; set; }
        public DateTime requestedDate { get; set; }
        public bool available { get; set; }
        public string requestedUserId { get; set; }
        public bool denied { get; set; }
        public string deniedReason { get; set; }
        public string requestType { get; set; }
        public Requesteduser requestedUser { get; set; }
        public bool canApprove { get; set; }
        public int id { get; set; }
    }

    public class Requesteduser
        {
            public string alias { get; set; }
            public string userType { get; set; }
            public string providerUserId { get; set; }
            public DateTime lastLoggedIn { get; set; }
            public string embyConnectUserId { get; set; }
            public int movieRequestLimit { get; set; }
            public int episodeRequestLimit { get; set; }
            public string userAccessToken { get; set; }
            public Notificationuserid[] notificationUserIds { get; set; }
            public bool isEmbyConnect { get; set; }
            public string userAlias { get; set; }
            public bool emailLogin { get; set; }
            public bool isSystemUser { get; set; }
            public string id { get; set; }
            public string userName { get; set; }
            public string normalizedUserName { get; set; }
            public string email { get; set; }
            public string normalizedEmail { get; set; }
            public bool emailConfirmed { get; set; }
            public string phoneNumber { get; set; }
            public bool phoneNumberConfirmed { get; set; }
            public bool twoFactorEnabled { get; set; }
            public DateTime lockoutEnd { get; set; }
            public bool lockoutEnabled { get; set; }
            public int accessFailedCount { get; set; }
    }

    public class Notificationuserid
        {
            public string playerId { get; set; }
            public string userId { get; set; }
            public DateTime addedAt { get; set; }
            public int id { get; set; }
    }

    public class Issue
    {
    public string title { get; set; }
    public string requestType { get; set; }
    public string providerId { get; set; }
    public int requestId { get; set; }
    public string subject { get; set; }
    public string description { get; set; }
    public int issueCategoryId { get; set; }
    public Issuecategory issueCategory { get; set; }
    public string status { get; set; }
    public DateTime resovledDate { get; set; }
    public string userReportedId { get; set; }
    public Userreported userReported { get; set; }
    public Comment[] comments { get; set; }
    public int id { get; set; }
    }

    public class Issuecategory
    {
            public string value { get; set; }
            public int id { get; set; }
    }

        public class Userreported
        {
            public string alias { get; set; }
            public string userType { get; set; }
            public string providerUserId { get; set; }
            public DateTime lastLoggedIn { get; set; }
            public string embyConnectUserId { get; set; }
            public int movieRequestLimit { get; set; }
            public int episodeRequestLimit { get; set; }
            public string userAccessToken { get; set; }
            public Notificationuserid1[] notificationUserIds { get; set; }
            public bool isEmbyConnect { get; set; }
            public string userAlias { get; set; }
            public bool emailLogin { get; set; }
            public bool isSystemUser { get; set; }
            public string id { get; set; }
            public string userName { get; set; }
            public string normalizedUserName { get; set; }
            public string email { get; set; }
            public string normalizedEmail { get; set; }
            public bool emailConfirmed { get; set; }
            public string phoneNumber { get; set; }
            public bool phoneNumberConfirmed { get; set; }
            public bool twoFactorEnabled { get; set; }
            public DateTime lockoutEnd { get; set; }
            public bool lockoutEnabled { get; set; }
            public int accessFailedCount { get; set; }
        }

        public class Notificationuserid1
        {
            public string playerId { get; set; }
            public string userId { get; set; }
            public DateTime addedAt { get; set; }
            public int id { get; set; }
        }

        public class Comment
        {
            public string userId { get; set; }
            public string comment { get; set; }
            public int issuesId { get; set; }
            public DateTime date { get; set; }
            public User user { get; set; }
            public int id { get; set; }
        }

        public class User
        {
            public string alias { get; set; }
            public string userType { get; set; }
            public string providerUserId { get; set; }
            public DateTime lastLoggedIn { get; set; }
            public string embyConnectUserId { get; set; }
            public int movieRequestLimit { get; set; }
            public int episodeRequestLimit { get; set; }
            public string userAccessToken { get; set; }
            public Notificationuserid2[] notificationUserIds { get; set; }
            public bool isEmbyConnect { get; set; }
            public string userAlias { get; set; }
            public bool emailLogin { get; set; }
            public bool isSystemUser { get; set; }
            public string id { get; set; }
            public string userName { get; set; }
            public string normalizedUserName { get; set; }
            public string email { get; set; }
            public string normalizedEmail { get; set; }
            public bool emailConfirmed { get; set; }
            public string phoneNumber { get; set; }
            public bool phoneNumberConfirmed { get; set; }
            public bool twoFactorEnabled { get; set; }
            public DateTime lockoutEnd { get; set; }
            public bool lockoutEnabled { get; set; }
            public int accessFailedCount { get; set; }
        }

        public class Notificationuserid2
        {
            public string playerId { get; set; }
            public string userId { get; set; }
            public DateTime addedAt { get; set; }
            public int id { get; set; }
        }

    }


