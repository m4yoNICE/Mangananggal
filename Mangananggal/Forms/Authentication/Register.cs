using Mangananggal.Connections;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Mangananggal.Forms.Authentication
{
    public partial class Register : Form
    {
        private bool notforget = true;
        private int userId;

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

        private bool IsPasswordComplex(string password)
        {
            // Check for minimum length
            if (password.Length < 12)
                return false;

            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;
            bool hasSpecial = false;

            // Iterate through each character in the password
            foreach (char c in password)
            {
                if (char.IsUpper(c))
                    hasUpper = true;
                else if (char.IsLower(c))
                    hasLower = true;
                else if (char.IsDigit(c))
                    hasDigit = true;
                else if (!char.IsLetterOrDigit(c)) // if it's not a letter or digit, it's special
                    hasSpecial = true;
            }

            // Return true only if all conditions are met
            return hasUpper && hasLower && hasDigit && hasSpecial;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string defaultRole = "User";

            if (string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtConfPassword.Text))
            {
                MessageBox.Show("Fill out all fields");
                return;
            }

            if (!txtPassword.Text.Equals(txtConfPassword.Text))
            {
                MessageBox.Show("Confirm Password Not The Same");
                return;
            }

            if (!IsPasswordComplex(txtPassword.Text))
            {
                MessageBox.Show("Password must be at least 12 characters long and include at least one uppercase letter, one lowercase letter, one digit, and one special character.");
                return;
            }

            try
            {
                string sql;
                int rowsAffected;

                if (notforget)
                {
                    sql = "INSERT INTO users (user_username, user_firstname, user_lastname, user_password, user_role) " +
                          "VALUES (@Username, @Firstname, @Lastname, HASHBYTES('SHA2_256', @Password  + 'jasperbayot'), @Role)";

                    rowsAffected = DBHelper.DBHelper.ExecuteNonQuery(sql,
                        new SqlParameter("@Username", txtUsername.Text),
                        new SqlParameter("@Firstname", txtFName.Text),
                        new SqlParameter("@Lastname", txtLName.Text),
                        new SqlParameter("@Password", txtPassword.Text),
                        new SqlParameter("@Role", defaultRole));
                }
                else
                {
                    sql = "UPDATE users SET user_password = HASHBYTES('SHA2_256', @Password + 'jasperbayot') WHERE user_id = @UserId";

                    rowsAffected = DBHelper.DBHelper.ExecuteNonQuery(sql,
                        new SqlParameter("@Password", txtPassword.Text),
                        new SqlParameter("@UserId", userId));
                }

                if (rowsAffected > 0)
                {
                    MessageBox.Show(notforget ? "Your Account has been Successfully Created" : "Password updated successfully!",
                                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Program.loginForm.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error occurred. Please try again.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void ForgetChangePassword(int userid)
        {
            string query = "SELECT user_username, user_firstname, user_lastname FROM users WHERE user_id = " + userid;

 
            using (SqlDataReader reader = DBHelper.DBHelper.ExecuteReaderQuery(query))
            {
                if (reader.Read())
                {
                    string username = reader["user_username"].ToString().Trim();
                    string firstname = reader["user_firstname"].ToString().Trim();
                    string lastname = reader["user_lastname"].ToString().Trim();

                    txtUsername.Text = username;
                    txtFName.Text = firstname;
                    txtLName.Text = lastname;
                    txtUsername.Enabled = false;
                    txtFName.Enabled = false;
                    txtLName.Enabled = false;

                    notforget = false;
                    userId = userid;
                }
                else
                {
                    MessageBox.Show("User not found.");
                }
            }


        }

      
        private void txtConfPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
