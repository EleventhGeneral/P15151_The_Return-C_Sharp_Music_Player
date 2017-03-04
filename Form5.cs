using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Form5 : Form
    {
        List<int> timesPlayed = new List<int>();
        List<string> tracks = new List<string>();
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            try
            {
                string[] lines = File.ReadAllLines("list.txt");
                foreach (string str in lines)
                {
                    FileInfo fl = new FileInfo(str + ".txt");
                    string name = Path.GetFileNameWithoutExtension(str + ".txt");
                    if (fl.Extension == ".txt")
                    {
                        timesPlayed.Add(int.Parse(File.ReadAllLines(fl.FullName)[5]));
                        tracks.Add(name);
                    }
                }

                List<int> sortedTimes = new List<int>(Enumerable.Range(0, timesPlayed.Count));
                sortedTimes.Sort(delegate (int k, int l) { return -timesPlayed[k] + timesPlayed[l]; });

                for (int i = 0; i < sortedTimes.Count; ++i)
                {

                    int j = sortedTimes[i];
                    listBox1.Items.Add(tracks[j] + " - " + timesPlayed[j].ToString());
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong...odds are you haven't played any songs yet...");
            }
            
        }
    }
}
