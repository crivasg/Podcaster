using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;

namespace Podcaster
{
    class PodcastFeed
    {
        // Test how to use the syndication feed
        // http://msdn.microsoft.com/en-us/library/system.servicemodel.syndication.syndicationfeed(v=vs.90).aspx
        // http://feeds.5by5.tv/master
        public String URL { private get; set; }
        public List<Episode> Episodes { get; private set; }

        public PodcastFeed()
        { 
        }

        public void Parse()
        {
            List<String> titleList = new List<String>();

            using (XmlReader feedReader = XmlReader.Create(this.URL))
            {
                SyndicationFeed feedContent = SyndicationFeed.Load(feedReader);
                if (null == feedContent)
                {
                    return;
                }

                foreach (SyndicationItem item in feedContent.Items)
                {
                    titleList.Add(item.Title.Text);

                    foreach (SyndicationLink links in item.Links.Where(links => links.RelationshipType == "enclosure"))
                    {


                    }

                }

            }

            MessageBox.Show(String.Join(Environment.NewLine, titleList.ToArray()));        
        }

    }
}
