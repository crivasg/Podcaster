using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Net.NetworkInformation;

using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;

namespace Podcaster
{
    public partial class PodcasterForm : Form
    {
        // Indicate the amount of space needed for a ListBox item.
        private const int itemHeight = 150;

        public PodcasterForm()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            podcasterStatusLabel.Text = String.Empty;
            episodeList.DrawMode = DrawMode.OwnerDrawVariable;

            PingReply reply = Utilities.PingServer(@"8.8.8.8");
            if (reply.Status != IPStatus.Success)
            {
                String replyString = String.Format("\n\nAddress: {0}\n", reply.Address.ToString()) +
                    String.Format("RoundTrip time: {0}\n", reply.RoundtripTime) +
                    String.Format("Time to live: {0}\n", reply.Options.Ttl) +
                    String.Format("Don't fragment: {0}\n", reply.Options.DontFragment) +
                    String.Format("Buffer size: {0}", reply.Buffer.Length);

                MessageBox.Show(@"Failed to detect the networks. Please, chec your network settings" + replyString);
                Application.Exit();
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void episodeListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {

        }

        // Handle the MeasureItem event for an owner-drawn ListBox. 
        private void ListBox1_MeasureItem(object sender,MeasureItemEventArgs e)
        {
            e.ItemHeight = itemHeight;
        }

        private void PodcasterForm_Load(object sender, EventArgs e)
        {

            PodcastFeed feed = new PodcastFeed()
            {
                URL = @"http://feeds.5by5.tv/master"
            };
            feed.Parse();
            List<Episode> episodeData = new List<Episode>();
            episodeData = feed.Episodes;

            // Test how to create custom ListBox
            Image image = Image.FromFile(@"C:\Documents and Settings\crivas\Desktop\40.jpg");
            Image image2 = Image.FromFile(@"C:\Documents and Settings\crivas\Desktop\29.jpg");
            Image image3 = Image.FromFile(@"C:\Documents and Settings\crivas\Desktop\the_frequency.jpg");

            foreach (Episode episode in episodeData)
            {
                EpisodeInfo ep1 = new EpisodeInfo()
                {
                    Picture = image,
                    PubDate = episode.PubData,
                    Author = episode.Authors,
                    Title = episode.Title,
                    Description = episode.Description,
                    Number = 2
                };
                episodeList.Items.Add(ep1);
            }
        }

        private void episodeList_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            //??
            EpisodeInfo episodeInfo = listBox.Items[e.Index] as EpisodeInfo;

            // Draw the background.
            e.DrawBackground();

            // Make the PlanetInfo object draw itself.
            episodeInfo.DrawItem(e.Graphics, e.Bounds, this.Font, false);
        }
    }
}
