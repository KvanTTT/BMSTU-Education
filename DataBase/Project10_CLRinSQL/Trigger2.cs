using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;


public partial class Triggers
{
    // Введите существующую таблицу или представление для конечного объекта и раскомментируйте строку атрибута
    [Microsoft.SqlServer.Server.SqlTrigger(Name = "Trigger2", Target = "tblUsers", Event = "AFTER INSERT")]
    public static void Trigger2()
    {
        SqlPipe Pipe = SqlContext.Pipe;

        using (SqlConnection Connection = new SqlConnection("context connection = true"))
        {
            Connection.Open();

            SqlCommand Command = new SqlCommand(@"
                SET NOCOUNT ON;
	            if (select RegisterDate from inserted) > getdate()
	            rollback
	            print'Вы не можете вставлять пользователя, который может быть в будущем зарегестрируется!!'", Connection);

            Pipe.ExecuteAndSend(Command);
        }
    }
}
