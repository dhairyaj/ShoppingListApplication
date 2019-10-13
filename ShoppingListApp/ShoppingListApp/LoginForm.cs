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
    public partial class LoginForm : Form
    {
        public static int uid = -1;

        public LoginForm()
        {
            InitializeComponent();
            this.LoginButton.Click += new EventHandler(loginButton_Click);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string myConnectionString = "server=localhost;database=csharp;uid=root;pwd=password@";
            MySqlConnection conn = new MySqlConnection(myConnectionString);
            try
            {
                conn.Open();

                string email = textBox1.Text.ToString();
                string password = textBox2.Text.ToString();

                int error = 0;
                string errorMsg = "";

                if (!IsValidEmail(email))
                {
                    error++;
                    errorMsg += "Invalid email address!\n";
                }

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE email = '" + email + "'", conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (rdr.GetString(2) == password)
                        {
                            DialogResult res = MessageBox.Show("Logged in successfully!");
                            if (res == DialogResult.OK)
                            {
                                uid = rdr.GetInt16(0);
                                this.Hide();
                                homescreen hs = new homescreen();
                                hs.ShowDialog();
                                this.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Incorrect credentials!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Account does not exist!");
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
    }
}
