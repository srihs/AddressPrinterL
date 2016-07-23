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
        string businessAddress;
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
            businessAddress = getBusinessAddress();
            txtInvoiceNumber.Focus();
        }

        private string getBusinessAddress()
        {
            string Srt = string.Empty;

            try
            {
                System.Data.SQLite.SQLiteConnection dbConnection = new Common().OpenConnection();

                string sql = "Select Name,Address_line1,Address_line2,Address_line3,Address_line4,Phone1,Phone2 from Settings_address where Is_default=1";
                System.Data.SQLite.SQLiteCommand dbCommand = new System.Data.SQLite.SQLiteCommand(sql, dbConnection);
                dbCommand.CommandType = System.Data.CommandType.Text;
                System.Data.SQLite.SQLiteDataReader dReader = dbCommand.ExecuteReader();

                if (dReader.HasRows)
                {
                    while (dReader.Read())
                    {
                        Srt = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}", dReader[0].ToString(), dReader[1].ToString(), dReader[2].ToString(), dReader[3].ToString(), dReader[4].ToString(), dReader[5].ToString(), dReader[6].ToString()).Replace("\n",
                            Environment.NewLine);

                    }

                }



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Address Printer");

            }

            return Srt;

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            CustomerSearch objSearch = new CustomerSearch();
            objSearch.ShowDialog();
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
                //dRow["CustomerName"] = objCustomer.customerName;
                dRow["Address1"] = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}", objCustomer.customerName, objCustomer.address1, objCustomer.address2, objCustomer.address3, objCustomer.address4, objCustomer.phone, objCustomer.phone2).Replace("\n",
                                                         Environment.NewLine);
                dRow["FromAddress"] = businessAddress;
                //dRow["Address2"] = objCustomer.address2;
                //dRow["Address3"] = objCustomer.address3;
                //dRow["Address4"] = objCustomer.address4;
                //dRow["TelephoneNo"] = objCustomer.phone +" / "+ objCustomer.phone2;
                dRow["InvoiceNo"] = "Inv: " + txtInvoiceNumber.Text;
                dRow["NoBoxes"] = "Box Count: " + txtNoOfBoxes.Text;
                dRow["Weight"] = txtWeight.Text + " Kgs";
                objDataset.Tables["CurierNote"].Rows.Add(dRow);


                PrinterSettings settings = new PrinterSettings();

                rtpCurierNote report = new rtpCurierNote();
                report.PrintOptions.PrinterName = settings.PrinterName;
                report.SetDataSource(objDataset.Tables["CurierNote"]);
                report.VerifyDatabase();



                report.PrintToPrinter(1, false, 0, 0);

                objDataset.Dispose();
                dRow = null;

                objDataset = new DataSetCurierNote();
                for (int i = 0; i < int.Parse(txtNoOfBoxes.Text); i++)
                {
                     dRow = objDataset.Tables["CurierNote"].NewRow();

                    dRow["Address1"] = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}", objCustomer.customerName, objCustomer.address1, objCustomer.address2, objCustomer.address3, objCustomer.address4, objCustomer.phone, objCustomer.phone2).Replace("\n",
                                                        Environment.NewLine);
                    dRow["FromAddress"] = businessAddress;
                    dRow["InvoiceNo"] = "Inv: " + txtInvoiceNumber.Text;
                    dRow["NoBoxes"] = "Box Count: " + txtNoOfBoxes.Text;
                    dRow["Weight"] = txtWeight.Text + " Kgs";
                    dRow["BoxCount"] = i +"/"+ txtNoOfBoxes.Text;
                    dRow["Rep"] =  objCustomer.rep;
                    dRow["CusId"] = objCustomer.id;

                    objDataset.Tables["CurierNote"].Rows.Add(dRow);

                }

                AddressNote objAddress = new AddressNote(objDataset);
                objAddress.Show();
                this.Close();



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
