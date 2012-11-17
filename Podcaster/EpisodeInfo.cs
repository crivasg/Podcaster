using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

// Based on http://www.blackbeltcoder.com/Articles/controls/owner-drawn-controls

namespace Podcaster
{
    class EpisodeInfo
    {
        public Image Picture { get; set ; }
        public DateTime PubDate { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Author { get; set; }
        public String Link { get; set; }
        public String Size { get; set; }
        public int Number { get; set; }
        public TimeSpan Duration { get; set; }

        public EpisodeInfo()
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

            return String.Format("{0}\n{1}\n{2}", Title, PubDate, Description);
        }

        public void DrawItem(Graphics gr, Rectangle bounds, Font font, bool showNameOnly)
        {
            // Calculate a reasonable margin.
            int margin = (int)(bounds.Height * 0.05);
            int height = bounds.Height - 2 * margin;
            double ratio = (1.0*Picture.Width) / (1.0*Picture.Height);
            int dwidth = (int)(100.0 * ratio * height)/100;

            // Draw the picture.
            Rectangle srcRect = new Rectangle(0, 0,
                Picture.Width, Picture.Height);
            Rectangle destRect = new Rectangle(
                bounds.Left + margin, bounds.Top + margin,
                dwidth, height);
            gr.DrawImage(Picture, destRect, srcRect,
                GraphicsUnit.Pixel);

            // Draw the name, mass, and year length.
            int textLeft = destRect.Right + margin;
            int width = bounds.Width - textLeft;
            Rectangle layoutRect = new Rectangle(
                textLeft, bounds.Top,
                width, bounds.Height);
            using (StringFormat stringFormat = new StringFormat())
            {
                stringFormat.LineAlignment = StringAlignment.Center;
                
                if (showNameOnly)
                {
                    gr.DrawString(ToString(), font, Brushes.Black,
                        layoutRect, stringFormat);
                }
                else
                {
                    gr.DrawString(EpisodeData(), font, Brushes.Black,
                        layoutRect, stringFormat);
                }
            }
        }

    }
}