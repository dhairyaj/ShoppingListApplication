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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            this.SignUpButton.Click += new EventHandler(SignUpButton_Click);
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

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            string myConnectionString = "server=localhost;database=csharp;uid=root;pwd=password@";
            MySqlConnection conn = new MySqlConnection(myConnectionString);
            try
            {
                conn.Open();

                string username = textBox1.Text.ToString();
                string password = textBox2.Text.ToString();
                string cpassword = textBox4.Text.ToString();
                string email = textBox3.Text.ToString();

                int error = 0;
                string errorMsg = "";

                if(password != cpassword)
                {
                    error++;
                    errorMsg += "Passwords do not match!\n";
                    Debug.WriteLine(password);
                    Debug.WriteLine(cpassword);
                }

                if(!IsValidEmail(email))
                {
                    error++;
                    errorMsg += "Invalid email address!\n";
                }

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE username='" + username + "' or email = '" + email + "'", conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if(count > 0)
                {
                    error++;
                    errorMsg += "Account with this username or email exists!";
                }

                if(error > 0)
                {
                    MessageBox.Show(errorMsg);
                } 
                else
                {
                    string sql = "INSERT INTO users(username, password, email) VALUES(@Username, @Password, @Email)";
                    cmd = new MySqlCommand(sql, conn);
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Email", email);

                    cmd.ExecuteNonQuery();

                    DialogResult result = MessageBox.Show("Registered successfully, log in!");
                    
                    if(result == DialogResult.OK)
                    {
                        this.Hide();
                        LoginForm loginForm = new LoginForm();
                        loginForm.ShowDialog();
                        this.Show();
                    }
                }

                conn.Close();
            } catch(Exception ex)
            {
                textBox1.Text = "Failure DB Connection!";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            this.Show();
        }
    }
}
