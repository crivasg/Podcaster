using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Podcaster
{
    class Episode
    {
        public Image Picture { get; set ; }
        public DateTime Date { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Author { get; set; }
        public String Link { get; set; }
        public String Size { get; set; }

        public Episode()
        {
        }

        // Return episode description
        public override String ToString()
        {
            return Description;
        }


        // Returuns a summary of the episodes
        public String EpisodeData()
        {
            return String.Empty ;
        }

    }
}