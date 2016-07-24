using MahApps.Metro.Controls;
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

namespace AddressPrinter
{
    /// <summary>
    /// Interaction logic for SearchInvoice.xaml
    /// </summary>
    public partial class SearchInvoice : MetroWindow
    {

        DataTable dt = new DataTable();


        public SearchInvoice()
        {
            InitializeComponent();
            dt.Columns.Add("To", typeof(string));
            dt.Columns.Add("Invoice No", typeof(string));
            dt.Columns.Add("No of Boxes", typeof(string));
            dt.Columns.Add("From", typeof(string));
            dt.Columns.Add("Weight", typeof(string));
            dt.Columns.Add("Rep", typeof(string));
            dt.Columns.Add("ReferenceNo", typeof(string));
            dgSearhCustomer.ItemsSource = dt.DefaultView;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dt.Rows.Clear();


                string Sql = "Select * from Customer_Currier_Bills";

                if (txtCustomerName.Text != string.Empty && txtInvoiceNumber.Text != string.Empty)
                {
                    Sql += "  where InvoiceNo Like '%" + txtInvoiceNumber.Text + "%' and Address1  Like '%" + txtCustomerName.Text + "%'";
                }
                else
                {
                    if (txtInvoiceNumber.Text != string.Empty)
                        Sql += " where InvoiceNo Like '%" + txtInvoiceNumber.Text + "%'";
                    else
                    {
                        if (txtCustomerName.Text != string.Empty)
                            Sql += " where Address1  Like '%" + txtCustomerName.Text + "%'";

                    }
                }

                System.Data.SQLite.SQLiteConnection dbConnection = new Common().OpenConnection();
                System.Data.SQLite.SQLiteCommand dbCommand = new System.Data.SQLite.SQLiteCommand(Sql, dbConnection);
                dbCommand.CommandType = System.Data.CommandType.Text;
                System.Data.SQLite.SQLiteDataReader dReader = dbCommand.ExecuteReader();

                if (dReader.HasRows)
                {
                    while (dReader.Read())
                    {
                        DataRow dRow = dt.NewRow();
                        dRow["To"] = (dReader[1]?.ToString() ?? "").ToString();
                        dRow["Invoice No"] = (dReader[2]?.ToString() ?? "").ToString();
                        dRow["No of Boxes"] = (dReader[3]?.ToString() ?? "").ToString();
                        dRow["From"] = (dReader[4]?.ToString() ?? "").ToString();
                        dRow["Weight"] = (dReader[5]?.ToString() ?? "").ToString();
                        dRow["Rep"] = (dReader[6]?.ToString() ?? "").ToString();
                        dRow["ReferenceNo"] = (dReader[7]?.ToString() ?? "").ToString();

                        dt.Rows.Add(dRow);
                    }

                    dgSearhCustomer.ItemsSource = dt.DefaultView;

                }
                else
                {
                    MessageBox.Show("No records found", "Address Printer", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Address Printer");
            }
        }

        private void dgSearhCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
                //if (row != null)
                DataRowView drv = (DataRowView)dgSearhCustomer.SelectedItem;
                if (drv != null)
                    BindData(drv);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Address Printer");

            }
        }

        private void BindData(DataRowView row)
        {
            try
            {
                DataSetCurierNote objDataset = new DataSetCurierNote();

                int BoxCount = int.Parse(row["No of Boxes"].ToString());
                for (int i = 0; i < BoxCount; i++)
                {
                    DataRow dRow = objDataset.Tables["CurierNote"].NewRow();
                    dRow["Address1"] = row[0].ToString();
                    dRow["FromAddress"] = row[3].ToString();
                    dRow["InvoiceNo"] = row[1].ToString();
                    dRow["NoBoxes"] = row[2].ToString();
                    dRow["Weight"] = row[4].ToString();
                    dRow["Rep"] = row[5].ToString();
                    dRow["BoxCount"] = "Box " + (i + 1 )+ " of " + row[2].ToString();
                    dRow["ReferenceNo"] = row[6].ToString();
                    objDataset.Tables["CurierNote"].Rows.Add(dRow);

                    
                }

                AddressNote objAddressNote = new AddressNote(objDataset);
                objAddressNote.Show();
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Address Printer");
            }
        }


        private void dgSearhCustomer_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    //BindData();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Address Printer");
            }
        }
    }
}
