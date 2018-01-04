using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    public class Users
    {
        public int UserID;
        public string UserName;
        public int AllScore;
    }

    [Microsoft.SqlServer.Server.SqlFunction(DataAccess = DataAccessKind.Read, FillRowMethodName = "FillRow2",
        TableDefinition = @"UserID INT, UserName NVARCHAR(60), AllScore INT")]
    public static System.Collections.IEnumerable Function3()
    {
        using (SqlConnection Connection = new SqlConnection("context connection = true"))
        {
            Connection.Open();

            SqlCommand Command = new SqlCommand(@"	
	            SELECT *
	            FROM tblUsers
	            WHERE RegisterDate < '2005-01-01' AND AllScore > 3000000", Connection);

            System.Collections.Generic.List<Users> L = new System.Collections.Generic.List<Users>();

            SqlDataReader Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                Users C = new Users();

                C.UserID = Reader.GetInt32(0);
                C.UserName = (string)Reader.GetSqlString(1);
                C.AllScore = Reader.GetInt32(7);

                L.Add(C);
            }

            return L;
        }
    }
  
    public static void FillRow2(Object obj, out SqlInt32 UserID, out SqlString UserName, out int AllScore)  
    {
        Users Obj = (Users)obj;
        UserID = new SqlInt32(Obj.UserID);
        UserName = Obj.UserName;
        AllScore = Obj.AllScore;
    }
};

