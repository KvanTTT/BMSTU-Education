using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction(DataAccess = DataAccessKind.Read)]
    public static SqlDouble Function1()
    {
        using (SqlConnection Connection = new SqlConnection("context connection = true"))
        {
            Connection.Open();

            SqlCommand Command = new SqlCommand(@"SELECT AVG(TaskCost) FROM tblTasks", Connection);

            return new SqlDouble((double)Command.ExecuteScalar());
        }
    }
};

