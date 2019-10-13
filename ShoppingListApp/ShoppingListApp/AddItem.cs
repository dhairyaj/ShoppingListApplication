using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace ShoppingListApp
{
    public partial class AddItem : Form
    {
        public AddItem()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddItemButton_Click(object sender, EventArgs e)
        {
            string myConnectionString = "server=localhost;database=csharp;uid=root;pwd=password@";
            MySqlConnection conn = new MySqlConnection(myConnectionString);
            try
            {
                conn.Open();

                int uid = LoginForm.uid;
                string item = textBox1.Text.ToString();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO items(uid, item) VALUES('" + uid + "', '" + item + "')", conn);

                cmd.ExecuteNonQuery();

                DialogResult res = MessageBox.Show("Successfully Added!");

                if(res == DialogResult.OK)
                {
                    this.Hide();
                    homescreen hs = new homescreen();
                    hs.ShowDialog();
                    this.Show();
                }

            }
            catch (Exception ex)
            {
                Debug.Write("DB cannot be connected!", ex.ToString());
            }
        }
    }
}
