using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homie_App
{
    public partial class addRoom : Form
    {
        SettingsForm settingsform;
        public addRoom(SettingsForm f)
        {
            InitializeComponent();
            settingsform = f;
        }

        public void PictureBox1_Click(object sender, EventArgs e)
        {
            var line = textBox1.Text;
            if (File.ReadAllLines("../../settings.txt").Length == 0)
            {
                File.AppendAllText("../../settings.txt", line);
            }
            else
            {
                File.AppendAllText("../../settings.txt", Environment.NewLine + line);
            }
            
            this.Close();
            var rooms = File.ReadAllLines("../../settings.txt");
            settingsform.checkedListBox2.DataSource = rooms;

        }
    }
}
