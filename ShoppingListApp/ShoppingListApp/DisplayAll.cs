using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace ShoppingListApp
{
    public partial class DisplayAll : Form
    {
        public DisplayAll()
        {
            InitializeComponent();

            string myConnectionString = "server=localhost;database=csharp;uid=root;pwd=password@";
            MySqlConnection conn = new MySqlConnection(myConnectionString);
            try
            {
                conn.Open();

                int uid = LoginForm.uid;

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM items WHERE uid = " + uid + "    ", conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    string toShow = "";
                    int i = 1;
                    while (rdr.Read())
                    {
                        toShow += i.ToString() + ". " + rdr.GetString(2) + System.Environment.NewLine;
                        i++;
                    }

                    textBox1.Text = toShow;
                }
                else
                {
                    MessageBox.Show("No items in your list!");
                    this.Hide();
                    RegisterForm registerForm = new RegisterForm();
                    registerForm.ShowDialog();
                    this.Show();
                }


            }
            catch (Exception ex)
            {
                Debug.Write("DB cannot be connected!");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            homescreen hs = new homescreen();
            hs.ShowDialog();
            this.Show();
        }
    }
}
