using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void StoredProcedure5()
    {
        using (SqlConnection Connection = new SqlConnection("context connection = true"))
        {
            Connection.Open();

            SqlCommand Command = new SqlCommand(@"
                    IF OBJECT_ID('dbo.Users1') IS NOT NULL
                    DROP TABLE Users1
                    CREATE TABLE Users1(UserID INT, UserName VARCHAR(60), RegisterDate DateTime)", Connection);

            SqlContext.Pipe.ExecuteAndSend(Command);
            
            SqlCommand Command2 = new SqlCommand(@"SELECT * FROM tblUsers WHERE Country = 'RUSSIAN FEDERATION' AND 
                                                 AllScore > 5000000", Connection);

            SqlDataReader Reader = Command2.ExecuteReader();

            int i = 1;

            List<SqlInt32> ID = new List<SqlInt32>();
            List<string> Name = new List<string>();
            List<SqlDateTime> RegDate = new List<SqlDateTime>();
            

            while (Reader.Read())
            {
                ID.Add(i++);
                Name.Add(Reader.GetString(1));
                RegDate.Add(Reader.GetSqlDateTime(5));
            }

            Reader.Close();

            SqlCommand Command3 = new SqlCommand(@" 
                INSERT Users1(UserID, UserName, RegisterDate)
                VALUES(@UserID, @UserName, @RegisterDate)", Connection);

            for (i = 0; i < ID.Count; i++)
            {
                Command3.Parameters.AddWithValue("@UserID", ID[i]);
                Command3.Parameters.AddWithValue("@UserName", Name[i]);
                Command3.Parameters.AddWithValue("@RegisterDate", RegDate[i]);

                SqlContext.Pipe.ExecuteAndSend(Command3);

                Command3.Parameters.Clear();
            }
        }
    }
};
