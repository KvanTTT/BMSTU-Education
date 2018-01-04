using System;
using System.Data.SqlClient;

namespace Primer01
{
    class ConnectionDisplay
    {
        static void Main()
        {
            // Создаем объект "conn" типа "SqlConnection"
            SqlConnection conn = new SqlConnection(@"Data Source=(local)\SQLEXPRESS; Integrated Security = true;");
            try
            {
                // Открываем соединение
                conn.Open();
                Console.WriteLine("Connection opened.");
                // Печатаем свойства соединения
                Console.WriteLine("Connection Properties:");
                Console.WriteLine("\tConnection String: {0}", conn.ConnectionString);
                Console.WriteLine("\tDatabase: {0}", conn.Database);
                Console.WriteLine("\tDataSource: {0}", conn.DataSource);
                Console.WriteLine("\tServerVersion: {0}", conn.ServerVersion);
                Console.WriteLine("\tState: {0}", conn.State);
                Console.WriteLine("\tWorkstationId: {0}", conn.WorkstationId);
                Console.WriteLine("\tPacketSize: {0}", conn.PacketSize);
                Console.WriteLine("\tConnectionTimeout: {0}", conn.ConnectionTimeout);
            }
            catch (SqlException e)
            {
                // Выводим сообщение об ошибке в случае возникновения исключения
                Console.WriteLine("Error: " + e);
            }
            finally
            {
                // Закрываем соединение
                conn.Close();
                Console.WriteLine("Connection closed.");
            }
            Console.ReadLine();
        }
    }
}
