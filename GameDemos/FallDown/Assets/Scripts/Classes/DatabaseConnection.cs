using System.Data.SqlClient;

namespace FallDownDemo
{
    public class DatabaseConnection
    {
        private SqlConnection sqlConnection;
        public string UserName
        {
            get { return "FallDown"; }
        }
        public string Password
        {   // we'll need to abstract this somehow eventually...
            get { return "F@llD0wnDem0"; }
        }
        public string DatabaseName
        {
            get { return "FallDown"; }
        }
        public string ServerName
        {
            get { return "P3NWSH12SQL-v01.shr.prod.phx3.secureserver.net"; }
        }
        public string ConnectionTimeout
        {
            get { return "30"; }
        }
        public SqlConnection SqlConnection
        {
            get { return sqlConnection; }
        }
        public DatabaseConnection()
        {
            sqlConnection = GetConnection();
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(string.Format("user id={0};password={1};server={2};Trusted_Connection=yes;database={3};connection_timeout={4}",
                                                    UserName, Password, ServerName, DatabaseName, ConnectionTimeout));
        }
        public void OpenConnection()
        {
            sqlConnection.Open();
        }
        public void CloseConnection()
        {
            sqlConnection.Close();
        }
    }
}