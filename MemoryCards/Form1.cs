using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MemoryCards.Properties;
using System.Threading;

namespace MemoryCards
{
    public partial class Form1 : Form
    {
        List<Image> pictures = new List<Image>{ Resources._1, Resources._2, Resources._3, Resources._4, Resources._5, Resources._6, Resources._7, Resources._8 };
        int[] randomized =new int[16];
        PictureBox first = null;
        int matches = 0;
        int tries = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int i = 1; i < 8; i++)
            {
                int next;
                do
                {
                    next = rnd.Next(16);
                }
                while (randomized[next] != 0);
                randomized[next] = i;
                do
                {
                    next = rnd.Next(16);
                }
                while (randomized[next] != 0);
                randomized[next] = i;
            }
        }

        async private void pictureBox_Click(object sender, EventArgs e)
        {
            var clicked = sender as PictureBox;
            clicked.BackgroundImage = pictures[randomized[clicked.TabIndex]];
            if (clicked != first)
            {
                if (first != null)
                {
                    tries++;
                    this.label1.Text = "Tries:" + tries;
                    if (clicked.BackgroundImage==first.BackgroundImage )
                    {
                        matches++;
                        this.label2.Text = "Matches:" + matches;
                        clicked.Enabled = false;
                        first = null;
                        if (matches == 8)
                        {
                            MessageBox.Show("You win with " + tries + " tries.");
                            Close();
                        }
                        return;
                    }
                    else
                    {
                        this.Enabled = false;
                        await Task.Delay(1000);
                        clicked.BackgroundImage = default;
                        first.BackgroundImage = default;
                        clicked.Enabled = true;
                        first.Enabled = true;
                        first = null;
                        this.Enabled = true;
                    }
                }
                else
                {
                    first = clicked;
                    first.Enabled = false;
                }
            }
        }

    }
}
