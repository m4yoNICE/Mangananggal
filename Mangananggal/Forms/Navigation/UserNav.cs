using Mangananggal.Forms.AdminForms;
using Mangananggal.Forms.UserForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mangananggal
{
    public partial class UserNav : Form
    {
        public static string welcomestring;
        public static int userid;
        public BorrowBook borrow = new BorrowBook();
        public ReturnBook returnbook = new ReturnBook();
        public UserNav()
        {
            InitializeComponent();
            
        }
        private void UserNav_Load(object sender, EventArgs e)
        {
            label1.Text = welcomestring;
        }

        public static void Welcome(string username, int id)
        {
            welcomestring = "Welcome "+ username;
            userid = id;
        }

        private void pnlBorrow_Click(object sender, EventArgs e)
        {
            panelShow.Controls.Clear();

            borrow.TopLevel = false;
            borrow.FormBorderStyle = FormBorderStyle.None;
            panelShow.Controls.Add(borrow);
            borrow.Size = panelShow.Size;
            borrow.Location = new Point(0, 0);
            borrow.getId(userid);
            borrow.Show();
            
        }

        private void Borrowlbl_Click(object sender, EventArgs e)
        {
            pnlBorrow_Click(sender, e);
        }

        private void pnlReturnBook_Click(object sender, EventArgs e)
        {
            panelShow.Controls.Clear();

            returnbook.TopLevel = false;
            returnbook.FormBorderStyle = FormBorderStyle.None;
            panelShow.Controls.Add(returnbook);
            returnbook.Size = panelShow.Size;
            returnbook.Location = new Point(0, 0);
            returnbook.getId(userid);
            returnbook.Show();
        }

        private void lblReturnBook_Click(object sender, EventArgs e)
        {
            pnlReturnBook_Click(sender, e);
        }
    }
}
