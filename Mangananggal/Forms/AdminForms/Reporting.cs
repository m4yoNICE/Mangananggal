using System;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mangananggal.Forms.AdminForms
{
    public partial class Reporting : Form
    {
        public Reporting()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            ReportDocument myReportDocument = new ReportDocument();
            myReportDocument.Load(@"C:\Codes\Appsdev (C#)\Mangananggal\Mangananggal\Forms\AdminForms\ReportTransaction.rpt");
            crystalReportViewer1.ReportSource = myReportDocument;
        }
    }
}
