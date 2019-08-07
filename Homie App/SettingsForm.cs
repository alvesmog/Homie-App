using Homie_App.Properties;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;

namespace Homie_App
{
    public partial class SettingsForm : Form
    {
        MqttClient client;

        public class Room
        {
            public int id { get; set; }
            public string name { get; set; }

        }

        private static string connection = "Data Source=Database.db";
        private static string databaseName = "Database.db";

        public SettingsForm()
        {
            InitializeComponent();

            //MQTT Parameters

            string Mqtt_Server_IP;
            string user;
            string pass;

            Mqtt_Server_IP = Settings.Default["Mqtt_Server_IP"].ToString();
            user = Settings.Default["user"].ToString();
            pass = Settings.Default["pass"].ToString();

            this.textBox1.Text = Mqtt_Server_IP;
            this.textBox2.Text = user;
            this.textBox3.Text = pass;

            if (Mqtt_Server_IP != "0")
            {
                this.client = new MqttClient(Mqtt_Server_IP);
                this.client.Connect(Guid.NewGuid().ToString(), user, pass);
            }

            //Database parameters

            if (!File.Exists(databaseName))
            {
                SQLiteConnection.CreateFile(databaseName);
                SQLiteConnection conn = new SQLiteConnection(connection);
                conn.Open();

                StringBuilder sql = new StringBuilder();
                sql.AppendLine("CREATE TABLE IF NOT EXISTS ROOMS ([ID] INTEGER PRIMARY KEY AUTOINCREMENT, [ROOM_NAME] VARCHAR(100))");
                SQLiteCommand cmd = new SQLiteCommand(sql.ToString(), conn);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Couldn't create database " + ex.Message);
                }
                conn.Close();

            }

            Load_rooms();
        }

        //Change MQTT settings
        private void Button1_Click(object sender, EventArgs e)
        {
            Settings.Default["Mqtt_Server_IP"] = textBox1.Text;
            Settings.Default["user"] = textBox2.Text;
            Settings.Default["pass"] = textBox3.Text;
            Settings.Default.Save();
            MessageBox.Show("Settings saved!", "Homie says...");
        }

        //Load rooms

        public void Load_rooms()
        {
            SQLiteConnection conn = new SQLiteConnection(connection);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM ROOMS", conn);
            SQLiteDataReader dr = cmd.ExecuteReader();
            List<Room> list = new List<Room>();
            while (dr.Read())
            {
                list.Add(new Room { id = Convert.ToInt32(dr["id"]), name = dr["ROOM_NAME"].ToString() });
            }
            dataGridView1.DataSource = list;
            conn.Close();
        }

        //Add new room
        private void Button5_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            addRoom f = new addRoom();
            f.FormClosed += HandleAddRoomFormClosed;
            f.Show();
        }

        private void HandleAddRoomFormClosed(Object sender, FormClosedEventArgs e)
        {
            this.Enabled = true;
            Load_rooms();
        }

        //Delete room
        private void Button4_Click(object sender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(connection);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM ROOMS WHERE ID = @ID", conn);
            cmd.Parameters.AddWithValue("ID", Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
            try
            {
                cmd.ExecuteNonQuery();
                Load_rooms();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        //Add new device
        //private void Button2_Click(object sender, EventArgs e)
        //{
        //    this.Enabled = false;
        //    addDevice f = new addDevice();
        //    f.FormClosed += HandleAddDeviceFormClosed;
        //    f.Show();
        //}

        //private void HandleAddDeviceFormClosed(Object sender, FormClosedEventArgs e)
        //{
        //    this.Enabled = true;
        //    var devices = File.ReadAllLines("../../devices.txt");
        //    checkedListBox1.DataSource = devices;
    }
}

