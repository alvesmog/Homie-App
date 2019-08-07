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
using System.Data.SQLite;

namespace Homie_App
{

    public partial class addRoom : Form
    {


        SettingsForm settingsform;
        public addRoom()
        {
            InitializeComponent();
        }

        public void PictureBox1_Click(object sender, EventArgs e)
        {
            SettingsForm.Room r = new SettingsForm.Room();
            r.name = textBox1.Text;

            string connection = "Data Source=Database.db";
            try
            {
                SQLiteConnection conn = new SQLiteConnection(connection);
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand("INSERT INTO ROOMS (ROOM_NAME) VALUES (@ROOM_NAME)", conn);
                cmd.Parameters.AddWithValue("ROOM_NAME", r.name);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sucess!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Couldn't add room: " + ex.Message);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
    }
}
