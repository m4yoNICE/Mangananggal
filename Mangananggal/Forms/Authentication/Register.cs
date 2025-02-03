using Mangananggal.Connections;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Mangananggal.Forms.Authentication
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {

           
         
        }

        private void lblBack_Click(object sender, EventArgs e)
        {
            Program.loginForm.Show();
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string defaultRole = "User";
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Fill out all fields");
                return;
            }
            try
            { 
          
                string sql = "INSERT INTO users (user_username, user_firstname, user_lastname, user_password, user_role) " +
                                "VALUES (@Username, @Firstname, @Lastname, @Password, @Role)";
                using (var conn = Connection.conn())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@Firstname", txtFName.Text);
                        cmd.Parameters.AddWithValue("@Lastname", txtLName.Text);
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@Role", defaultRole);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Your Account has been Successfully Created", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Program.loginForm.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Error occurred while registering. Please try again.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
}
