using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Podcaster
{
    public partial class PodcasterForm : Form
    {
        // Indicate the amount of space needed for a ListBox item.
        private const int itemHeight = 100;

        public PodcasterForm()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            podcasterStatusLabel.Text = String.Empty;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void episodeListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            //??
            Episode episodeInfo = listBox.Items[e.ItemIndex] as Episode;

            // Draw the background.
            e.DrawBackground();

            // Make the PlanetInfo object draw itself.
            episodeInfo.DrawItem(e.Graphics, e.Bounds, this.Font, false);
        }

        // Handle the MeasureItem event for an owner-drawn ListBox. 
        private void ListBox1_MeasureItem(object sender,MeasureItemEventArgs e)
        {
            e.ItemHeight = itemHeight;
        }
    }
}
