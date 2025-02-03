using Mangananggal.Connections;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Mangananggal.Forms.AdminForms
{
    public partial class ReturnerTable : Form
    {
        public ReturnerTable()
        {
            InitializeComponent();
            
        }

       

        private void LoadUsers()
        {
            using (var conn = Connection.conn())
            {
                // Define the SQL query to fetch users from the Users table
                string query = "SELECT * FROM returnbook";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void ReturnerTable_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }
    }
}

