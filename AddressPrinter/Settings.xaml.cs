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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {
        DataTable dt = new DataTable();

        public Settings()
        {
            InitializeComponent();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Address", typeof(string));

            dataGridAddress.ItemsSource = dt.DefaultView;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dataGridAddress.Columns[0].Visibility = Visibility.Hidden;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Boolean isDefault = false;

                if (checkBoxIsDefult.IsChecked ?? true)
                    isDefault = true;
                else
                    isDefault = false;


                System.Data.SQLite.SQLiteConnection dbConnection = new Common().OpenConnection();
                string Sql = "Insert into Settings_address (Name,Address_line1,Address_line2,Address_line3,Address_line4," +
                    "Is_default)" +
                    "values('" + txtCustomerName.Text + "','" + txtAddress1.Text + "','" + txtAddress2.Text + "','" + txtAddress3.Text + "','" + txtAddress4.Text + "','" + isDefault + "')";

                System.Data.SQLite.SQLiteCommand dbCommand = new System.Data.SQLite.SQLiteCommand(Sql, dbConnection);
                dbCommand.CommandType = System.Data.CommandType.Text;
                dbCommand.ExecuteNonQuery();


                DataRow dRow = dt.NewRow();
                dRow["Name"] = txtCustomerName.Text.ToString();
                dRow["Address"] = (txtAddress1.Text?.ToString() ?? "").ToString() + " " + (txtAddress3.Text?.ToString() ?? "").ToString() + " " + (txtAddress4.Text?.ToString() ?? "").ToString();
                dt.Rows.Add(dRow);

                dataGridAddress.ItemsSource = dt.DefaultView;
                dataGridAddress.Columns[0].Visibility = Visibility.Hidden;

                foreach (Control txtBox in gdCreate.Children)
                {
                    if (txtBox.GetType() == typeof(TextBox))
                    {
                        ((TextBox)txtBox).Text = "";
                    }
                }


                txtCustomerName.SetValue(TextBoxHelper.WatermarkProperty, "Customer Name");
                txtAddress1.SetValue(TextBoxHelper.WatermarkProperty, "Address line 1");
                txtAddress2.SetValue(TextBoxHelper.WatermarkProperty, "Address line 2");
                txtAddress3.SetValue(TextBoxHelper.WatermarkProperty, "Address line 3");
                txtAddress4.SetValue(TextBoxHelper.WatermarkProperty, "Address line 4");
                dataGridAddress.ItemsSource = null;
                checkBoxIsDefult.IsChecked = false;
                MessageBox.Show("Record Saved", "Address Printer", MessageBoxButton.OKCancel, MessageBoxImage.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
