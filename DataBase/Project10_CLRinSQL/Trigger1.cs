using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;


public partial class Triggers
{
    // Введите существующую таблицу или представление для конечного объекта и раскомментируйте строку атрибута
    [Microsoft.SqlServer.Server.SqlTrigger (Name="Trigger1", Target="tblPayments", Event="AFTER DELETE")]
    public static void Trigger1()
    {
        SqlPipe Pipe = SqlContext.Pipe;

        using (SqlConnection Connection = new SqlConnection("context connection = true"))
        {
            Connection.Open();

            SqlCommand Command = new SqlCommand(@"
                print 'Эти поля были удалены'
                select * from deleted", Connection);

            Pipe.ExecuteAndSend(Command);
        }
    }
}
