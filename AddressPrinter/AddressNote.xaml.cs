using CrystalDecisions.CrystalReports.Engine;
using MahApps.Metro.Controls;
using SAPBusinessObjects.WPF.Viewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AddressPrinter
{
    /// <summary>
    /// Interaction logic for AddressNote.xaml
    /// </summary>
    public partial class AddressNote : MetroWindow
    {
        private DataSetCurierNote objDataset;

        public AddressNote()
        {
            InitializeComponent();
        }

        public AddressNote(DataSetCurierNote objDataset)
        {
            try
            {
                this.objDataset = objDataset;
                rtpViwer = new CrystalReportsViewer();
                ReportDocument reportDocument = new ReportDocument();
                //report.VerifyDatabase();
                //  rtpViwer.ViewerCore.ReportSource = report;
                string path = System.AppDomain.CurrentDomain.BaseDirectory;
                reportDocument.Load(System.AppDomain.CurrentDomain.BaseDirectory + "AddressLabel.rpt");//C#
                reportDocument.SetDataSource(objDataset.Tables["CurierNote"]);

                ViewerCore view = rtpViwer.ViewerCore;
                view.ReportSource = reportDocument;
                //view.RefreshReport();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Address Printer");
            }
        }
    }
}
