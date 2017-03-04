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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox1.Text += "Costructed by P15044 , P15072 and P15151...\n";
            richTextBox1.Text += "Special Thanks to: Google and Uncle Johny!!!\n";
            richTextBox1.Text += "-------------------------------------------------\n";
            richTextBox1.Text += "Find P15044 at: p15044@students.cs.unipi.gr\n";
            richTextBox1.Text += "Find P15072 at: p15072@students.cs.unipi.gr\n";
            richTextBox1.Text += "Find P15151 at: p15151@students.cs.unipi.gr\n";
            richTextBox1.ReadOnly = true;
        }

    }
}
