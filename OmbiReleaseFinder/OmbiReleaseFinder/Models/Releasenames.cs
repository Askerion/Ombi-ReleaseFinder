using System;
using System.Collections.Generic;

namespace OmbiReleaseFinder.Models
{
    public partial class Releasenames
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid MovieId { get; set; }

        public CustomMovie Movie { get; set; }
    }
}
