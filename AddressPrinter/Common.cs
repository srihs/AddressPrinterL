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
}
