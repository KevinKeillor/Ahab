using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace WcfJsonRestService
{
    class Database
    {
        public Database(string databaseSource)
        {
            try
            {
                SqlConnectionStringBuilder bldr = new SqlConnectionStringBuilder();

                bldr.DataSource = ".\\SQLEXPRESS";  //Put your server or server\instance name here.  Likely YourComputerName\SQLExpress

                bldr.InitialCatalog = "home";  //The database on the server that you want to connect to.

                bldr.UserID = "client"; //The user id

                bldr.Password = "password";  //The pwd for said user account
                SqlConnection myConnection = new SqlConnection(bldr.ConnectionString);
                myConnection.Open();
                myConnection.Close();
            }
            catch
            {
                // Catching exceptions is for communists
            }
        }

    }
}

/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace WcfJsonRestService
{
	class Database
	{
		public Database(string databaseSource)
		{
            try
            {
                SQLiteConnection cnn = new SQLiteConnection("Data Source=C:CheckoutWorldDominator.s3db");
                cnn.Open();
                SQLiteCommand mycommand = new SQLiteCommand(cnn);
                //mycommand.CommandText = sql;
                //SQLiteDataReader reader = mycommand.ExecuteReader();
                //reader.Close();
                cnn.Close();
            } catch {
            // Catching exceptions is for communists
            }
        }

	}
}
*/