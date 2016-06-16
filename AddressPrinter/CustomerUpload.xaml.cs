using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using Excel;
using Microsoft.Win32;

namespace AddressPrinter
{
    /// <summary>
    /// Interaction logic for CustomerUpload.xaml
    /// </summary>
    public partial class CustomerUpload : Window
    {
        public CustomerUpload()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xlsx";
            openfile.Filter = "(.xlsx)|*.xlsx";
            //openfile.ShowDialog();

            var browsefile = openfile.ShowDialog();

            if (browsefile == true)
            {
                string filePath = openfile.FileName;

                
                DataTable dt = new DataTable();

                dt.Columns.Add("Customer Name", typeof(string));
                dt.Columns.Add("Address 1", typeof(string));
                dt.Columns.Add("Address 2", typeof(string));
                dt.Columns.Add("Address 3", typeof(string));
                dt.Columns.Add("Address 4", typeof(string));
                dt.Columns.Add("Phone", typeof(string));
                dt.Columns.Add("Phone 2", typeof(string));
                dt.Columns.Add("Fax", typeof(string));
                dt.Columns.Add("Rep", typeof(string));


                FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

                
                //1. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                //...
                //2. DataSet - The result of each spreadsheet will be created in the result.Tables
                DataSet result = excelReader.AsDataSet();
               

                //5. Data Reader methods
                while (excelReader.Read())
                {
                    DataRow dRow = dt.NewRow();
                    dRow["Customer Name"] = (excelReader[3]?.ToString() ?? "").ToString(); 
                    dRow["Address 1"] = (excelReader[4]?.ToString() ?? "").ToString();  
                    dRow["Address 2"] = (excelReader[5]?.ToString() ?? "").ToString();
                    dRow["Address 3"] = excelReader[6]?.ToString() ?? ""; //(excelReader[6].ToString() ?? "").ToString();
                    dRow["Address 4"] = (excelReader[7]?.ToString() ?? "").ToString();
                    dRow["Phone"] = (excelReader[0]?.ToString() ?? "").ToString();
                    dRow["Phone 2"] = (excelReader[1]?.ToString() ?? "").ToString();
                    dRow["Fax"] = (excelReader[2]?.ToString() ?? "").ToString();
                    dRow["Rep"] = (excelReader[8]?.ToString() ?? "").ToString();
                    dt.Rows.Add(dRow);


                }

                //6. Free resources (IExcelDataReader is IDisposable)
                excelReader.Close();


                dt.Rows.RemoveAt(0);


                dtGrid.ItemsSource = dt.DefaultView;

               

            }
        }
    }
}
