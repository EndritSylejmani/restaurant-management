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
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MenagjimiRestorantav
{
    public partial class Form6 : Form
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

        long orderId;
        public Form6(long orderId)
        {
            InitializeComponent();
            this.orderId = orderId;


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form6_Load(object sender, EventArgs e)
        {
            using (MySqlConnection connection = GetConnection())
            {
                string query = "SELECT p.name, op.product_count, p.price, p.price * op.product_count AS product_cost " +
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
                        double fullPrice = 0;
                        while (reader.Read())
                        {
                            Label label = new Label();
                            label.Size = new Size(300, 30);
                            string value = reader.GetString(0) + " ------------------ " + reader.GetString(1) + "x - (" + reader.GetString(2) + " € per cope)";
                            label.Text = value;
                            label.Location = new Point(50, verticalPixels);
                            panel.Controls.Add(label);
                            verticalPixels += 35;
                            fullPrice += double.Parse(reader.GetString(3));
                        }

                        Label fullPriceLabel = new Label();
                        fullPriceLabel.Size = new Size(100, 30);
                        fullPriceLabel.Text = "Gjithesej " + fullPrice;
                        fullPriceLabel.Location = new Point(50, verticalPixels);
                        panel.Controls.Add(fullPriceLabel);
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderList form = new OrderList();

            form.Show();

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();

            form.Show();

            this.Hide();
        }
    }
}
