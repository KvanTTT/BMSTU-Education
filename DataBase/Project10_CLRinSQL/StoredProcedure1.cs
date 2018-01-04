using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void StoredProcedure1()
    {
        SqlPipe Pipe = SqlContext.Pipe;

        using (SqlConnection Connection = new SqlConnection("context connection = true"))
        {
            Connection.Open();

            SqlCommand Command = new SqlCommand(@"
                IF OBJECT_ID('dbo.tblActiveUsers') IS NOT NULL
                    DROP TABLE tblActiveUsers
	            CREATE TABLE tblActiveUsers
	            (
		            UserID INT PRIMARY KEY,
		            UserName VARCHAR (60) NOT NULL,
	            );                    
	            INSERT tblActiveUsers (UserID, UserName)
	            SELECT tblUsers.UserID, tblUsers.UserName
	            FROM tblUsers
	            WHERE tblUsers.AvgScorePerDay > 200.0", Connection);

            Pipe.ExecuteAndSend(Command);
        }
    }
};
