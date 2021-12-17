using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Попытка_2
{
    public partial class Body : Form
    {
        public Body()
        {
            InitializeComponent();
            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
            first1.BringToFront();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
            first1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button2.Height;
            SidePanel.Top = button2.Top;
            second1.BringToFront();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            tdDay.Text = DateTime.Now.ToString("dd");
            tdWD.Text = DateTime.Now.ToString("dddd");
        }

        private void Body_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button3.Height;
            SidePanel.Top = button3.Top;
            userControl11.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button4.Height;
            SidePanel.Top = button4.Top;
            help_body1.BringToFront();
        }

        private void Body_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
