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
    public partial class Form3 : Form
    {
        public Form3()
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


        private void Form3_Load(object sender, EventArgs e)
        {
            using (MySqlConnection connection = GetConnection())
            {

                // Retrieve the list of tables from the database
                string query = "SELECT * FROM tables";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                // Create a panel to hold the buttons
                Panel panel = new Panel();
                panel.AutoScroll = true;
                panel.Dock = DockStyle.Fill;

                // Set the number of buttons per row
                int buttonsPerRow = 3;
                int buttonWidth = 150;
                int buttonHeight = 100;
                int spacing = 10;
                int currentRow = 0;
                int currentColumn = 0;

                // Create buttons for each product and add them to the panel
                while (reader.Read())
                {
                    // Get the product details from the reader
                    string tableNumber = reader.GetString(1);

                    // Create a new button
                    Button button = new Button();
                    button.Text = tableNumber;
                    button.Size = new Size(buttonWidth, buttonHeight);
                    button.Location = new Point(currentColumn * (buttonWidth + spacing) + 30, currentRow * (buttonHeight + spacing) + 100);
                    /*button.BackgroundImage = ;*/

                    // Add a click event handler to the button
                    button.Click += (sender, e) =>
                    {
                        Form4 form4 = new Form4(int.Parse(tableNumber));
                        form4.Show();
                        this.Hide();
                    };

                    // Add the button to the panel
                    panel.Controls.Add(button);

                    // Update the current row and column indices
                    currentColumn++;
                    if (currentColumn >= buttonsPerRow)
                    {
                        currentColumn = 0;
                        currentRow++;
                    }
                }

                // Add the panel to the form
                this.Controls.Add(panel);

                // Close the reader
                reader.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Form4 form4 = new Form4(1);

            // Display the new form
            form4.Show();

            // Close the current form
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(2);

            // Display the new form
            form4.Show();

            // Close the current form
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(3);

            // Display the new form
            form4.Show();

            // Close the current form
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(4);

            // Display the new form
            form4.Show();

            // Close the current form
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(5);

            // Display the new form
            form4.Show();

            // Close the current form
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(6);

            // Display the new form
            form4.Show();

            // Close the current form
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
