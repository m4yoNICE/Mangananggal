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

namespace Mangananggal.Forms.UserForms
{
    public partial class ReturnBook : Form
    {
        public static int CurrentUserId = 1;
        public ReturnBook()
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
                        CurrentUserId = (int)result;
                        return CurrentUserId;
                    }
                }

                // If borrower does not exist, insert a new borrower and get the borrower_id
                string insertQuery = "INSERT INTO borrower (user_id) VALUES (@UserId); SELECT SCOPE_IDENTITY();";
                using (var cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userid);


                    object newBorrowerId = cmd.ExecuteScalar();
                    CurrentUserId = Convert.ToInt32(newBorrowerId);
                    return CurrentUserId;
                }


            }
        }
        private void ReturnBook_Load(object sender, EventArgs e)
        {
            LoadBorrowedBooks();
        }

        private void LoadBorrowedBooks()
        {
            using (var conn = Connection.conn())
            {
                string query = @"SELECT borrowbook.borrow_id, books.book_name, borrowbook.borrow_date, borrowbook.due_date
                         FROM borrowbook
                         JOIN books ON borrowbook.book_id = books.book_id
                         WHERE borrowbook.borrower_id = @BorrowerId";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@BorrowerId", CurrentUserId);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dataGridView1.DataSource = table;
                AddReturnButtonColumn();
            }
        }

        private void AddReturnButtonColumn()
        {
            if (!dataGridView1.Columns.Contains("ReturnButton"))
            {
                DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
                {
                    Name = "ReturnButton",
                    HeaderText = "Action",
                    Text = "Return",
                    UseColumnTextForButtonValue = true
                };

                dataGridView1.Columns.Add(buttonColumn);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "ReturnButton" && e.RowIndex >= 0)
            {
                int borrowId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["borrow_id"].Value);

                ReturnBookAction(borrowId);
            }
        }

        private void ReturnBookAction(int borrowId)
        {
            using (var conn = Connection.conn())
            {
                conn.Open();

                string query = "UPDATE borrowbook SET return_date = @ReturnDate WHERE borrow_id = @BorrowId";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ReturnDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@BorrowId", borrowId);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Book returned successfully!");
            LoadBorrowedBooks(); // Refresh data
        }
    }
}


