using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void StoredProcedure2(SqlString Name, SqlString Project, SqlDateTime RegisterDate)
    {
        SqlPipe Pipe = SqlContext.Pipe;

        using (SqlConnection Connection = new SqlConnection("context connection = true"))
        {
            Connection.Open();

            SqlCommand Command = new SqlCommand(@"	
                DECLARE @max_id INT
	            SELECT @max_id = Max(UserID)
	            FROM tblUsers
	            INSERT tblUsers (UserID, UserName, Team, Country, Project, RegisterDate, AvgScorePerDay, AllScore)
	            VALUES(@max_id + 1, @Name, NULL, NULL, @Project, @RegisterDate, 0, 0)", Connection);
            Command.Parameters.AddWithValue("@Name", Name);
            Command.Parameters.AddWithValue("@Project", Project);
            Command.Parameters.AddWithValue("@RegisterDate", RegisterDate);
            Pipe.ExecuteAndSend(Command);
        }
    }
};
