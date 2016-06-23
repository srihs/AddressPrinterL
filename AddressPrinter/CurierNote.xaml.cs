﻿using System;
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
    /// Interaction logic for CurierNote.xaml
    /// </summary>
    public partial class CurierNote : MetroWindow
    {
        public CurierNote()
        {
            InitializeComponent();
        }

        public CurierNote(Customer customer)
        {
            InitializeComponent();
            txtCustomerName.Text = customer.customerName;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            CustomerSearch objSearch = new CustomerSearch();
            objSearch.Show();
            this.Close();
        }
    }
}
