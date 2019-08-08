using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Homie_App
{
    public class Server
    {
        private string connection = "Data Source=Database.db";
        private string databaseName = "Database.db";
        SQLiteConnection conn;

        public void Connect()
        {
            conn = new SQLiteConnection(connection);
            conn.Open();
        }

        public void SelectAll()
        {
            conn = new SQLiteConnection(connection);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM ROOMS", conn);
        }

        public string[] SelectDevices(string room)
        {
            Connect();
            SQLiteCommand cmd = new SQLiteCommand("SELECT @room from ROOMS", conn);
            cmd.ExecuteNonQuery();
            return cmd;
        }
    }

}
