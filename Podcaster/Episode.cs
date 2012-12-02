using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podcaster
{
    class Episode
    {
        public String Title { get; set; }
        public String Authors { get; set; }
        public DateTime PubData { get; set; }
        public Uri Url { get; set; }
        public long Length { get; set; }
        public String Type { get; set; }
        public Uri ImageUrl { get; set; }
        public String Description { get; set; }

        public Episode()
        { 
        }

        // Return episode description
        public override String ToString()
        {
            return String.Format("{0}\n{1}\n{2}\n\n{3}", 
                this.Title,this.Authors,this.PubData.ToString(),
                this.Description.Length > 100 ? this.Description.Substring(0,100): this.Description);
        }
    }
}
