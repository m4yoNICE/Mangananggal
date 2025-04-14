using Mangananggal.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Mangananggal.Forms.UserForms
{
    public partial class BuyBook : Form
    {
        public static int currentUserId = 1;
        private List<OrderItem> selectedItems = new List<OrderItem>();

        public void getId(int userId)
        {
            currentUserId = userId;
        }

        public BuyBook()
        {
            InitializeComponent();
        }
        private void BookTable_Load(object sender, EventArgs e)
        {
            LoadBooks();
            
        }

        private void LoadBooks()
        {
            using (var conn = Connection.conn())
            {
                string query = "SELECT book_id, book_name, book_author, book_genre, book_price FROM books";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                AddQuantityColumn();
                AddAddToCartButtonColumn();
            }
        }

        private void AddQuantityColumn()
        {
            // Add a quantity column for user input
            if (!dataGridView1.Columns.Contains("Quantity"))
            {
                var qtyColumn = new DataGridViewTextBoxColumn
                {
                    Name = "Quantity",
                    HeaderText = "Quantity",
                    ValueType = typeof(int)
                };
                dataGridView1.Columns.Add(qtyColumn);
            }
        }

        private void AddAddToCartButtonColumn()
        {
            // Add "Add to Cart" button column
            if (!dataGridView1.Columns.Contains("AddToCartButton"))
            {
                DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
                {
                    Name = "AddToCartButton",
                    HeaderText = "Action",
                    Text = "Add to Cart",
                    UseColumnTextForButtonValue = true
                };
                dataGridView1.Columns.Add(buttonColumn);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "AddToCartButton" && e.RowIndex >= 0)
            {
                int bookId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["book_id"].Value);
                int quantity = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Quantity"].Value);

                if (quantity > 0)
                {
                    AddToCart(bookId, quantity);
                }
                else
                {
                    MessageBox.Show("Please enter a valid quantity.");
                }
            }
        }

        private void AddToCart(int bookId, int quantity)
        {
            // Check if the book is already in the cart
            var existingItem = selectedItems.Find(item => item.ProductId == bookId);
            if (existingItem != null)
            {
                // Update the quantity of the existing item
                existingItem.Quantity += quantity;
            }
            else
            {
                // Add new item to cart
                selectedItems.Add(new OrderItem
                {
                    ProductId = bookId,
                    Quantity = quantity
                });
            }

            MessageBox.Show($"Book ID: {bookId} added to cart.");
        }
        public class OrderItem
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Your cart is empty. Please add items to your cart first.");
                return;
            }

            decimal totalAmount = 0;
            foreach (var item in selectedItems)
            {
                totalAmount += item.Quantity * item.Price;
            }

            using (var conn = Connection.conn())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Insert a record into the orders table
                        string orderQuery = "INSERT INTO orders (user_id, order_date, total_amount) OUTPUT INSERTED.order_id VALUES (@UserId, @OrderDate, @Total)";
                        SqlCommand orderCmd = new SqlCommand(orderQuery, conn, transaction);
                        orderCmd.Parameters.AddWithValue("@UserId", currentUserId);
                        orderCmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                        orderCmd.Parameters.AddWithValue("@Total", totalAmount);
                        int orderId = (int)orderCmd.ExecuteScalar();

                        // Insert each item into the order_items table
                        foreach (var item in selectedItems)
                        {
                            string itemQuery = "INSERT INTO order_items (order_id, product_id, quantity, price) VALUES (@OrderId, @ProductId, @Qty, @Price)";
                            SqlCommand itemCmd = new SqlCommand(itemQuery, conn, transaction);
                            itemCmd.Parameters.AddWithValue("@OrderId", orderId);
                            itemCmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                            itemCmd.Parameters.AddWithValue("@Qty", item.Quantity);
                            itemCmd.Parameters.AddWithValue("@Price", item.Price);
                            itemCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Order placed successfully!");
                        selectedItems.Clear(); // Clear cart after placing the order
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
        }
    }
}


