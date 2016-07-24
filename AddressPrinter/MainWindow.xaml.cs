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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace AddressPrinter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddCustomer objCustomer = new AddCustomer();
                objCustomer.ShowDialog();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void BtnEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddCustomer objCustomer = new AddCustomer("EDIT");
                objCustomer.ShowDialog();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void BtnUploadCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerUpload objCustomerLoad = new CustomerUpload();
                objCustomerLoad.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void BtnCourierNote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerSearch objCustomerSearch = new CustomerSearch();
                objCustomerSearch.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Settings objSettings = new Settings();
                objSettings.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void BtnPrintAddress_Click(object sender, RoutedEventArgs e)
        {
            AddressNote objAddress = new AddressNote();
            objAddress.Show();
            this.Close();
        }
    }
}
