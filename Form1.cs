using System;
using System.Collections.Generic;
using WMPLib;
using System.IO;
using System.Windows.Forms;


namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        List<string> nlist = new List<string>();//List of names
        List<string> plist = new List<string>();//List of paths
        string[] library_list;//Used in LoadList()
        int mode = 0;//Playing mode...0 means "Default"
        bool playing;//player's state
        int selected = -1;//number of file selected in listbox1...-1 means no file has been selected
        string[] details;//Used to transfer the info from Details() to other voids and Forms
        int error = 0;//continious
        bool paused = false;


        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            Second.Start();
            if (!File.Exists("list.txt"))
                File.WriteAllText("list.txt", "");
            LoadList();
        }

        private void Second_Tick(object sender, EventArgs e)
        {
            button_lights_menu_checks();
            if (!playing)
                Play_Mode();
        }//Timer

        private void axWindowsMediaPlayer1_PlayStateChange_1(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPlaying)
            {
                playing = true;
                paused = false;
                pauseToolStripMenuItem.Text = "Pause";
            }
                

            if (axWindowsMediaPlayer1.playState == WMPPlayState.wmppsMediaEnded)
            {
                playing = false; paused = true;
                pauseToolStripMenuItem.Text = "Play";
            }
                

            if (axWindowsMediaPlayer1.playState == WMPPlayState.wmppsPaused)
            {
                paused = true;
                pauseToolStripMenuItem.Text = "Play";
            }
                
        }

        void LoadList()

        {
            try
            {
                nlist.Clear(); plist.Clear();
                library_list = File.ReadAllLines("list.txt");
                foreach (string line in library_list)
                {
                    FileInfo f_info = new FileInfo(line);
                    plist.Add(f_info.FullName);
                    nlist.Add(f_info.Name);
                }
                listBox1.Items.Clear();
                for (int i = 0; i < nlist.Count; i++)
                    listBox1.Items.Add(nlist[i]);
            }
            catch
            {
                ShowError();
            }


        }

        void ImportSong()
        {
            try
            {
                OpenFileDialog importsong = new OpenFileDialog();
                importsong.Multiselect = true;
                importsong.Filter = "MP3 Files (*.mp3) | *.mp3";
                importsong.Title = "Add Song";
                DialogResult result = importsong.ShowDialog();
                if (result == DialogResult.OK)
                {
                    File.AppendAllLines("list.txt", importsong.FileNames);
                    LoadList();
                }
            }
            catch
            {
                ShowError();
            }
        }

        void Details(string plist)
        {
            try
            {
                if (File.Exists(plist + ".txt"))
                    details = File.ReadAllLines(plist + ".txt");
                else
                    details = null;
            }
            catch
            {
                details = null;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex != selected)
                {
                    axWindowsMediaPlayer1.URL = plist[listBox1.SelectedIndex];
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    selected = listBox1.SelectedIndex;
                    if (File.Exists(plist[selected] + ".txt"))
                    {
                        Details(plist[selected]);
                        int i = int.Parse(details[5]); i++;
                        details[5] = i.ToString();
                        File.WriteAllLines(plist[selected] + ".txt", details);
                    }
                    else
                    {
                        string[] b = { "-", "-", "-", "-", "-", "0" };
                        File.WriteAllLines(plist[selected] + ".txt", b);
                    }
                }
            }
            catch
            {
                ShowError();
            }
        }

        void Play_Mode()
        {
            try
            {
                int next;
                switch (mode)
                {
                    case 1://"Repeat All" mode----same as "Next"
                        next = listBox1.SelectedIndex + 1;
                        if (next >= listBox1.Items.Count)
                            next = 0;
                        listBox1.SelectedIndex = next;
                        break;
                    case 2://"Shuffle" mode
                        int same = listBox1.SelectedIndex;
                        Random number = new Random();
                        next = number.Next(0, listBox1.Items.Count - 1);
                        while (next == same)
                            next = number.Next(0, listBox1.Items.Count - 1);
                        listBox1.SelectedIndex = next;
                        break;
                    case 3://"Repeat One" mode
                        axWindowsMediaPlayer1.Ctlcontrols.play();
                        break;
                    case 4://"Previous"
                        next = listBox1.SelectedIndex - 1;
                        if (next < 0)
                            next = listBox1.Items.Count - 1;
                        listBox1.SelectedIndex = next;
                        break;
                }

            }
            catch
            {
                ShowError();
            }
        }

        void button_lights_menu_checks()
        {
            if (mode == 1)//"Repeat All"
            {
                //Light only "Repeat All" Button
                button1.ForeColor = System.Drawing.Color.Blue;
                button2.ForeColor = System.Drawing.Color.Black;
                button1.Text = "Repeat All";
                //Check only "Repeat All" in Menustrip
                repeatToolStripMenuItem.CheckState = CheckState.Checked;
                shuffleToolStripMenuItem.CheckState = CheckState.Unchecked;
                repeatOneToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
            else if (mode == 2)//"Shuffle"
            {
                //Light only "Shuffle" Button
                button1.ForeColor = System.Drawing.Color.Black;
                button2.ForeColor = System.Drawing.Color.Blue;
                button1.Text = "Repeat";
                //Check only "Shuffle" in Menustrip
                repeatToolStripMenuItem.CheckState = CheckState.Unchecked;
                shuffleToolStripMenuItem.CheckState = CheckState.Checked;
                repeatOneToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
            else if (mode == 3)//"Repeat One"
            {
                //Light only "Repeat One" Button
                button1.ForeColor = System.Drawing.Color.Blue;
                button2.ForeColor = System.Drawing.Color.Black;
                button1.Text = "Repeat One";
                //Check only "Repeat One" in Menustrip
                repeatToolStripMenuItem.CheckState = CheckState.Unchecked;
                shuffleToolStripMenuItem.CheckState = CheckState.Unchecked;
                repeatOneToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else//"Default"
            {
                //"Default" Lights
                button1.ForeColor = System.Drawing.Color.Black;
                button2.ForeColor = System.Drawing.Color.Black;
                button1.Text = "Repeat";
                //"Default" Checking
                repeatToolStripMenuItem.CheckState = CheckState.Unchecked;
                shuffleToolStripMenuItem.CheckState = CheckState.Unchecked;
                repeatOneToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
            if (axWindowsMediaPlayer1.settings.mute == true)
                muteToolStripMenuItem.CheckState = CheckState.Checked;
            else
                muteToolStripMenuItem.CheckState = CheckState.Unchecked;
        }



        /////////"File" Menu Items/////////
        private void addSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4.PerformClick();
        }//Add Song

        private void editDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.PerformClick();
        }//Edit Details

        private void removeSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button5.PerformClick();
        }//Remove Song

        private void favoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }//Favorites

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }//Exit


        /////////"Playback" Menu Items/////////
        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!paused)
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                pauseToolStripMenuItem.Text ="Play";
            }
            else if (paused)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
                pauseToolStripMenuItem.Text = "Pause";
            }
        }//Pause

        private void nextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button7.PerformClick();          
        }//Next

        private void previousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button6.PerformClick();
        }//Previous

        private void repeatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mode == 1)
            {
                mode = 3;
                button1.PerformClick();
            }
            else
            {
                mode = 0;
                button1.PerformClick();
            }    
        }//Repeat All

        private void repeatOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mode == 3)
                button1.PerformClick();
            else
            {
                mode = 1;
                button1.PerformClick();
            }
        }//Repeat One

        private void shuffleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.PerformClick();
        }//Shuffle

        private void muteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.settings.mute == true)
                axWindowsMediaPlayer1.settings.mute = false;
            else
                axWindowsMediaPlayer1.settings.mute = true;
        }//Mute


        /////////"Help" Menu Items/////////
        private void aboutTrackifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }//Using Trackify

        private void aboutMeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }//About


        /////////Main App Buttons/////////
        private void button4_Click(object sender, EventArgs e)
        {
            ImportSong();
        }//Add Song

        private void button3_Click(object sender, EventArgs e)
        {
            if (selected != -1)
            {
                Details(plist[selected]);
                Form4 f4 = new Form4(details, nlist[selected], plist[selected], selected);
                f4.ShowDialog();
                LoadList();
            }

        }//Edit Details

        private void button5_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.close();
            try
            {

                if (selected != -1)
                {
                    DialogResult dialog1 = MessageBox.Show("You are about to remove the following: \n" + nlist[listBox1.SelectedIndex], "Confirm?", MessageBoxButtons.OKCancel);
                    if (dialog1 == DialogResult.OK)
                    {
                        List<string> newLibrary = new List<string>();
                        newLibrary.AddRange(File.ReadAllLines("list.txt"));
                        newLibrary.RemoveAt(listBox1.SelectedIndex);
                        File.WriteAllLines("list.txt", newLibrary.ToArray());
                        File.Delete(plist[listBox1.SelectedIndex] + ".txt");
                        LoadList();
                    }
                }
            }
            catch
            {
                ShowError();
            }

        }//Remove Song

        private void button1_Click(object sender, EventArgs e)
        {
            if (mode == 0|| mode == 2)
                mode = 1;//Repeat All
            else if (mode == 1)
                mode = 3;//Repeat One
            else if (mode == 3)
                mode = 0;//Normal
                        
        }//Repeat All-Repeat One

        private void button2_Click(object sender, EventArgs e)
        {
            if (mode != 2)
                mode = 2;
            else
                mode = 0;
        }//Shuffle

        private void button7_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            int temp = mode; mode = 1;
            Play_Mode();
            mode = temp;
        }//Next

        private void button6_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            int temp = mode; mode = 4;
            Play_Mode();
            mode = temp;
        }//Previous


        //Error-most "catch" calls this baby
        void ShowError()
        {
            error++;
            if (error == 5)
            {
                if (MessageBox.Show("Something went wrong!We suggest you quit Trackify...", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    this.Close();
                error = 0;
            }


        }

    }
}