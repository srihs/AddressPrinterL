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
