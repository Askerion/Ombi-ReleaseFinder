using System;
using System.Collections.Generic;

namespace OmbiReleaseFinder.Models
{
    public partial class Releasenames
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MovieId { get; set; }

        public CustomMovie Movie { get; set; }
    }
}
