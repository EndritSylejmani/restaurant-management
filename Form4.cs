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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MenagjimiRestorantav
{
    public partial class Form4 : Form
    {

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

        int tableNumber;
        public Form4(int tableNumber)
        {
            this.tableNumber = tableNumber;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label3.Text = (int.Parse(label3.Text) + 1).ToString();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = (int.Parse(label2.Text) + 1).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label4.Text = (int.Parse(label4.Text) + 1).ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label5.Text = (int.Parse(label5.Text) + 1).ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label6.Text = (int.Parse(label6.Text) + 1).ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label7.Text = (int.Parse(label7.Text) + 1).ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = GetConnection())
            {
                string query = "INSERT INTO orders (table_id) VALUES (" + tableNumber.ToString() + ")";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                long lastInsertedId = cmd.LastInsertedId;
                if (label2.Text != "0")
                {
                    string query2 = "INSERT INTO order_products (order_id, product_id, product_count) VALUES (" + lastInsertedId.ToString() + " , 1, " + label2.Text + " )";
                    MySqlCommand cmd2 = new MySqlCommand(query2, connection);
                    cmd2.ExecuteNonQuery();
                }
                if (label3.Text != "0")
                {
                    string query2 = "INSERT INTO order_products (order_id, product_id, product_count) VALUES (" + lastInsertedId.ToString() + " , 2, " + label3.Text + " )";
                    MySqlCommand cmd2 = new MySqlCommand(query2, connection);
                    cmd2.ExecuteNonQuery();
                }
                if (label4.Text != "0")
                {
                    string query2 = "INSERT INTO order_products (order_id, product_id, product_count) VALUES (" + lastInsertedId.ToString() + " , 3, " + label4.Text + " )";
                    MySqlCommand cmd2 = new MySqlCommand(query2, connection);
                    cmd2.ExecuteNonQuery();
                }
                if (label5.Text != "0")
                {
                    string query2 = "INSERT INTO order_products (order_id, product_id, product_count) VALUES (" + lastInsertedId.ToString() + " , 4, " + label5.Text + " )";
                    MySqlCommand cmd2 = new MySqlCommand(query2, connection);
                    cmd2.ExecuteNonQuery();
                }
                if (label6.Text != "0")
                {
                    string query2 = "INSERT INTO order_products (order_id, product_id, product_count) VALUES (" + lastInsertedId.ToString() + " , 5, " + label6.Text + " )";
                    MySqlCommand cmd2 = new MySqlCommand(query2, connection);
                    cmd2.ExecuteNonQuery();
                }
                if (label7.Text != "0")
                {
                    string query2 = "INSERT INTO order_products (order_id, product_id, product_count) VALUES (" + lastInsertedId.ToString() + " , 6, " + label7.Text + " )";
                    MySqlCommand cmd2 = new MySqlCommand(query2, connection);
                    cmd2.ExecuteNonQuery();
                }

                Form6 form6 = new Form6(lastInsertedId);

                form6.Show();

                this.Hide();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int value = int.Parse(label6.Text);
            if (value > 0)
            {
                label6.Text = (value - 1).ToString();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int value = int.Parse(label2.Text);
            if (value > 0)
            {
                label2.Text = (value - 1).ToString();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int value = int.Parse(label3.Text);
            if (value > 0)
            {
                label3.Text = (value - 1).ToString();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int value = int.Parse(label4.Text);
            if (value > 0)
            {
                label4.Text = (value - 1).ToString();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int value = int.Parse(label5.Text);
            if (value > 0)
            {
                label5.Text = (value - 1).ToString();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int value = int.Parse(label7.Text);
            if (value > 0)
            {
                label7.Text = (value - 1).ToString();
            }
        }
    }
}
