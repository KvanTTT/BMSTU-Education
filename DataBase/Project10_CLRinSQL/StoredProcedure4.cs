using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void StoredProcedure4(out SqlMoney AVG)
    {
        SqlPipe Pipe = SqlContext.Pipe;

        using (SqlConnection Connection = new SqlConnection("context connection = true"))
        {
            Connection.Open();

            SqlCommand Command = new SqlCommand(@"	
                SELECT AVG(PaymentCredit) FROM tblPayments", 
            Connection);

            AVG = new SqlMoney((decimal)Command.ExecuteScalar());
        }
    }
};
