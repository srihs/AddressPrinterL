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
using MahApps.Metro.Controls;

namespace AddressPrinter
{
    /// <summary>
    /// Interaction logic for CustomerSearch.xaml
    /// </summary>
    public partial class CustomerSearch : MetroWindow
    {
        DataTable dt = new DataTable();

        public CustomerSearch()
        {
            InitializeComponent();
            try
            {
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Customer Name", typeof(string));
                dt.Columns.Add("Address", typeof(string));

                dt.Columns.Add("Rep", typeof(string));
                dgSearhCustomer.ItemsSource = dt.DefaultView;
                txtCustomerName.Focus();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtCustomerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ///if (e.Key == Key.Enter)
                //{
                if (txtCustomerName.Text != string.Empty)
                {
                    dt.Rows.Clear();
                    System.Data.SQLite.SQLiteConnection dbConnection = new Common().OpenConnection();
                    string Sql = " Select * from Customer where Cus_CustomerName Like '%" + txtCustomerName.Text + "%'";
                    System.Data.SQLite.SQLiteCommand dbCommand = new System.Data.SQLite.SQLiteCommand(Sql, dbConnection);
                    dbCommand.CommandType = System.Data.CommandType.Text;
                    System.Data.SQLite.SQLiteDataReader dReader = dbCommand.ExecuteReader();
                    //// dbCommand.Dispose();
                    //  dbConnection.Dispose();

                    if (dReader.HasRows)
                    {
                        while (dReader.Read())
                        {
                            DataRow dRow = dt.NewRow();
                            dRow["Id"] = int.Parse(dReader[0].ToString());
                            dRow["Customer Name"] = (dReader[1]?.ToString() ?? "").ToString();
                            dRow["Address"] = (dReader[2]?.ToString() ?? "").ToString() + " " + (dReader[3]?.ToString() ?? "").ToString() + " " + (dReader[4]?.ToString() ?? "").ToString();
                            dRow["Rep"] = (dReader[8]?.ToString() ?? "").ToString();
                            dt.Rows.Add(dRow);
                        }

                        dgSearhCustomer.ItemsSource = dt.DefaultView;
                        dgSearhCustomer.Columns[0].Visibility = Visibility.Hidden;

                    }
                    else
                    {
                        MessageBox.Show("No records found", "Address Printer", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                    }
                }
                else
                {
                    dgSearhCustomer.ItemsSource = null;
                }
            }
            //}
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
                    int custId = (int)((DataRowView)dgSearhCustomer.SelectedItems[0])["Id"];
                    e.Handled = true;

                    searchCustomer(custId);

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

                int custId = (int)((DataRowView)dgSearhCustomer.SelectedItems[0])["Id"];

                searchCustomer(custId);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Address Printer");
            }
        }

        public void searchCustomer(int id)
        {
            Customer objCus = new Customer().findCustomer(id);
            CurierNote objCurierNote = new CurierNote(objCus);
            objCurierNote.Show();
            this.Close();
        }
    }
}
