using Mangananggal.Connections;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Mangananggal.Forms.UserForms
{
    public partial class BorrowBook : Form
    {
        public static int currentBorrowerId = 1; 

        public BorrowBook()
        {
            InitializeComponent();
        }

        public int getId(int userid)
        {
            using (var conn = Connection.conn())
            {
                conn.Open();

                // Check if borrower exists
                string checkQuery = "SELECT borrower_id FROM borrower WHERE user_id = @UserId";
                using (var cmd = new SqlCommand(checkQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userid);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        currentBorrowerId = (int)result;
                        return currentBorrowerId;
                    }
                }

                // If borrower does not exist, insert a new borrower and get the borrower_id
                string insertQuery = "INSERT INTO borrower (user_id) VALUES (@UserId); SELECT SCOPE_IDENTITY();";
                using (var cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userid);

                  
                    object newBorrowerId = cmd.ExecuteScalar();
                    currentBorrowerId = Convert.ToInt32(newBorrowerId);
                    return currentBorrowerId;
                }


            }
        }
        private void BookTable_Load(object sender, EventArgs e)
        {
            LoadBooks();
            
        }

        private void LoadBooks()
        {
            using (var conn = Connection.conn())
            {
                // Define the SQL query to fetch books from the Books table
                string query = "SELECT book_id, book_name, book_author, book_genre FROM books";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

                AddBorrowButtonColumn();
            }
        }

        private void AddBorrowButtonColumn()
        {
            // Check if the button column already exists
            if (!dataGridView1.Columns.Contains("BorrowButton"))
            {
                // Create a new button column
                DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
                {
                    Name = "BorrowButton",
                    HeaderText = "Action",
                    Text = "Borrow",
                    UseColumnTextForButtonValue = true // Ensures the button displays "Borrow"
                };

                // Add the button column to the DataGridView
                dataGridView1.Columns.Add(buttonColumn);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is in the 'Borrow' button column
            if (e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "BorrowButton" && e.RowIndex >= 0)
            {
                // Get the selected book ID
                int bookId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["book_id"].Value);

                // Perform the borrowing action
                BorrowBookAction(bookId);
            }
        }

        private void BorrowBookAction(int bookId)
        {
            // Check if the user is logged in
            if (currentBorrowerId <= 0)
            {
                MessageBox.Show("You need to log in first.");
                return;
            }

            if (!IsBookAvailable(bookId))
            {
                MessageBox.Show($"Book ID: {bookId} has been successfully borrowed by User ID: {currentBorrowerId}.");
                return;
            }

            using (var conn = Connection.conn())
            {
                conn.Open();
                // Start a transaction to ensure both insertions (borrow and book availability) are handled atomically
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {

                        // Insert a record into the borrowbook table
                        string borrowQuery = "INSERT INTO borrowbook (borrower_id, book_id, borrow_date, due_date) " +
                                             "VALUES (@BorrowerId, @BookId, @BorrowDate, @DueDate)";
                        using (var cmd = new SqlCommand(borrowQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@BorrowerId", currentBorrowerId);
                            cmd.Parameters.AddWithValue("@BookId", bookId);
                            cmd.Parameters.AddWithValue("@BorrowDate", DateTime.Now);
                            cmd.Parameters.AddWithValue("@DueDate", DateTime.Now.AddDays(14));  // Example: 2-week due date
                            cmd.ExecuteNonQuery();
                        }

                    // Commit the transaction
                        transaction.Commit();
                        MessageBox.Show("Book borrowed successfully!");
                    }
                    catch (Exception ex)
                    {
                        // If there's an error, roll back the transaction
                        transaction.Rollback();
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
        }

        private bool IsBookAvailable(int bookId)
        {
            using (var conn = Connection.conn())
            {
                conn.Open();

                // Check if the book is already borrowed
                string checkQuery = "SELECT COUNT(*) FROM borrowbook WHERE book_id = @BookId AND due_date IS NULL";
                using (var cmd = new SqlCommand(checkQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    int count = (int)cmd.ExecuteScalar();
                    return count == 0; // If count is 0, the book is available
                }
            }
        }
    }
}

