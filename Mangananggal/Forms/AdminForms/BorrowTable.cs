using Mangananggal.Connections;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Mangananggal.Forms.AdminForms
{
    public partial class BorrowTable : Form
    {
        public BorrowTable()
        {
            InitializeComponent();
            
        }

        private void BorrowTable_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            using (var conn = Connection.conn())
            {
                // Define the SQL query to fetch users from the Users table
                string query = "SELECT borrower_id, user_id FROM borrower";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

       
    }
}

