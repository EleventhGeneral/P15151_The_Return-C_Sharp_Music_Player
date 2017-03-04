using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox1.Text += "Hi there!\n";
            richTextBox1.Text += "\n";
            richTextBox1.Text += "My name is T and I am gonna help you use Trackify!\n";
            richTextBox1.Text += "First of all, let's take a closer look to the player itself...\n";
            richTextBox1.Text += "Trackify uses a common Windows Media Player.It provides buttons with options for Play and Pause,\n";
            richTextBox1.Text += "Stop, as much as Mute and Adjust Volume.\n";
            richTextBox1.Text += "You can directly skip to a specific moment of the song but tapping the blue bar above the buttons\n";
            richTextBox1.Text += "that were previously mentioned...\n";
            richTextBox1.Text += "We also provided our platform with buttons for the options 'Repeat All'/'Repeat One', Next, Previous Song\n";
            richTextBox1.Text += "and 'Shuffle' as you can see, under the Player...\n";
            richTextBox1.Text += "At the left side of the Player we have Panel dedicated to the Playlist...You can see the songs currently in\n";
            richTextBox1.Text += "your playlist...Under the Panel you will find the button for Adding a new Song, \n";
            richTextBox1.Text += "the one for Editing the Details of the Song and the one for Removing a Song...\n";
            richTextBox1.Text += "\n";
            richTextBox1.Text += "Have fun!!!     T...\n";
            richTextBox1.ReadOnly = true;
        }

    }
}
