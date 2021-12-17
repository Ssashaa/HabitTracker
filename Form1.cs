using MySql.Data.MySqlClient;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.passField.AutoSize = false;
            this.passField.Size = new Size(this.passField.Size.Width, 32);
            loginField.Text = "Введите имя";
            loginField.ForeColor = Color.Gray;
            this.passField.UseSystemPasswordChar = false;
            passField.Text = "Введите пароль";
            passField.ForeColor = Color.Gray;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button_login_Click(object sender, EventArgs e)
        {
            String loginUser = loginField.Text;
            String passUser = passField.Text;

            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login`= @uL AND `pass` = @uP ", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passUser;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {

                Settings.UserID = table.Rows[0].Field<UInt32>("id");

                Body body = new Body();
                body.Show();
                this.Close();
            }
            else
                MessageBox.Show("Ошибка!");


        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void loginField_Enter(object sender, EventArgs e)
        {
            if (loginField.Text == "Введите имя")
            {
                loginField.Text = "";
                loginField.ForeColor = Color.FromArgb(183, 59, 193);
            }
        }

        private void loginField_Leave(object sender, EventArgs e)
        {

            if (loginField.Text == "")
            {
                loginField.Text = "Введите имя";
                loginField.ForeColor = Color.Gray;
            }
        }

        private void passField_Enter(object sender, EventArgs e)
        {
            if (passField.Text == "Введите пароль")
            {
                this.passField.UseSystemPasswordChar = true;
                passField.Text = "";
                passField.ForeColor = Color.FromArgb(183, 59, 193);
            }
        }

        private void passField_Leave(object sender, EventArgs e)
        {
            if (passField.Text == "")
            {
                this.passField.UseSystemPasswordChar = false;
                passField.Text = "Введите пароль";
                passField.ForeColor = Color.Gray;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ////Application.Exit();
        }
    }
}
