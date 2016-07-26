using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressPrinter
{
    public class Common
    {
        public System.Data.SQLite.SQLiteConnection OpenConnection()
        {
            try
            {
                System.Data.SQLite.SQLiteConnection dbConnection = new System.Data.SQLite.SQLiteConnection("Data Source=AddressDB.sqlite;Version=3;");

                if (dbConnection.State == System.Data.ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                else
                    dbConnection.Close();

                return dbConnection;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }

    public class Customer
    {
        public Customer() { }
        public Customer findCustomer(int id)
        {
            try
            {
                Customer objCus = new Customer();


                System.Data.SQLite.SQLiteConnection dbConnection = new Common().OpenConnection();
                string Sql = " Select * from Customer where  id =" + id + "";
                System.Data.SQLite.SQLiteCommand dbCommand = new System.Data.SQLite.SQLiteCommand(Sql, dbConnection);
                dbCommand.CommandType = System.Data.CommandType.Text;
                System.Data.SQLite.SQLiteDataReader dReader = dbCommand.ExecuteReader();

                if (dReader.HasRows)
                {
                    while (dReader.Read())
                    {
                        objCus.id = int.Parse(dReader[0].ToString());
                        objCus.customerName = (dReader[1]?.ToString() ?? "").ToString();

                        objCus.address1 = (dReader[2]?.ToString() ?? "").ToString();
                        objCus.address2 = (dReader[3]?.ToString() ?? "").ToString();
                        objCus.address3 = (dReader[4]?.ToString() ?? "").ToString();
                        objCus.address4 = (dReader[5]?.ToString() ?? "").ToString();
                        objCus.phone = (dReader[6]?.ToString() ?? "").ToString();
                        objCus.phone2 = (dReader[7]?.ToString() ?? "").ToString();
                        objCus.fax = (dReader[9]?.ToString() ?? "").ToString();
                        objCus.rep = (dReader[8]?.ToString() ?? "").ToString();

                    }
                }

                return objCus;
            }

            catch (Exception)
            {

                throw;
            }
        }
        public int id { get; set; }
        public string customerName { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string address4 { get; set; }
        public string phone { get; set; }
        public string phone2 { get; set; }
        public string fax { get; set; }
        public string rep { get; set; }




    }
}
