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
using static Mangananggal.Forms.UserForms.BuyBook;

namespace Mangananggal.Forms.AdminForms
{
    public partial class OrderTable : Form
    {
        public OrderTable()
        {
            InitializeComponent();
        }

        private void OrderTable_Load(object sender, EventArgs e)
        {
            LoadOrder();
            LoadOrderItem();    
        }

        private void LoadOrder()
        {
            using (var conn = Connection.conn())
            {
                string query = "SELECT order_id, user_id, order_date, total_amount FROM orders";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void LoadOrderItem()
        {
            using (var conn = Connection.conn())
            {
                string query = @"SELECT item_id, order_id, product_id, quantity, price FROM order_items;
";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }

}
