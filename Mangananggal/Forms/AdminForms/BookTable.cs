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
    public partial class BookTable : Form
    {
        public BookTable()
        {
            InitializeComponent();
        }

        private void BookTable_Load(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void ClearTextboxes()
        {
            txtName.Text = string.Empty;
            txtAuthor.Text = string.Empty;
            txtGenre.Text = string.Empty;
            txtPrice.Text = string.Empty;

        }

        private void LoadBooks()
        {
            using (var conn = Connection.conn())
            {
                // Define the SQL query to fetch users from the Users table
                string query = "SELECT book_id, book_name, book_author, book_genre FROM books";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

       
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if the clicked row is valid (e.g., not a header row)
                if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    // Safely assign the values to the textboxes
                    txtName.Text = dataGridView1.Rows[e.RowIndex].Cells["book_name"].Value?.ToString();
                    txtAuthor.Text = dataGridView1.Rows[e.RowIndex].Cells["book_author"].Value?.ToString();
                    txtGenre.Text = dataGridView1.Rows[e.RowIndex].Cells["book_genre"].Value?.ToString();
                    txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells["book_price"].Value?.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtAuthor.Text) || string.IsNullOrEmpty(txtGenre.Text))
            {
                MessageBox.Show("Fill out all fields");
                return;
            }
            decimal bookPrice;
            if (!decimal.TryParse(txtPrice.Text, out bookPrice))
            {
                MessageBox.Show("Please enter a valid price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string sql = "INSERT INTO books (book_name, book_author, book_genre, book_price) " +
                             "VALUES (@BookName, @BookAuthor, @BookGenre, @BookPrice)";
                using (var conn = Connection.conn())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookName", txtName.Text);
                        cmd.Parameters.AddWithValue("@BookAuthor", txtAuthor.Text);
                        cmd.Parameters.AddWithValue("@BookGenre", txtGenre.Text);
                        cmd.Parameters.AddWithValue("@BookPrice", txtPrice.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Book has been successfully added", "Add Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearTextboxes();
                            LoadBooks();
                        }
                        else
                        {
                            MessageBox.Show("Error occurred while adding the book. Please try again.", "Add Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string bookName = txtName.Text.Trim();
            string bookAuthor = txtAuthor.Text.Trim();
            string bookGenre = txtGenre.Text.Trim();
            decimal bookPrice;

            // Validate input fields
            if (string.IsNullOrEmpty(bookName) || string.IsNullOrEmpty(bookAuthor) || string.IsNullOrEmpty(bookGenre))
            {
                MessageBox.Show("Please fill out all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Try to parse the price input
            if (!decimal.TryParse(txtPrice.Text, out bookPrice))
            {
                MessageBox.Show("Please enter a valid price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            try
            {
                // Ensure a record is selected
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    if (dataGridView1.Rows[rowIndex].Cells["book_id"].Value != null)
                    {
                        int bookID = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["book_id"].Value);

                        // SQL Update query
                        string sql = @"UPDATE books 
                                SET book_name = @BookName, 
                                    book_author = @BookAuthor, 
                                    book_genre = @BookGenre,
                                    book_price = @BookPrice
                                WHERE book_id = @BookID";

                        // Open connection and execute command
                        using (var conn = Connection.conn())
                        {
                            conn.Open(); // Explicitly open the connection
                            using (var cmd = new SqlCommand(sql, conn))
                            {
                                // Add parameters
                                cmd.Parameters.AddWithValue("@BookName", bookName);
                                cmd.Parameters.AddWithValue("@BookAuthor", bookAuthor);
                                cmd.Parameters.AddWithValue("@BookGenre", bookGenre);
                                cmd.Parameters.AddWithValue("@BookID", bookID);
                                cmd.Parameters.AddWithValue("@BookPrice", bookPrice);

                                // Execute query and handle results
                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Book successfully updated!", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ClearTextboxes();
                                    LoadBooks();
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
                    int bookID = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["book_id"].Value); // Adjust to your primary key field

                    // Confirm deletion
                    var confirmResult = MessageBox.Show("Are you sure you want to delete this book?",
                                                        "Confirm Delete",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        string sql = "DELETE FROM books WHERE book_id = @BookID";

                        using (var conn = Connection.conn())
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand(sql, conn))
                            {
                                // Add the parameter for the query
                                cmd.Parameters.AddWithValue("@BookID", bookID);

                                // Execute the delete command
                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Book successfully deleted from the list!",
                                                    "Delete Success",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information);

                                    // Refresh the data in the DataGridView
                                    LoadBooks();
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
    }
}
