using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

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
                //XmlQualifiedName n = new XmlQualifiedName("itunes", "http://www.w3.org/2000/xmlns/");
                //String itunesNs = "http://www.itunes.com/dtds/podcast-1.0.dtd";
                //feedContent.AttributeExtensions.Add(n, itunesNs);
                if (feedContent == null)
                {
                    return;
                }

                foreach (SyndicationItem item in feedContent.Items)
                {
                    SyndicationPerson author = item.Authors[0];
                    
                    titleList.Add(item.Title.Text + "\n" + author.Email + "\n" + item.PublishDate.LocalDateTime.ToString() + "\n" + item.Summary.Text);

                    //foreach (SyndicationLink links in item.Links.Where(links => links.RelationshipType == "enclosure"))
                    foreach (SyndicationLink links in item.Links)
                    {

                        
                    }

                }

            }
            File.WriteAllLines(@"C:\Documents and Settings\crivas\Desktop\5by5.txt",titleList.ToArray());     
        }

    }
}
