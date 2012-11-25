using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podcaster
{
    class PodcastFeed
    {
        public String URL { private get; set; }
        public List<Episode> Episodes { get; private set; }

        public PodcastFeed()
        { 
        }

    }
}
