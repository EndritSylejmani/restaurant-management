using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenagjimiRestorantav
{
    public partial class SingleOrder : Form
    {
        private int orderId;
        public SingleOrder(int orderId)
        {
            InitializeComponent();
            this.orderId = orderId;
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

        private void add_button_click(object sender, EventArgs e)
        {
            int passedId = (int)((Button)sender).Tag;

            string query = "UPDATE order_products SET product_count = product_count + 1 WHERE id = @id";

            using (MySqlConnection connection = GetConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", passedId);

                int rowsAffected = command.ExecuteNonQuery();

                // Refresh the form
                this.Close();

                // Reopen the form to reload it
                SingleOrder newForm = new SingleOrder(orderId);
                newForm.Show();
            }
        }

        private void remove_button_click(object sender, EventArgs e)
        {
            int passedId = (int)((Button)sender).Tag;

            string query = "UPDATE order_products SET product_count = CASE WHEN product_count > 0 THEN product_count - 1 ELSE product_count END WHERE id = @id; " +
                "DELETE FROM order_products WHERE product_count = 0 AND id = @id";

            using (MySqlConnection connection = GetConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", passedId);

                int rowsAffected = command.ExecuteNonQuery();

                // Refresh the form
                this.Close();

                // Reopen the form to reload it
                SingleOrder newForm = new SingleOrder(orderId);
                newForm.Show();
            }
        }


        private void SingleOrder_Load(object sender, EventArgs e)
        {
            using (MySqlConnection connection = GetConnection())
            {
                string query = "SELECT p.name, op.product_count, p.price, op.id " +
               "FROM order_products op " +
               "JOIN products p ON op.product_id = p.id " +
               "WHERE op.order_id = @order_id";


                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@order_id", orderId);

                    Panel panel = new Panel();
                    panel.Dock = DockStyle.Fill;
                    this.Controls.Add(panel);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int verticalPixels = 150;
                        while (reader.Read())
                        {
                            Label label = new Label();
                            label.Size = new Size(400, 30);
                            string value = reader.GetString(0) + " ------------------ " + reader.GetString(1) + "x - (" + reader.GetString(2) + " € per cope)";
                            label.Text = value;
                            label.Location = new Point(150, verticalPixels);
                            panel.Controls.Add(label);


                            Button plus = new Button();
                            plus.Size = new Size(50, 30);
                            plus.Text = "+";
                            plus.Location = new Point(570, verticalPixels);
                            plus.Tag = int.Parse(reader.GetString(3));
                            plus.Click += new EventHandler(add_button_click);
                            panel.Controls.Add(plus);

                            Button minus = new Button();
                            minus.Size = new Size(50, 30);
                            minus.Text = "-";
                            minus.Location = new Point(630, verticalPixels);
                            minus.Tag = int.Parse(reader.GetString(3));
                            minus.Click += new EventHandler(remove_button_click);
                            panel.Controls.Add(minus);

                            verticalPixels += 35;
                        }
                    }
                }

            }
        }
    }
}
