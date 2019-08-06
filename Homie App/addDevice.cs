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
    public partial class addDevice : Form
    {
        public addDevice()
        {
            InitializeComponent();
            var rooms = File.ReadAllLines("../../settings.txt");
            listBox1.DataSource = rooms;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            List<string> rooms = File.ReadAllLines("../../settings.txt").ToList();

            if (rooms.Contains(listBox1.SelectedItem))
            {
                File.AppendAllLines("../../settings.txt", new string[] { textBox1.Text });
            }

           // File.WriteAllLines("../../settings.txt", rooms.ToArray());

           // var rooms2 = File.ReadAllLines("../../settings.txt");
            //listBox1.DataSource = rooms2;

           // this.Close();

        }
    }
}
