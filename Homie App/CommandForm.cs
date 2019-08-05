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
    public partial class CommandForm : Form
    {
        public CommandForm()
        {
            InitializeComponent();
            var rooms = File.ReadAllLines("../../settings.txt");
            listBox1.DataSource = rooms;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            var SettingsForm = new SettingsForm();
            SettingsForm.Show();
        }
    }
}
