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

namespace AddressPrinter
{
    /// <summary>
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : MetroWindow
    {
        #region -Private Variables-
        bool isValidForm = false;
        #endregion
        public AddCustomer()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                isValidForm = checkValidity();

                if (isValidForm)
                {
                    System.Data.SQLite.SQLiteConnection dbConnection = new Common().OpenConnection();
                    string Sql = "Insert into Customer (Cus_CustomerName,Cus_CustomerAddress1,Cus_CustomerAddress2,Cus_CustomerAddress3,"+
                        "Cus_CustomerAddress4,"+
                        "Cus_Phone1,"+
                        "Cus_Phone2,"+
                        "Cus_Rep)"+
                        "values('"+txtCustomerName.Text +"','"+txtAddress1.Text + "','" + txtAddress2.Text + "','" + txtAddress3.Text + "','" + txtAddress4.Text + "','" + txtPhone.Text + "','" + txtPhone2.Text + "','" + txtRep.Text + "')";

                    System.Data.SQLite.SQLiteCommand dbCommand = new System.Data.SQLite.SQLiteCommand(Sql, dbConnection);
                    dbCommand.CommandType = System.Data.CommandType.Text;
                    dbCommand.ExecuteNonQuery();
                    MessageBox.Show("Record Saved","Address Printer",MessageBoxButton.OKCancel,MessageBoxImage.Information);

                    txtCustomerName.SetValue(TextBoxHelper.WatermarkProperty, "Customer Name");
                    txtAddress1.SetValue(TextBoxHelper.WatermarkProperty, "Address line 1");
                    txtAddress2.SetValue(TextBoxHelper.WatermarkProperty, "Address line 2");
                    txtAddress3.SetValue(TextBoxHelper.WatermarkProperty, "Address line 3");
                    txtAddress4.SetValue(TextBoxHelper.WatermarkProperty, "Address line 4");
                    txtPhone.SetValue(TextBoxHelper.WatermarkProperty, "Telephone");
                    txtPhone2.SetValue(TextBoxHelper.WatermarkProperty, "Telephone 2");
                    txtFax.SetValue(TextBoxHelper.WatermarkProperty, "Fax");
                    txtRep.SetValue(TextBoxHelper.WatermarkProperty, "Rep");






                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Address Printer");
            }
        }

        private bool checkValidity()
        {
            isValidForm = true;

            if (txtCustomerName.Text == string.Empty)
            {
                txtCustomerName.BorderBrush = System.Windows.Media.Brushes.Red;
                txtCustomerName.SetValue(TextBoxHelper.WatermarkProperty, "Required Field");
                isValidForm = false;
            }


            if (txtAddress1.Text == string.Empty)
            {
                txtAddress1.BorderBrush = System.Windows.Media.Brushes.Red;
                txtAddress1.SetValue(TextBoxHelper.WatermarkProperty, "Required Field");
                isValidForm = false;

            }


            if (txtAddress2.Text == string.Empty)
            {
                txtAddress2.BorderBrush = System.Windows.Media.Brushes.Red;
                txtAddress2.SetValue(TextBoxHelper.WatermarkProperty, "Required Field");
                isValidForm = false;

            }


            return isValidForm;
        }
    }
}
