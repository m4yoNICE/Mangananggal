using Mangananggal.Connections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mangananggal.Forms.AdminForms
{
    public partial class UserTable : Form
    {
        public string role;
        public UserTable()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void ClearTextboxes()
        {
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtFirstname.Text = string.Empty;
            txtLastname.Text = string.Empty;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadUsers()
        {
            using (var conn = Connection.conn())
            { 
                // Define the SQL query to fetch users from the Users table
                string query = "SELECT user_id, user_username, user_password,user_firstname, user_lastname, user_role FROM users";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void UserTable_Load(object sender, EventArgs e)
        {

        }
       

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string firstname = txtFirstname.Text.Trim();
            string lastname = txtLastname.Text.Trim();
            string roles = role;

            if (string.IsNullOrEmpty(roles))
            {
                MessageBox.Show("Role cannot be empty.");
                return;
            }

            // Validate input fields
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname))
            {
                MessageBox.Show("Please fill out all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Ensure a record is selected
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    if (dataGridView1.Rows[rowIndex].Cells["user_id"].Value != null)
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["user_id"].Value);

                        // SQL Update query
                        string sql = @"UPDATE users 
                               SET user_username = @Username, 
                                   user_firstname = @Firstname, 
                                   user_lastname = @Lastname, 
                                   user_password = @Password, 
                                   user_role = @Role 
                               WHERE user_id = @id";

                        // Open connection and execute command
                        using (var conn = Connection.conn())
                        {
                            conn.Open(); // Explicitly open the connection
                            using (var cmd = new SqlCommand(sql, conn))
                            {
                                // Add parameters
                                cmd.Parameters.AddWithValue("@Username", username);
                                cmd.Parameters.AddWithValue("@Firstname", firstname);
                                cmd.Parameters.AddWithValue("@Lastname", lastname);
                                cmd.Parameters.AddWithValue("@Password", password);
                                cmd.Parameters.AddWithValue("@Role", roles);
                                cmd.Parameters.AddWithValue("@id", id);


                                // Execute query and handle results
                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Account successfully updated!", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ClearTextboxes();
                                    LoadUsers();
                                }
                               
                            }
                        }
                    }
                   
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure a row is selected in the DataGridView
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["user_id"].Value); // Adjust to your primary key field

                    // Confirm deletion
                    var confirmResult = MessageBox.Show("Are you sure you want to delete this user?",
                                                        "Confirm Delete",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {

                        string sql = "DELETE FROM users WHERE user_id = @ID";

                        using (var conn = Connection.conn())
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand(sql, conn))
                            {
                                // Add the parameter for the query
                                cmd.Parameters.AddWithValue("@ID", id);

                                // Execute the delete command
                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("User successfully deleted from the list!",
                                                    "Delete Success",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information);

                                    // Refresh the data in the DataGridView
                                    LoadUsers();

                                }

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if the clicked row is valid (e.g., not a header row)
                if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    // Safely assign the values to the textboxes
                    txtUsername.Text = dataGridView1.Rows[e.RowIndex].Cells["user_username"].Value?.ToString();
                    txtPassword.Text = dataGridView1.Rows[e.RowIndex].Cells["user_password"].Value?.ToString();
                    txtFirstname.Text = dataGridView1.Rows[e.RowIndex].Cells["user_firstname"].Value?.ToString();
                    txtLastname.Text = dataGridView1.Rows[e.RowIndex].Cells["user_lastname"].Value?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rdAdmin_CheckedChanged_1(object sender, EventArgs e)
        {
            role = "Admin";
        }

        private void rdUser_CheckedChanged_1(object sender, EventArgs e)
        {
            role = "User";
        }

        
    }
}
