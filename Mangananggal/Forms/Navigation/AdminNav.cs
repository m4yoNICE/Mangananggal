using Mangananggal.Forms.AdminForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mangananggal.Forms.Navigation
{
    public partial class AdminNav : Form
    {
        public UserTable usertable = new UserTable();
        public BookTable bookTable = new BookTable();
        public OrderTable orderTable = new OrderTable();
        public Reporting reporting = new Reporting();

        public AdminNav()
        {
            InitializeComponent();
        }

        private void pnlUser_Click(object sender, EventArgs e)
        {
            panelShow.Controls.Clear();

            usertable.TopLevel = false;
            usertable.FormBorderStyle = FormBorderStyle.None;
            panelShow.Controls.Add(usertable);
            usertable.Size = panelShow.Size;
            usertable.Location = new Point(0, 0);
            usertable.Show();
        }


        private void userlbl_Click(object sender, EventArgs e)
        {
            pnlUser_Click(sender, e);
        }

        private void pnlHome_Click(object sender, EventArgs e)
        {
            panelShow.Controls.Clear();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pnlHome_Click(sender, e);
        }

        private void lblBooks_Click(object sender, EventArgs e)
        {
            panel3_Click(sender, e);
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            panelShow.Controls.Clear();

            bookTable.TopLevel = false;
            bookTable.FormBorderStyle = FormBorderStyle.None;
            panelShow.Controls.Add(bookTable);
            bookTable.Size = panelShow.Size;
            bookTable.Location = new Point(0, 0);
            bookTable.Show();
        }
        private void pnlBorrow_Click(object sender, EventArgs e)
        {
            panelShow.Controls.Clear();

            orderTable.TopLevel = false;
            orderTable.FormBorderStyle = FormBorderStyle.None;
            panelShow.Controls.Add(orderTable);
            orderTable.Size = panelShow.Size;
            orderTable.Location = new Point(0, 0);
            orderTable.Show();
        }

        private void lblBorrow_Click(object sender, EventArgs e)
        {
            pnlBorrow_Click(sender, e);
        }

    
        private void pnlReport_Click(object sender, EventArgs e)
        {
            panelShow.Controls.Clear();

            reporting.TopLevel = false;
            reporting.FormBorderStyle = FormBorderStyle.None;
            panelShow.Controls.Add(reporting);
            reporting.Size = panelShow.Size;
            reporting.Location = new Point(0, 0);
            reporting.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblReport_Click(object sender, EventArgs e)
        {
            pnlReport_Click(sender, e);
        }

        private void pnlReport_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelShow_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
