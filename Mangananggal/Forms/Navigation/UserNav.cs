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
        public BuyBook buybook = new BuyBook();
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

            buybook.TopLevel = false;
            buybook.FormBorderStyle = FormBorderStyle.None;
            panelShow.Controls.Add(buybook);
            buybook.Size = panelShow.Size;
            buybook.Location = new Point(0, 0);
            buybook.getId(userid);
            buybook.Show();
            
        }

        private void Borrowlbl_Click(object sender, EventArgs e)
        {
            pnlBorrow_Click(sender, e);
        }

        private void pnlReturnBook_Click(object sender, EventArgs e)
        {
 
        }

        private void lblReturnBook_Click(object sender, EventArgs e)
        {
            pnlReturnBook_Click(sender, e);
        }

        private void panelShow_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
