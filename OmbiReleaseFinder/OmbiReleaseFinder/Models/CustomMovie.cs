using System;
using System.Collections.Generic;

namespace OmbiReleaseFinder.Models
{
    public partial class CustomMovie
    {
        public CustomMovie()
        {
            FtpRelease = new HashSet<FtpRelease>();
            Releasenames = new HashSet<Releasenames>();
        }

        public Guid Id { get; set; }
        public int MovieDbId { get; set; }
        public string OriginalTitle { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string PosterPath { get; set; }

        public ICollection<FtpRelease> FtpRelease { get; set; }
        public ICollection<Releasenames> Releasenames { get; set; }

        public override string ToString()
        {
            return $"{Title} ({OriginalTitle})";
        }
    }
}
