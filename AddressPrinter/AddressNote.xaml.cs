using CrystalDecisions.CrystalReports.Engine;
using MahApps.Metro.Controls;
using SAPBusinessObjects.WPF.Viewer;
using System;
using System.Collections.Generic;
using System.Data;
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
using Microsoft.Reporting.WinForms;

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
                InitializeComponent();
                this.objDataset = objDataset;

               
                AddressLabel report = new AddressLabel();
                report.SetDataSource(objDataset.Tables["CurierNote"]);
                report.VerifyDatabase();
                report.Refresh();

                ViewerCore view = rtpViwer.ViewerCore;
                view.ReportSource = report;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Address Printer");
            }
        }
    }
}
