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
    /// Interaction logic for SaveCurierNote.xaml
    /// </summary>
    public partial class SaveCurierNote : MetroWindow
    {
        private DataSetCurierNote objDataset;

        public SaveCurierNote(DataSetCurierNote objDataset)
        {
            InitializeComponent();
            this.objDataset = objDataset;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRow dRow = null;


                if (objDataset != null)
                {
                    for (int i = 0; i < objDataset.Tables[0].Rows.Count; i++)
                    {
                        dRow = objDataset.Tables[0].Rows[i];
                        dRow["ReferenceNo"] = txtReference.Text;
                    }

                    string Sql = "Insert into Customer_Currier_Bills(`Address1`,`InvoiceNo`,`NoBoxes`,`FromAddress`,`Weight`,`Rep`,`ReferenceNo`)" +
                        "values('" + dRow["Address1"] + "','" + dRow["InvoiceNo"] + "','" + dRow["NoBoxes"] + "','" + dRow["FromAddress"] + "','" + dRow["Weight"] + "','" + dRow["Rep"] + "','" + dRow["ReferenceNo"] + "')";



                    System.Data.SQLite.SQLiteConnection dbConnection = new Common().OpenConnection();
                    System.Data.SQLite.SQLiteCommand dbCommand = new System.Data.SQLite.SQLiteCommand(Sql, dbConnection);
                    dbCommand.CommandType = System.Data.CommandType.Text;
                    dbCommand.ExecuteNonQuery();

                    MessageBox.Show("Record Saved", "Address Printer", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    this.Close();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Address Printer");

            }
        }
    }
}
