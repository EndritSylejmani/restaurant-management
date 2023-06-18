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

namespace MenagjimiRestorantav
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private MySqlConnection GetConnection()
        {
            string server = "localhost";
            string database = "restaurant";
            string username = "root";
            string password = "";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" +
            "UID=" + username + ";" + "PASSWORD=" + password + ";";

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (MySqlConnection connection = GetConnection())
            {
                string query =
                    "select * from users WHERE username = \"" + textBox1.Text + "\" AND password = \"" + textBox2.Text + "\" LIMIT 1;";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        UserObject user = new UserObject();
                        user.ID = reader.GetString(0);
                        user.username = reader.GetString(1);
                        user.role = reader.GetString(4);
                        UserSession.Login(user);
                    }

                    Form3 form3 = new Form3();

                    form3.Show();

                    this.Hide();
                } else {
                    MessageBox.Show("Te dhenat qe i keni dhene nuk jane te sakta");
                }

            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }
    }
}
