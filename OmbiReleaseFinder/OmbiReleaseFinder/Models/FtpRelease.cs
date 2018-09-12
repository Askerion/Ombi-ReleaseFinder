using System;
using System.Collections.Generic;

namespace OmbiReleaseFinder.Models
{
    public partial class FtpRelease
    {
        public Guid Id { get; set; }
        public string FtpReleasename { get; set; }
        public string FtpReleaseGroup { get; set; }
        public string FtpFolder { get; set; }
        public Guid? MovieId { get; set; }

        public CustomMovie Movie { get; set; }
    }
}
