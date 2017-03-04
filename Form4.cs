using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Form4 : Form
    {
        string times_played = "0";
        string final_path = " ";
        string ext = "";
        string file_Path = "";
        string file_Directory = "";
        int index = 0;
        public Form4(string[] details,string file_Name,string path,int id)
        {
            InitializeComponent();
            try
            {
                if (details != null && details.Length == 6)
                {

                    textBox2.Text = details[1];
                    textBox3.Text = details[2];
                    comboBox1.Text = details[3];
                    comboBox2.Text = details[4];

                }
                textBox1.Text = file_Name;
                FileInfo fl = new FileInfo(path);
                ext = fl.Extension;
                file_Path = path;
                file_Directory = fl.Directory.FullName;
                index = id;

                times_played = details[5];
                Text += ":" + file_Name;
                final_path = path;
            }
            catch{ }
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!textBox1.Text.Contains(ext))
                    textBox1.Text += ext;

                if (!File.Exists(file_Directory + "\\" + textBox1.Text))
                {
                    File.Copy(file_Path, file_Directory + "\\" + textBox1.Text);
                    File.Copy(file_Path + ".txt", file_Directory + "\\" + textBox1.Text + ".txt");

                    File.Delete(file_Path);
                    File.Delete(file_Path + ".txt");
                }
                
                List<string> newLib = new List<string>();
                newLib.AddRange(File.ReadAllLines("list.txt"));
                newLib.RemoveAt(index);
                newLib.Add(file_Directory + "\\" + textBox1.Text);
                File.WriteAllLines("list.txt", newLib.ToArray());

                string[] Saved = {textBox1.Text,textBox2.Text, textBox3.Text, comboBox1.Text, comboBox2.Text, times_played };
                File.WriteAllLines(file_Directory + "\\" + textBox1.Text + ".txt", Saved, Encoding.UTF8);

                MessageBox.Show("Details edited successfully!!!");
                
            }
            catch
            {
               MessageBox.Show("Ooops...Something went wrong... :/");
            }
            this.Close();

        }//"Save" Button

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }//"Cancel" Button

    }
}
