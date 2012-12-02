using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

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
            Episodes = new List<Episode>();

            using (XmlReader feedReader = XmlReader.Create(this.URL))
            {
                SyndicationFeed feedContent = SyndicationFeed.Load(feedReader);
                //XmlQualifiedName n = new XmlQualifiedName("itunes", "http://www.w3.org/2000/xmlns/");
                String itunesNs = "http://www.itunes.com/dtds/podcast-1.0.dtd";
                //feedContent.AttributeExtensions.Add(n, itunesNs);
                if (feedContent == null)
                {
                    return;
                }

                foreach (SyndicationItem item in feedContent.Items)
                {
                    SyndicationPerson author = item.Authors[0];

                    // Get values of syndication extension elements for a given namespace
                    string extensionNamespaceUri = "http://www.itunes.com/dtds/podcast-1.0.dtd";
                    SyndicationElementExtension extension = item.ElementExtensions.Where<SyndicationElementExtension>(x => x.OuterNamespace == extensionNamespaceUri).FirstOrDefault();
                    XPathNavigator dataNavigator = new XPathDocument(extension.GetReader()).CreateNavigator();

                    XmlNamespaceManager resolver = new XmlNamespaceManager(dataNavigator.NameTable);
                    resolver.AddNamespace("itunes", extensionNamespaceUri);

                    XPathNavigator authorNavigator = dataNavigator.SelectSingleNode("itunes:author", resolver);
                    XPathNavigator subtitleNavigator = dataNavigator.SelectSingleNode("itunes:subtitle", resolver);
                    XPathNavigator summaryNavigator = dataNavigator.SelectSingleNode("itunes:summary", resolver);
                    XPathNavigator durationNavigator = dataNavigator.SelectSingleNode("itunes:duration", resolver);
                    XPathNavigator imageNavigator = dataNavigator.SelectSingleNode("itunes:image", resolver);
                    String imageOuterXML = imageNavigator.OuterXml;

                    String imageUrl = String.Empty;
                    if (imageNavigator.MoveToFirstAttribute())
                    {
                        if (imageNavigator.Name == "href")
                        {
                            imageUrl = imageNavigator.Value.ToString();
                        }
                        while (imageNavigator.MoveToNextAttribute())
                        {
                            if (imageNavigator.Name == "href")
                            {
                                imageUrl = imageNavigator.Value.ToString();
                            }
                        }
                        
                        // go back from the attributes to the parent element
                        imageNavigator.MoveToParent();
                    }
                 

                    String authorx = authorNavigator != null ? authorNavigator.Value : String.Empty;
                    String subtitle = subtitleNavigator != null ? subtitleNavigator.Value : String.Empty;
                    String summary = summaryNavigator != null ? summaryNavigator.Value : String.Empty;
                    String duration = durationNavigator != null ? durationNavigator.Value : String.Empty;

                    Uri url = null;
                    long length = 0;
                    String mediaType = String.Empty;

                    foreach (SyndicationLink links in item.Links.Where<SyndicationLink>(links => links.RelationshipType == "enclosure"))
                    {
                        url = links.Uri;
                        length = links.Length;
                        mediaType = links.MediaType;
                    }

                    Episode podcastEpisode = new Episode()
                    {
                        Title = item.Title.Text,
                        Authors = author.Email,
                        PubData = item.PublishDate.LocalDateTime,
                        FileUrl = url,
                        Length = length,
                        Type = mediaType,
                        ImageUrl = new Uri(imageUrl),
                        Description = item.Summary.Text
                    };

                    this.Episodes.Add(podcastEpisode);
                    
                }

       
            }
             
        }

        /// <summary>
        /// Stores the feeds in a SQLite database
        /// </summary>
        public void Feed2SQLite()
        { 
        
        }

        /// <summary>
        /// Downloads the episode covers to %APPDATA%\Podcaster\Covers
        /// </summary>
        public void DownloadEpisodeCover()
        { 
        }
    }
}
