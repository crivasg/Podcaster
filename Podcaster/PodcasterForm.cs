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

            PingReply reply = Utilities.PingServer(@"8.8.8.8");
            if (reply.Status != IPStatus.Success)
            {
                String replyString = String.Format("\n\nAddress: {0}\n", reply.Address.ToString()) + 
                    String.Format("RoundTrip time: {0}\n", reply.RoundtripTime) +
                    String.Format("Time to live: {0}\n", reply.Options.Ttl) +
                    String.Format("Don't fragment: {0}\n", reply.Options.DontFragment) + 
                    String.Format("Buffer size: {0}", reply.Buffer.Length);

                MessageBox.Show(@"Failed to detect the networks. Please, chec your network settings" + replyString);
            }

            // Test how to use the syndication feed


            // Test how to create custom ListBox
            Image image = Image.FromFile(@"C:\Documents and Settings\crivas\Desktop\picture_standard.jpg");
            String description = File.ReadAllText(@"C:\Documents and Settings\crivas\Desktop\description.txt");
            Image image2 = Image.FromFile(@"C:\Documents and Settings\crivas\Desktop\geel_friday.jpg");
            String description2 = File.ReadAllText(@"C:\Documents and Settings\crivas\Desktop\geel_friday.txt");
            Image image3 = Image.FromFile(@"C:\Documents and Settings\crivas\Desktop\the_frequency.jpg");
            String description3 = File.ReadAllText(@"C:\Documents and Settings\crivas\Desktop\the_fequency.txt");

            EpisodeInfo ep1 = new EpisodeInfo()
            {
                Picture = image,
                PubDate = DateTime.Now,
                Author = @"Moisés Chiullan",
                Title = @"Giant Size 2: Punch Holes in the Sky",
                Description = description,
                Number = 2
            };
            EpisodeInfo ep2 = new EpisodeInfo()
            {
                Picture = image2,
                PubDate = DateTime.Now,
                Author = @"Faith Korpi & Jason Seifer",
                Title = @"Geek Friday 50: You Were Genuinely Concerned That I'm Ill Prepared for Business Time",
                Description = description2,
                Number = 2
            };

            EpisodeInfo ep3 = new EpisodeInfo()
            {
                Picture = image3,
                PubDate = DateTime.Now,
                Author = @"Haddie Cooke & Dan Benjamin",
                Title = @"The Frequency 23: Don't Sniff my Browser",
                Description = description3,
                Number = 2
            };

            episodeList.Items.Add(ep1);
            episodeList.Items.Add(ep2);
            episodeList.Items.Add(ep3);
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
