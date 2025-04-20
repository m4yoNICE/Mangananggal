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
            string priceQuery = $"SELECT book_price FROM books WHERE book_id = {bookId}";
            decimal price = DBHelper.DBHelper.ExecuteScalarQuery(priceQuery);

            var existingItem = selectedItems.Find(item => item.ProductId == bookId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                selectedItems.Add(new OrderItem
                {
                    ProductId = bookId,
                    Quantity = quantity,
                    Price = price
                });
            }

            UpdateTotalAmount();

            MessageBox.Show($"Book ID: {bookId} added to cart at ₱{price} each.");
        }

        private void UpdateTotalAmount()
        {
            decimal totalAmount = 0;
            foreach (var item in selectedItems)
            {
                totalAmount += item.Quantity * item.Price;
            }
            // Update the txtTotal textbox with the new total amount
            txtTotal.Text = totalAmount.ToString("F2");
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

            // Parse the amount paid by the user
            if (!decimal.TryParse(txtAmount.Text, out decimal amountPaid))
            {
                MessageBox.Show("Please enter a valid amount paid.");
                return;
            }

            decimal totalAmount = 0;
            foreach (var item in selectedItems)
            {
                totalAmount += item.Quantity * item.Price;
            }

            decimal changeDue = amountPaid - totalAmount;

            if (amountPaid < totalAmount)
            {
                MessageBox.Show("Insufficient amount paid.");
                return;
            }

            using (var conn = Connection.conn())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Insert order record with new fields
                        string orderQuery = @"
                    INSERT INTO orders (
                        user_id, order_date, total_amount,
                        amount_paid, change_due
                    )
                    OUTPUT INSERTED.order_id
                    VALUES (
                        @UserId, @OrderDate, @Total,
                        @Paid, @Change
                    )";



                        SqlCommand orderCmd = new SqlCommand(orderQuery, conn, transaction);
                        orderCmd.Parameters.AddWithValue("@UserId", currentUserId);
                        orderCmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                        orderCmd.Parameters.AddWithValue("@Total", totalAmount);
                        orderCmd.Parameters.AddWithValue("@Paid", amountPaid);
                        orderCmd.Parameters.AddWithValue("@Change", changeDue);

                        int orderId = (int)orderCmd.ExecuteScalar();

                        // Insert order items
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
                        MessageBox.Show($"Order placed successfully! Change: ₱{changeDue:F2}");
                        selectedItems.Clear();
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


