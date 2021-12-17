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
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
            loginFieldreg.Text = "Введите имя";
            loginFieldreg.ForeColor = Color.Gray;
            this.passFieldreg.UseSystemPasswordChar = false;
            passFieldreg.Text = "Введите пароль";
            passFieldreg.ForeColor = Color.Gray;
        }

        private void RegForm_Load(object sender, EventArgs e)
        {

        }

        private void loginFieldreg_Enter(object sender, EventArgs e)
        {
            if (loginFieldreg.Text == "Введите имя")
            {
                loginFieldreg.Text = "";
                loginFieldreg.ForeColor = Color.FromArgb(183, 59, 193);
            }
        }

        private void loginFieldreg_Leave(object sender, EventArgs e)
        {
            if (loginFieldreg.Text == "")
            {
                loginFieldreg.Text = "Введите имя";
                loginFieldreg.ForeColor = Color.Gray;
            }
        }

        private void passFieldreg_Enter(object sender, EventArgs e)
        {
            if (passFieldreg.Text == "Введите пароль")
            {
                this.passFieldreg.UseSystemPasswordChar = true;
                passFieldreg.Text = "";
                passFieldreg.ForeColor = Color.FromArgb(183, 59, 193);
            }
        }

        private void passFieldreg_Leave(object sender, EventArgs e)
        {
            if (passFieldreg.Text == "")
            {
                this.passFieldreg.UseSystemPasswordChar = false;
                passFieldreg.Text = "Введите пароль";
                passFieldreg.ForeColor = Color.Gray;
            }
        }

        private void button_reg_Click(object sender, EventArgs e)
        {
            if (loginFieldreg.Text == "Введите имя")
            {
                MessageBox.Show("Ошибка! Введите имя.");
                return;
            }

            if (passFieldreg.Text == "Введите пароль")
            {
                MessageBox.Show("Ошибка! Введите пароль.");
                return;
            }

            if (isUserExists())
                return;

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `pass`) VALUES(@login, @pass)", db.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginFieldreg.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = passFieldreg.Text;

            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт был успешно создан!");
                Help help = new Help();
                help.Show();
                this.Close();
            }

            else
                MessageBox.Show("Ошибка! Аккаунт не был создан.");
            db.closeConnection();
        }

        public Boolean isUserExists()
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login`= @uL", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginFieldreg.Text;
          

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже существует, введите другой!");
                return true;
            }
            else
                return false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void RegForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
        }
    }
}
