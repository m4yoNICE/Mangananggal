using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mangananggal.Forms.AdminForms.Reports
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            string query = @"SELECT 
                        o.order_id,
                        o.order_date,
                        u.user_firstname + ' ' + u.user_lastname AS customer_name,
                        b.book_name,
                        oi.quantity,
                        oi.price,
                        (oi.quantity * oi.price) AS total_line,
                        o.total_amount,
                        o.amount_paid,
                        o.change_due
                    FROM orders o
                    JOIN order_items oi ON o.order_id = oi.order_id
                    JOIN books b ON oi.product_id = b.book_id
                    JOIN users u ON o.user_id = u.user_id
                    ORDER BY o.order_date DESC";

            using (SqlConnection connection = Connections.Connection.conn())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "SalesReportTable");

                string reportPath = Path.Combine(Application.StartupPath, "Forms/AdminForms/Reports/SaleReport.rpt");

                if (!File.Exists(reportPath))
                {
                    MessageBox.Show("Report file not found!");
                    return;
                }
                else {

                    ReportDocument rpt = new ReportDocument();
                    rpt.Load(reportPath);
                    rpt.SetDataSource(ds.Tables["SalesReportTable"]);

                    crystalReportViewer1.ReportSource = rpt;
                    crystalReportViewer1.Refresh();

                    string exportPath = Path.Combine(Application.StartupPath, "Forms/AdminForms/Reports/SalesReport.pdf");
                    rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, exportPath);
                    MessageBox.Show("PDF exported to:\n" + exportPath);
                }

            }
        }
    }
}
