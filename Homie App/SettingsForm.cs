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

        public void Refresh_Listbox()
        {
            this.checkedListBox1.Invalidate();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Settings.Default["Mqtt_Server_IP"] = textBox1.Text;
            Settings.Default["user"] = textBox2.Text;
            Settings.Default["pass"] = textBox3.Text;
            Settings.Default.Save();
            MessageBox.Show("Settings saved!", "Homie says...");
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            var addRoom = new addRoom(this);
            addRoom.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            List<string> lines = File.ReadAllLines("../../settings.txt").ToList();

            foreach (string selectedItem in checkedListBox2.CheckedItems)
            {
                if (lines.Contains(selectedItem))
                {
                    lines.Remove(selectedItem);
                }

                File.WriteAllLines("../../settings.txt", lines.ToArray());

                var rooms = File.ReadAllLines("../../settings.txt");
                checkedListBox2.DataSource = rooms;

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            addDevice f = new addDevice();
            f.FormClosed += HandleAddDeviceFormClosed;
            f.Show();
        }


        private void HandleAddDeviceFormClosed(Object sender, FormClosedEventArgs e)
        {
            this.Enabled = true;
            var devices = File.ReadAllLines("../../devices.txt");
            checkedListBox1.DataSource = devices;
        }
    }
}
