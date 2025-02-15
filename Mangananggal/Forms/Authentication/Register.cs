﻿using Mangananggal.Connections;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Mangananggal.Forms.Authentication
{
    public partial class Register : Form
    {
        public string staticSalt = "jasperbayot";
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
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Fill out all fields");
                return;
            }

            if (!txtPassword.Text.Equals(txtConfPassword.Text))
            {
                MessageBox.Show("Confirm Password Not The The Same");
                return;
            }

            if (!IsPasswordComplex(txtPassword.Text))
            {
                MessageBox.Show("Password must be at least 12 characters long and include at least one uppercase letter, one lowercase letter, one digit, and one special character.");
                return;
            }

            try
            {
                string staticSalt = "jasperbayot";
                string sql = "INSERT INTO users (user_username, user_firstname, user_lastname, user_password, user_role) " +
             "VALUES (@Username, @Firstname, @Lastname, HASHBYTES('SHA2_256', @Password + 'jasperbayot'), @Role)";
                using (var conn = Connection.conn())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@Firstname", txtFName.Text);
                        cmd.Parameters.AddWithValue("@Lastname", txtLName.Text);
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text + staticSalt);
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

        private void txtConfPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
