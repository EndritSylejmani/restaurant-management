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
    public partial class OrderList : Form
    {
        public OrderList()
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

        private void dynamicButton_Click(object sender, EventArgs e)
        {
            int passedId = (int)((Button)sender).Tag;
            SingleOrder form = new SingleOrder(passedId);
            form.Show();
            this.Hide();
        }

        private void fshijePorosine(object sender, EventArgs e)
        {
            int passedId = (int)((Button)sender).Tag;

            MySqlConnection connection = GetConnection();

            // Begin the transaction
            using (MySqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    // Delete order_products records
                    string deleteOrderProductsQuery = "DELETE FROM order_products WHERE order_id = @orderId";
                    MySqlCommand deleteOrderProductsCommand = new MySqlCommand(deleteOrderProductsQuery, connection);
                    deleteOrderProductsCommand.Parameters.AddWithValue("@orderId", passedId);
                    deleteOrderProductsCommand.Transaction = transaction;
                    deleteOrderProductsCommand.ExecuteNonQuery();

                    // Delete the order
                    string deleteOrderQuery = "DELETE FROM orders WHERE id = @orderId";
                    MySqlCommand deleteOrderCommand = new MySqlCommand(deleteOrderQuery, connection);
                    deleteOrderCommand.Parameters.AddWithValue("@orderId", passedId);
                    deleteOrderCommand.Transaction = transaction;
                    deleteOrderCommand.ExecuteNonQuery();

                    // Commit the transaction
                    transaction.Commit();

                    OrderList form = new OrderList();
                    form.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    // Handle any exception that occurred
                    transaction.Rollback();
                    Console.WriteLine("Error occurred during deletion: " + ex.Message);
                }
            }
        }

        private void OrderList_Load(object sender, EventArgs e)
        {
            using (MySqlConnection connection = GetConnection())
            {
                string query = "SELECT * FROM ORDERS";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    Panel panel = new Panel();
                    panel.Dock = DockStyle.Fill;
                    panel.AutoScroll = true;
                    this.Controls.Add(panel);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int verticalPixels = 20;
                        while (reader.Read())
                        {
                            Button button = new Button();
                            button.Size = new Size(300, 50);
                            string value = "Porosia nr: " + reader.GetString(0) + " - Tavolina nr:" + reader.GetString(1);
                            button.Text = value;
                            button.Location = new Point(30, verticalPixels);
                            button.Tag = int.Parse(reader.GetString(0));
                            button.Click += new EventHandler(dynamicButton_Click);
                            panel.Controls.Add(button);

                            Button delete = new Button();
                            delete.Size = new Size(100, 50);
                            delete.Text = "Fshij porosine";
                            delete.Location = new Point(340, verticalPixels);
                            delete.Tag = int.Parse(reader.GetString(0));
                            delete.Click += new EventHandler(fshijePorosine);
                            panel.Controls.Add(delete);

                            verticalPixels += 55;
                        }
                    }
                }

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
