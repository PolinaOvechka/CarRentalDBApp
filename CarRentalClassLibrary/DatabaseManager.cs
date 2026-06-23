using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;

namespace CarRentalClassLibrary
{
    public class DatabaseManager
    {
        private OleDbConnection connection;
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Car_DB_practice.accdb";

        public bool Connect()
        {
            try
            {
                if (connection == null)
                {
                    connection = new OleDbConnection(connectionString);
                }

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Disconnect()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
        }

        public DataTable GetTable(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return null;
            }

            DataTable dataTable = new DataTable();
            try
            {
                string query = $"SELECT * FROM [{tableName}]";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return dataTable;
        }

        public DataTable ExecuteQuery(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return null;
            }

            DataTable dataTable = new DataTable();
            try
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return dataTable;
        }

        public int ExecuteNonQuery(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return 0;
            }

            try
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public OleDbConnection GetConnection()
        {
            return connection;
        }
    }
}
