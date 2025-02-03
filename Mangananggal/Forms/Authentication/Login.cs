using Mangananggal.Connections;
using Mangananggal.Forms.Authentication;
using Mangananggal.Forms.Navigation;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Mangananggal
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void ClearTextbox()
        {
            txtboxUsername.Text = "";
            txtboxPassword.Text = "";
        }



        private void lblRegister_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            ClearTextbox();
            this.Hide();

        }

        private void btnLogin2_Click(object sender, EventArgs e)
        {
            string username = txtboxUsername.Text;
            string password = txtboxPassword.Text;

        }

        private bool invalidInput()
        {
            return txtboxUsername.Text.Contains(",") || txtboxUsername.Text.Contains("'") || txtboxPassword.Text.Contains(",") || txtboxPassword.Text.Contains("'");
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtboxUsername.Text) && string.IsNullOrEmpty(txtboxPassword.Text))
            {
                lblError.Text = "Please fill in the blanks.";
                lblError.Visible = true;
            }
            else if (string.IsNullOrEmpty(txtboxPassword.Text))
            {
                lblError.Text = "Please enter password.";
                lblError.Visible = true;
            }
            else if (string.IsNullOrEmpty(txtboxUsername.Text))
            {
                lblError.Text = "Please enter username.";
                lblError.Visible = true;
            }
            else if (invalidInput())
            {
                lblError.Text = "Special characters are not allowed.";
                lblError.Visible = true;
            }
            else
            {
                try
                {
                    string query = "SELECT user_role, user_username, user_id FROM users WHERE user_username = @Username AND user_password = @Password";

                    using (var conn = Connection.conn()) // Using 'var' for simplicity
                    using (var cmd = new SqlCommand(query, conn)) // Same here
                    {   
                        cmd.Parameters.AddWithValue("@Username", txtboxUsername.Text);
                        cmd.Parameters.AddWithValue("@Password", txtboxPassword.Text);
                        conn.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Check if any row is returned
                            {
                                string role = reader["user_role"].ToString().Trim(); // Trim any extra spaces
                                string username = reader["user_username"].ToString().Trim();
                                int userid = Convert.ToInt32(reader["user_id"]);
                                lblError.Visible = false;
                                MessageBox.Show("Login successful!");
                                ClearTextbox();
                               

                                if (role == "Admin")
                                {
                                    new AdminNav().Show();
                                    this.Hide();
                                }
                                else if (role == "User")
                                {
                                    UserNav.Welcome(username, userid);
                                    new UserNav().Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Invalid role assigned to user.");
                                }
                            }
                            else
                            {
                                lblError.Text = "Invalid username or password.";
                                lblError.Visible = true;
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Mangananggal.Connections.Connection.TestConnection())
            {
                MessageBox.Show("Connection successful!", "Database Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Connection failed!", "Database Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
