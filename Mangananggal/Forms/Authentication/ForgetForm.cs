using Mangananggal.Connections;
using Mangananggal.DBHelper;
using Mangananggal.Forms.Navigation;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Timers;

namespace Mangananggal.Forms.Authentication
{
    public partial class ForgetForm : Form
    {
        private int userId;
        public ForgetForm()
        {
            InitializeComponent();
        }

        private void ForgetForm_Load(object sender, EventArgs e)
        {
            btnVerify.Enabled = false;
            txtOTPcode.Enabled = false;
            btnOTPsendcode.Enabled = false;
            panel1.Visible = true;
        }


        private void btncheckUsername_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM users WHERE user_username = '" + txtUsername.Text.Trim() + "'";
                int count = DBHelper.DBHelper.ExecuteScalarQuery(query);


                if (count > 0)
                {
                    lblCheckUsername.Text = "Username exists!";
                    btnVerify.Enabled = true;
                    txtOTPcode.Enabled = true;
                    btnOTPsendcode.Enabled = true;
                    panel1.Visible = false;
                }
                else
                {
                    lblCheckUsername.Text = "Username not found.";
                    btnVerify.Enabled = false;
                    txtOTPcode.Enabled = false;
                    btnOTPsendcode.Enabled = false;
                    panel1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private int countdown = 20; // 20-second timer
        private System.Windows.Forms.Timer countdownTimer = new System.Windows.Forms.Timer();

        private void btnOTPsendcode_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve user ID
                string userQuery = "SELECT user_id FROM users WHERE user_username = '" + txtUsername.Text.Trim() + "'";
                userId = DBHelper.DBHelper.ExecuteScalarQuery(userQuery);

                // Generate a random OTP (6-digit)
                Random random = new Random();
                int otpCode = random.Next(100000, 999999);

                string insertQuery = "INSERT INTO otp (user_id, otp_code, otp_expiration) VALUES (@UserId, @OtpCode, DATEADD(MINUTE, 1, GETDATE()))";
                int rowsAffected = DBHelper.DBHelper.ExecuteNonQuery(insertQuery,
                    new SqlParameter("@UserId", userId),
                    new SqlParameter("@OtpCode", otpCode));
                if (rowsAffected > 0)
                {
                    MessageBox.Show($"OTP Code is {otpCode}\nUse it within 1 minute", "OTP Code", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error occurred while generating OTP. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Start the countdown timer
                btnOTPsendcode.Enabled = false;
                lblOTPcountdown.Text = $"You can send code again in {countdown} seconds";
                lblOTPcountdown.Visible = true;
                countdownTimer.Interval = 1000;
                countdownTimer.Tick += CountdownTimer_Tick;
                countdownTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            countdown--;
            lblOTPcountdown.Text = $"You can send code again in {countdown} seconds";

            if (countdown <= 0)
            {
                countdownTimer.Stop();
                lblOTPcountdown.Visible = false;
                btnOTPsendcode.Enabled = true;
                countdown = 20;
            }
        }

        private void lblBack_Click(object sender, EventArgs e)
        {
            Program.loginForm.Show();
            this.Close();
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM otp WHERE otp_code = '" + txtOTPcode.Text.Trim() + "'";
                int count = DBHelper.DBHelper.ExecuteScalarQuery(query);


                if (count > 0)
                {
                    MessageBox.Show($"OTP Code Verified", "Verified", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Register register = new Register();
                    register.ForgetChangePassword(userId); // Pass the userId to the Register form
                    register.Show();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Wrong Code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        
    }
}
