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
using MahApps.Metro.Controls;
using System.Text.RegularExpressions;
using System.Data;
using System.Drawing.Printing;

namespace AddressPrinter
{
    /// <summary>
    /// Interaction logic for CurierNote.xaml
    /// </summary>
    public partial class CurierNote : MetroWindow
    {
        Customer objCustomer;

        public CurierNote()
        {
            InitializeComponent();
        }

        public CurierNote(Customer customer)
        {
            InitializeComponent();
            txtCustomerName.Text = customer.customerName;
            //string.Format("Test: {0}/{1}", val1, val2);
            txtCustomerAddress.Text = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}", customer.address1, customer.address2, customer.address3, customer.address4, customer.phone, customer.phone2, customer.rep).Replace("\n",
                                                         Environment.NewLine);
            objCustomer = customer;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            CustomerSearch objSearch = new CustomerSearch();
            objSearch.Show();
            this.Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtInvoiceNumber.Text == string.Empty)
                {
                    txtInvoiceNumber.BorderBrush = System.Windows.Media.Brushes.Red;
                    txtInvoiceNumber.SetValue(TextBoxHelper.WatermarkProperty, "Required Field");
                    return;
                }

                if (txtNoOfBoxes.Text == string.Empty)
                {
                    txtNoOfBoxes.BorderBrush = System.Windows.Media.Brushes.Red;
                    txtNoOfBoxes.SetValue(TextBoxHelper.WatermarkProperty, "Required Field");
                    return;
                }

                DataSetCurierNote objDataset = new DataSetCurierNote();
                DataRow dRow = objDataset.Tables["CurierNote"].NewRow();
                dRow["CustomerName"] = objCustomer.customerName;
                dRow["Address1"] = objCustomer.address1;
                dRow["Address2"] = objCustomer.address2;
                dRow["Address3"] = objCustomer.address3;
                dRow["Address4"] = objCustomer.address4;
                dRow["TelephoneNo"] = objCustomer.phone;
                dRow["InvoiceNo"] = txtInvoiceNumber.Text;
                dRow["NoBoxes"] = txtNoOfBoxes.Text;
                objDataset.Tables["CurierNote"].Rows.Add(dRow);
                

                PrinterSettings settings = new PrinterSettings();

                rtpCurierNote report = new rtpCurierNote();
                report.PrintOptions.PrinterName = settings.PrinterName;
                report.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
                report.SetDataSource(objDataset.Tables["CurierNote"]);
                report.VerifyDatabase();



                report.PrintToPrinter(1, false, 0, 0);

                

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Address Printer");
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {


        }
    }
}
