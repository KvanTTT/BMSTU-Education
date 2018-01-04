using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void StoredProcedure3(SqlInt32 Year, out SqlMoney YearPaymentCredit)
    {
        SqlPipe Pipe = SqlContext.Pipe;

        using (SqlConnection Connection = new SqlConnection("context connection = true"))
        {
            Connection.Open();
            SqlCommand Command = new SqlCommand(@"
                  SELECT Sum(PaymentCredit)
                  FROM tblPayments
                  GROUP BY Year(PaymentDate)
                  HAVING Year(PaymentDate) = @Year",
            Connection);
            Command.Parameters.AddWithValue("@Year", Year);

            decimal X = (decimal)Command.ExecuteScalar();
            YearPaymentCredit = new SqlMoney(X);
            Pipe.ExecuteAndSend(Command);
        }
    }
};
