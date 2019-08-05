using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using Homie_App.Properties;
using System.IO;

namespace Homie_App
{
    public partial class SettingsForm : Form
    {
        MqttClient client;
        public SettingsForm()
        {
            InitializeComponent();

            string Mqtt_Server_IP;
            string user;
            string pass;

            Mqtt_Server_IP = Settings.Default["Mqtt_Server_IP"].ToString();
            user = Settings.Default["user"].ToString();
            pass = Settings.Default["pass"].ToString();

            this.textBox1.Text = Mqtt_Server_IP;
            this.textBox2.Text = user;
            this.textBox3.Text = pass;

            var rooms = File.ReadAllLines("../../settings.txt");
            checkedListBox2.DataSource = rooms;

            if (Mqtt_Server_IP != "0")
            {
                this.client = new MqttClient(Mqtt_Server_IP);
                this.client.Connect(Guid.NewGuid().ToString(), user, pass);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Settings.Default["Mqtt_Server_IP"] = textBox1.Text;
            Settings.Default["user"] = textBox2.Text;
            Settings.Default["pass"] = textBox3.Text;
            Settings.Default.Save();
            MessageBox.Show("Settings saved!","Homie says...");
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            var addRoom = new addRoom();
            addRoom.Show();
        }
    }
}
