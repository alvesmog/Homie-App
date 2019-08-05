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
        public addRoom()
        {
            InitializeComponent();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            var line = textBox1.Text;
            File.AppendAllText("../../settings.txt", Environment.NewLine + line);
            this.Close();
        }
    }
}
