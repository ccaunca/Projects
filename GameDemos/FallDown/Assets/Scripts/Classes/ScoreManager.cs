using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace FallDownDemo
{
    public class ScoreManager
    {
        private static ScoreManager instance;
        private DatabaseConnection databaseConnection;
        private int maxScores;
        private ScoreManager()
        {
            databaseConnection = new DatabaseConnection();
            maxScores = 10;
        }
        public static ScoreManager GetInstance()
        {
            if (instance == null)
            {
                instance = new ScoreManager();
            }
            return instance;
        }
        public IList<int> GetScores()
        {
            List<int> scores = new List<int>();
            string connString = "Data Source=P3NWSH12SQL-v01.shr.prod.phx3.secureserver.net;Initial Catalog=FallDown;User ID = FallDown;Password=F@llD0wnDem0";

            try
            {
                using (IDbConnection sqlConn = new SqlConnection(connString))
                {
                    using (IDbCommand dbCmd = sqlConn.CreateCommand())
                    {
                        dbCmd.CommandType = CommandType.Text;
                        dbCmd.CommandText = "SELECT TOP 10 * FROM HighScores";
                        sqlConn.Open();
                        var result = dbCmd.ExecuteScalar();
                        sqlConn.Close();
                    }
                }
                return scores;
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }
    }
}