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
    public partial class Second : UserControl
    {
        private int _totalDays = 24;
        private int _totalHabitsCount = 5;
        private List<CheckBox> _checkBoxList = new List<CheckBox>();
        private DB _db = new DB();
        public Second()
        {
            InitializeComponent();
            foreach (var control in this.Controls)
            {
                if (control is CheckBox)
                {
                    _checkBoxList.Add((CheckBox)control);
                }
            }
            _checkBoxList = _checkBoxList.OrderBy(cb => int.Parse(cb.Name.Replace("checkBox", ""))).ToList();

            var HabitsOneDays = new List<int>();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `HabbitOne` WHERE `userId`= @uId", _db.getConnection());
            command.Parameters.Add("@uId", MySqlDbType.Int32).Value = Settings.UserID;
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            foreach (DataRow row in table.Rows)
            {
                HabitsOneDays.Add(row.Field<int>("day"));
            }

            var HabitsTwoDays = new List<int>();
            command = new MySqlCommand("SELECT * FROM `HabbitTwo` WHERE `userId`= @uId", _db.getConnection());
            command.Parameters.Add("@uId", MySqlDbType.Int32).Value = Settings.UserID;
            table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                HabitsTwoDays.Add(row.Field<int>("day"));
            }

            var HabitsThreeDays = new List<int>();
            command = new MySqlCommand("SELECT * FROM `HabbitThree` WHERE `userId`= @uId", _db.getConnection());
            command.Parameters.Add("@uId", MySqlDbType.Int32).Value = Settings.UserID;
            table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                HabitsThreeDays.Add(row.Field<int>("day"));
            }

            var HabitsFourDays = new List<int>();
            command = new MySqlCommand("SELECT * FROM `HabbitFour` WHERE `userId`= @uId", _db.getConnection());
            command.Parameters.Add("@uId", MySqlDbType.Int32).Value = Settings.UserID;
            table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                HabitsFourDays.Add(row.Field<int>("day"));
            }

            var HabitsFiveDays = new List<int>();
            command = new MySqlCommand("SELECT * FROM `HabbitFive` WHERE `userId`= @uId", _db.getConnection());
            command.Parameters.Add("@uId", MySqlDbType.Int32).Value = Settings.UserID;
            table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                HabitsFiveDays.Add(row.Field<int>("day"));
            }

            foreach (var day in HabitsOneDays)
                _checkBoxList[day - 1 + _totalDays * 0].Checked = true;

            foreach (var day in HabitsTwoDays)
                _checkBoxList[day - 1 + _totalDays * 1].Checked = true;

            foreach (var day in HabitsThreeDays)
                _checkBoxList[day + _totalDays * 2].Checked = true;

            foreach (var day in HabitsFourDays)
                _checkBoxList[day + _totalDays * 3].Checked = true;

            foreach (var day in HabitsFiveDays)
                _checkBoxList[day + _totalDays * 4].Checked = true;

            foreach(var checkBox in _checkBoxList)
            {
                checkBox.CheckedChanged += CheckBox_CheckedChanged;
            }
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var index = _checkBoxList.IndexOf(checkBox);
            _db.openConnection();

            var command = new MySqlCommand();
            command.Parameters.Add("@uId", MySqlDbType.Int32).Value = Settings.UserID;
            command.Parameters.Add("@day", MySqlDbType.Int32).Value = index % _totalHabitsCount + 1;
            command.Connection = _db.getConnection();

            var habitIndex = index / _totalDays;
            if (checkBox.Checked)
            {
                switch (habitIndex)
                {
                    case 0:
                        {
                            command.CommandText = "Insert into HabbitOne Values(NULL, @uId, @day)";
                            break;
                        }
                    case 1:
                        {
                            command.CommandText = "Insert into HabbitTwo Values(NULL, @uId, @day)";
                            break;
                        }
                    case 2:
                        {
                            command.CommandText = "Insert into HabbitThree Values(NULL, @uId, @day)";
                            break;
                        }
                    case 3:
                        {
                            command.CommandText = "Insert into HabbitFour Values(NULL, @uId, @day)";
                            break;
                        }
                    case 4:
                        {
                            command.CommandText = "Insert into HabbitFive Values(NULL, @uId, @day)";
                            break;
                        }
                }
            }
            else
            {
                switch (habitIndex)
                {
                    case 0:
                        {
                            command.CommandText = "Delete from HabbitOne Where userId = @uId and day = @day";
                            break;
                        }
                    case 1:
                        {
                            command.CommandText = "Delete from HabbitTwo Where userId = @uId and day = @day";
                            break;
                        }
                    case 2:
                        {
                            command.CommandText = "Delete from HabbitThree Where userId = @uId and day = @day";
                            break;
                        }
                    case 3:
                        {
                            command.CommandText = "Delete from HabbitFour Where userId = @uId and day = @day";
                            break;
                        }
                    case 4:
                        {
                            command.CommandText = "Delete from HabbitFive Where userId = @uId and day = @day";
                            break;
                        }
                }
            }

            command.ExecuteNonQuery();
            _db.closeConnection();
        }
    }
}
